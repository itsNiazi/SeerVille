import React from "react";
import { createFileRoute, redirect, useRouter, useRouterState } from "@tanstack/react-router";

import { useAuth } from "../auth";

const fallback = "/dashboard" as const;

export const Route = createFileRoute("/")({
  beforeLoad: ({ context, search }) => {
    if (context.auth.isAuthenticated) {
      throw redirect({ to: search.redirect || fallback });
    }
  },
  component: LoginComponent,
});

function LoginComponent() {
  const auth = useAuth();
  const router = useRouter();
  const isLoading = useRouterState({ select: (s) => s.isLoading });
  const navigate = Route.useNavigate();
  const [isSubmitting, setIsSubmitting] = React.useState(false);

  const search = Route.useSearch();

  async function onFormSubmit(event: React.FormEvent<HTMLFormElement>) {
    setIsSubmitting(true);
    try {
      event.preventDefault();
      const data = new FormData(event.currentTarget);
      const fieldValueEmail = data.get("email");
      const fieldValuePassword = data.get("password");

      if (!fieldValueEmail || !fieldValuePassword) return;
      const email = fieldValueEmail.toString();
      const password = fieldValuePassword.toString();

      const loginDto = { email: email, password: password };

      await router.invalidate();
      const response = await fetch("http://localhost:5290/api/user/auth", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(loginDto),
      });

      if (response.status === 200) {
        const user = await response.json();
        await auth.login(user);
      }

      await navigate({ to: search.redirect || fallback });
    } catch (error) {
      console.error("Error logging in: ", error);
    } finally {
      setIsSubmitting(false);
    }
  }

  const isLoggingIn = isLoading || isSubmitting;

  return (
    <div>
      <h3>Login page</h3>
      {search.redirect ? (
        <p>You need to login to access this page.</p>
      ) : (
        <p>Login to see all the cool content in here.</p>
      )}
      <form onSubmit={onFormSubmit}>
        <fieldset disabled={isLoggingIn}>
          <div>
            <label htmlFor="email-input">Email</label>
            <input id="email-input" name="email" placeholder="Enter your name" type="email" required />
            <label htmlFor="password-input">Password</label>
            <input id="password-input" name="password" placeholder="Enter your password" type="password" required />
          </div>
          <button type="submit">{isLoggingIn ? "Loading..." : "Login"}</button>
        </fieldset>
      </form>
    </div>
  );
}
