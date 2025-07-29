import { z } from "zod";

export const SignInRequestSchema = z.object({
  email: z.email(),
  password: z.string().min(8),
});

export const SignUpRequestSchema = z.object({
  username: z
    .string()
    .min(2)
    .max(30)
    .regex(/^[a-zA-Z0-9_.-]+$/),
  email: z.email(),
  password: z.string().min(8),
});

export const SigningResponseSchema = z.object({
  userId: z.uuid(),
  username: z.string(),
  email: z.email(),
  role: z.enum(["user", "moderator", "admin"]),
  accessToken: z.jwt(),
  createdAt: z.string(), //figure out proper date validation
});

// TypesScript types inferred from schemas.
export type SignInRequest = z.infer<typeof SignInRequestSchema>;
export type SignUpRequest = z.infer<typeof SignUpRequestSchema>;
export type SigningResponse = z.infer<typeof SigningResponseSchema>;
