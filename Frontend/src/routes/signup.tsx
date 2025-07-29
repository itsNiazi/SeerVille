import { createFileRoute, redirect } from "@tanstack/react-router";
import { SignUpForm } from "@/components/signup-form";
import { z } from "zod";
import AuthLayout from "@/components/layouts/auth-layout";

const fallback = "/auth/dashboard" as const;

export const Route = createFileRoute("/signup")({
  validateSearch: z.object({
    redirect: z.string().optional().catch(""),
  }),
  beforeLoad: ({ context, search }) => {
    if (context.auth.isAuthenticated) {
      throw redirect({ to: search.redirect || fallback });
    }
  },
  component: SignupComponent,
});

function SignupComponent() {
  return (
    <AuthLayout>
      <SignUpForm />
    </AuthLayout>
  );
}
