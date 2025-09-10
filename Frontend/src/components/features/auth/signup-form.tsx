import React, { useState } from "react";
import { cn } from "@/lib/utils";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { GalleryVerticalEnd } from "lucide-react";
import { useAuth } from "@/auth";
import { useNavigate, useRouter, useRouterState } from "@tanstack/react-router";
import { SigningResponseSchema, SignUpRequestSchema, type SignUpRequest } from "@/api/internal/auth/auth.schema";
import { signUp } from "@/api/internal/auth/auth.api";

const fallback = "/auth/dashboard" as const;

export function SignUpForm({ className, ...props }: React.ComponentProps<"form">) {
  const auth = useAuth();
  const router = useRouter();
  const isLoading = useRouterState({ select: (s) => s.isLoading });
  const navigate = useNavigate();

  const [isSubmitting, setIsSubmitting] = useState(false);
  const isLoggingIn = isLoading || isSubmitting;
  const [formError, setFormError] = useState("");
  const [error, setError] = useState("");

  async function onFormSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    setIsSubmitting(true);
    setFormError("");
    setError("");

    try {
      const formData = new FormData(event.currentTarget);
      const email = formData.get("email")?.toString();
      const username = formData.get("username")?.toString();
      const password = formData.get("password")?.toString();

      if (!username || !email || !password) {
        return;
      }

      const SignUpRequest: SignUpRequest = { username, email, password };
      const result = SignUpRequestSchema.safeParse(SignUpRequest);

      if (!result.success) {
        const fieldError = result.error.issues[0].message;
        setFormError(fieldError);
        return;
      }

      const response = await signUp(SignUpRequest);
      if (response.error) {
        switch (response.status) {
          case 400:
            setError("Invalid input, please try again.");
            break;
          case 409:
            setError("Email already exists");
            break;
          default:
            setError("Unexpected error occured.");
        }
        return;
      }

      const apiResult = SigningResponseSchema.safeParse(response);
      if (apiResult.success) {
        await auth.doSignIn(response);
        await router.invalidate();
        await navigate({ to: fallback });
      }
    } catch (error) {
      setError("Unexpected error occured.");
    } finally {
      setIsSubmitting(false);
    }
  }

  return (
    <>
      <form onSubmit={onFormSubmit} className={cn("flex flex-col gap-6", className)} {...props}>
        <fieldset className="flex flex-col items-center gap-2 text-center">
          <h2 className="text-2xl font-bold">Create an account</h2>
          <p className="text-muted-foreground text-sm text-balance">Enter the form below to create an account</p>
        </fieldset>

        <fieldset className="grid gap-5">
          <div className="grid gap-3">
            <Label htmlFor="email">Email</Label>
            <Input id="email" name="email" type="email" placeholder="email@example.com" required />
          </div>

          <div className="grid gap-3">
            <Label htmlFor="username">Username</Label>
            <Input id="username" name="username" type="text" required />
          </div>

          <div className="grid gap-3">
            <Label htmlFor="password">Password</Label>
            <Input id="password" name="password" type="password" required />
          </div>
          <Button type="submit" className="w-full" disabled={isLoggingIn}>
            {isLoggingIn ? "Loading..." : "Sign Up"}
          </Button>
          <p className="text-sm text-center text-red-400 min-h-[1.25rem]">{error || formError}</p>
        </fieldset>
      </form>
      <fieldset>
        <p className="after:border-border relative text-center text-sm after:absolute after:inset-0 after:top-1/2 after:z-0 after:flex after:items-center after:border-t mb-6 mt-3">
          <span className="bg-background text-muted-foreground relative z-10 px-2">Or continue with</span>
        </p>

        <Button variant="outline" className="w-full" onClick={() => navigate({ to: "/" })}>
          <GalleryVerticalEnd className="size-4" />
          Sign In
        </Button>
      </fieldset>
    </>
  );
}
