import { z } from "zod";

const errorMessages = {
  email: {
    invalid: "Invalid email address",
  },
  username: {
    min: "Username must be atleast 2 characters",
    max: "Username must be less than 30 characters",
    regex: "Username must not contain special characters",
  },
  password: {
    min: "Password must be atleast 8 characters",
  },
};

export const SignInRequestSchema = z.object({
  email: z.email(errorMessages.email.invalid),
  password: z.string().min(8, errorMessages.password.min),
});

export const SignUpRequestSchema = z.object({
  username: z
    .string()
    .min(2, errorMessages.username.min)
    .max(30, errorMessages.username.max)
    .regex(/^[a-zA-Z0-9_.-]+$/, errorMessages.username.regex),
  email: z.email(errorMessages.email.invalid),
  password: z.string().min(8, errorMessages.password.min),
});

export const SigningResponseSchema = z.object({
  userId: z.uuid(),
  username: z.string(),
  email: z.email(),
  role: z.enum(["user", "moderator", "admin"]),
  accessToken: z.jwt(),
  createdAt: z.string(), //figure out proper date validation
  avatarPath: z.string(),
});

// TypesScript types inferred from schemas.
export type SignInRequest = z.infer<typeof SignInRequestSchema>;
export type SignUpRequest = z.infer<typeof SignUpRequestSchema>;
export type SigningResponse = z.infer<typeof SigningResponseSchema>;
