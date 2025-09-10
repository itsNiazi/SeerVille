import { z } from "zod";

export const UserResponseSchema = z.object({
  userId: z.uuid(),
  username: z.string().min(2),
  email: z.email(),
  role: z.enum(["user", "moderator", "admin"]),
  createdAt: z.string(),
});
export const UserPredictionResponseSchema = z.object({
  predictionId: z.uuid(),
  predictionName: z.string(),
  predictionRules: z.string(),
  predictionDate: z.string(),
  resolutionDate: z.string(),
  votedDate: z.string(),
  isResolved: z.boolean(),
  isCorrect: z.boolean().nullable(),
  topicName: z.string(),
  userVote: z.boolean(),
});

export const UserStatsResponseSchema = z.object({
  totalVotes: z.number(),
  resolvedVotes: z.number(),
  correctVotes: z.number(),
  accuracy: z.number(),
});

export const UserTopTopicSchema = z.object({
  topicId: z.uuid(),
  topicName: z.string(),
  topicIcon: z.string(),
  totalVotes: z.number(),
  resolvedVotes: z.number(),
  accuracy: z.number(),
});

export const UserListResponseSchema = z.array(UserResponseSchema);
export const ListUserTopTopicsResponseSchema = z.array(UserTopTopicSchema);
export const ListUserPredictionsResponseSchema = z.array(UserPredictionResponseSchema);

export type UserResponse = z.infer<typeof UserResponseSchema>;
export type UserListResponse = z.infer<typeof UserListResponseSchema>;
export type UserStatsResponse = z.infer<typeof UserStatsResponseSchema>;
export type UserTopTopicsResponse = z.infer<typeof UserTopTopicSchema>;
export type ListUserTopTopicsResponse = z.infer<typeof ListUserTopTopicsResponseSchema>;
export type UserPredictionResponse = z.infer<typeof UserPredictionResponseSchema>;
export type ListUserPredictionsResponse = z.infer<typeof ListUserPredictionsResponseSchema>;
