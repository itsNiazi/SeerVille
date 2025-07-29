import { z } from "zod";

export const PredictionResponseSchema = z.object({
  predictionId: z.uuid(),
  creatorId: z.uuid(),
  topicId: z.uuid(),
  predicationName: z.string(),
  predictionDate: z.string(),
  resolutionDate: z.string(),
  isResolved: z.boolean(),
  isCorrect: z.boolean().nullable(),
  resolvedAt: z.string().nullable(),
});

export const PredictionListResponseSchema = z.array(PredictionResponseSchema);

export const CreatePredictionRequestSchema = z.object({
  topicId: z.uuid(),
  predictionName: z.string().min(2),
  resolutionDate: z.string(),
});

export const UpdatePredictionRequestSchema = z.object({
  predictionName: z.string().min(2),
  resolutionDate: z.string(),
});

export const PatchPredictionRequestSchema = z.object({
  isResolved: z.boolean(),
  isCorrect: z.boolean(),
});

export type PredictionResponse = z.infer<typeof PredictionResponseSchema>;
export type PredictionListResponse = z.infer<typeof PredictionListResponseSchema>;
export type CreatePredictionRequest = z.infer<typeof CreatePredictionRequestSchema>;
export type UpdatePredictionRequest = z.infer<typeof UpdatePredictionRequestSchema>;
export type PatchPredictionRequest = z.infer<typeof PatchPredictionRequestSchema>;
