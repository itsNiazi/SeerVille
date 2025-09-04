import { createFileRoute, redirect } from "@tanstack/react-router";
import { LoginForm } from "@/components/features/auth/login-form";
import { z } from "zod";
import AuthLayout from "@/components/layouts/auth-layout";

const fallback = "/auth/dashboard" as const;

export const Route = createFileRoute("/")({
  validateSearch: z.object({
    redirect: z.string().optional().catch(""),
  }),
  beforeLoad: ({ context, search }) => {
    if (context.auth.isAuthenticated) {
      throw redirect({ to: search.redirect || fallback });
    }
  },
  component: LoginComponent,
});

function LoginComponent() {
  return (
    <AuthLayout>
      <LoginForm />
    </AuthLayout>
  );
}
