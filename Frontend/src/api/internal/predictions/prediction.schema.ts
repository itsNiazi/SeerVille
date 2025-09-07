import { z } from "zod";

const errorMessages = {
  topicId: "Please provide a valid topic UUID",
  min: "Prediction name must be atleast 2 characters",
  date: "Please provide a valid date",
  bool: "Please provide a boolean value",
};

export const PredictionResponseSchema = z.object({
  predictionId: z.uuid(),
  creatorId: z.uuid(),
  topicId: z.uuid(),
  predictionName: z.string(),
  predictionRules: z.string(),
  predictionDate: z.string(),
  resolutionDate: z.string(),
  isResolved: z.boolean(),
  isCorrect: z.boolean().nullable(),
  resolvedAt: z.string().nullable(),
});

export const PredictionSummaryRequestSchema = z.object({
  topicId: z.uuid().nullable().optional(),
  isResolved: z.boolean().nullable().optional(),
  sortBy: z.enum(["new", "endingsoon", "popular"]).optional(),
  pageSize: z.number().positive().nullable().optional(),
});

export const PredictionSummaryResponseSchema = z.object({
  predictionId: z.uuid(),
  creatorId: z.uuid(),
  topicId: z.uuid(),
  predictionName: z.string(),
  predictionRules: z.string(),
  predictionDate: z.string(),
  resolutionDate: z.string(),
  isResolved: z.boolean(),
  isCorrect: z.boolean().nullable(),
  resolvedAt: z.string().nullable(),
  yesVotes: z.number(),
  noVotes: z.number(),
  totalVotes: z.number(),
  predictedOutcome: z.boolean().nullable(),
});

export const PredictionListResponseSchema = z.array(PredictionResponseSchema);
export const PredictionSummaryListResponseSchema = z.array(PredictionSummaryResponseSchema);

export const CreatePredictionRequestSchema = z.object({
  topicId: z.uuid(errorMessages.topicId),
  predictionName: z.string().min(2, errorMessages.min),
  resolutionDate: z.string(errorMessages.date),
});

export const UpdatePredictionRequestSchema = z.object({
  predictionName: z.string().min(2, errorMessages.min),
  resolutionDate: z.string(errorMessages.date),
});

export const PatchPredictionRequestSchema = z.object({
  isResolved: z.boolean(errorMessages.bool),
  isCorrect: z.boolean(errorMessages.bool),
});

export type PredictionResponse = z.infer<typeof PredictionResponseSchema>;
export type PredictionListResponse = z.infer<typeof PredictionListResponseSchema>;

export type PredictionSummaryRequest = z.infer<typeof PredictionSummaryRequestSchema>;
export type PredictionSummaryResponse = z.infer<typeof PredictionSummaryResponseSchema>;
export type PredictionSummaryListResponse = z.infer<typeof PredictionSummaryListResponseSchema>;

export type CreatePredictionRequest = z.infer<typeof CreatePredictionRequestSchema>;
export type UpdatePredictionRequest = z.infer<typeof UpdatePredictionRequestSchema>;
export type PatchPredictionRequest = z.infer<typeof PatchPredictionRequestSchema>;
