import { z } from "zod";

const errorMessage = {
  predictionId: "Please provide a valid prediction UUID",
  predictedOutCome: "Please provide a boolean value",
};

export const PredictionVoteResponseSchema = z.object({
  voteId: z.uuid(),
  predictionId: z.uuid(),
  userId: z.string(),
  predictedOutcome: z.boolean(),
  votedAt: z.string(),
});

export const PredictionVoteListResponseSchema = z.array(PredictionVoteResponseSchema);

export const CreatePredictionVoteRequestSchema = z.object({
  predictionId: z.uuid(errorMessage.predictionId),
  predictedOutcome: z.boolean(errorMessage.predictedOutCome),
});

export type PredictionVoteResponse = z.infer<typeof PredictionVoteResponseSchema>;
export type PredictionVoteListResponse = z.infer<typeof PredictionVoteListResponseSchema>;
export type CreatePredictionVoteRequest = z.infer<typeof CreatePredictionVoteRequestSchema>;
