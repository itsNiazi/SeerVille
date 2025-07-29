import { z } from "zod";

export const UserResponseSchema = z.object({
  userId: z.uuid(),
  username: z.string().min(2),
  email: z.email(),
  role: z.enum(["user", "moderator", "admin"]),
  createdAt: z.string(),
});

export const UserListResponseSchema = z.array(UserResponseSchema);

export type UserResponse = z.infer<typeof UserResponseSchema>;
export type UserListResponse = z.infer<typeof UserListResponseSchema>;
