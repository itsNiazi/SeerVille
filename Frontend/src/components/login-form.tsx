import React, { useState } from "react";
import { cn } from "@/lib/utils";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { GalleryVerticalEnd } from "lucide-react";
import { useAuth } from "@/auth";
import { useRouter, useRouterState } from "@tanstack/react-router";
import { Route } from "@/routes";
import { SigningResponseSchema, SignInRequestSchema, type SignInRequest } from "@/api/internal/auth/auth.schema";
import { signIn } from "@/api/internal/auth/auth.api";

const fallback = "/auth/dashboard" as const;

export function LoginForm({ className, ...props }: React.ComponentProps<"form">) {
  const auth = useAuth();
  const router = useRouter();
  const isLoading = useRouterState({ select: (s) => s.isLoading });
  const navigate = Route.useNavigate();
  const search = Route.useSearch();

  const [isSubmitting, setIsSubmitting] = useState(false);
  const [formError, setFormError] = useState("");
  const [error, setError] = useState("");

  async function onFormSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    setIsSubmitting(true);
    setFormError("");
    setError("");

    try {
      // Retrieve form field inputs
      const formData = new FormData(event.currentTarget);
      const email = formData.get("email")?.toString();
      const password = formData.get("password")?.toString();

      if (!email || !password) {
        return;
      }

      // Input validation (Client data)
      const signInRequest: SignInRequest = { email, password };
      const result = SignInRequestSchema.safeParse(signInRequest);

      if (!result.success) {
        const fieldError = result.error.issues[0].path[0];
        fieldError === "email"
          ? setFormError("Invalid email address")
          : setFormError("Password must be atleast 8 characters");
        return;
      }

      // Sign-in (Server request)
      const response = await signIn(signInRequest);

      if (response.error) {
        switch (response.status) {
          case 401:
            setError("Wrong email or password.");
            break;
          default:
            setError("Unexpected error occured.");
        }
      } else {
        // Output validation (Server data)
        const apiResult = SigningResponseSchema.safeParse(response);
        console.log(response);
        if (apiResult.success) {
          await auth.doSignIn(response);
          await router.invalidate();
          await navigate({ to: search.redirect || fallback });
        }
      }
    } catch (error) {
      setError("Unexpected error occured.");
    } finally {
      setIsSubmitting(false);
    }
  }

  const isLoggingIn = isLoading || isSubmitting;

  return (
    <>
      <form onSubmit={onFormSubmit} className={cn("flex flex-col gap-6", className)} {...props}>
        <fieldset className="flex flex-col items-center gap-2 text-center">
          <h2 className="text-2xl font-bold">Login to your account</h2>
          <p className="text-muted-foreground text-sm text-balance">Enter your email below to login to your account</p>
        </fieldset>

        <fieldset className="grid gap-5">
          <div className="grid gap-3">
            <Label htmlFor="email">Email</Label>
            <Input id="email" name="email" type="email" placeholder="email@example.com" required />
          </div>

          <div className="grid gap-3">
            <Label htmlFor="password">Password</Label>
            <Input id="password" name="password" type="password" required />
          </div>
          <Button type="submit" className="w-full" disabled={isLoggingIn}>
            {isLoggingIn ? "Loading..." : "Login"}
          </Button>
        </fieldset>
        <p className="text-sm text-center text-red-400 min-h-[1.25rem]">{error || formError}</p>
      </form>

      <fieldset>
        <p className="after:border-border relative text-center text-sm after:absolute after:inset-0 after:top-1/2 after:z-0 after:flex after:items-center after:border-t mb-6 mt-3">
          <span className="bg-background text-muted-foreground relative z-10 px-2">Or continue with</span>
        </p>

        <Button variant="outline" className="w-full" onClick={() => navigate({ to: "/signup" })}>
          <GalleryVerticalEnd className="size-4" />
          Sign up
        </Button>
      </fieldset>
    </>
  );
}
