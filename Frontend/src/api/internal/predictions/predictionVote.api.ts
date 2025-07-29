import { api } from "../internal.api";
import type {
  PredictionVoteResponse,
  PredictionVoteListResponse,
  CreatePredictionVoteRequest,
} from "./predictionVote.schema";

export function getPredictionVotes() {
  // Retrieves all prediction votes from authenticated user
  return api<PredictionVoteListResponse>("/predictionVote", { method: "GET" });
}

export function createPredictionVote(createPredictionVote: CreatePredictionVoteRequest) {
  // Creates prediction vote for authenticated user
  return api<PredictionVoteResponse>("/predictionVote", {
    method: "POST",
    body: JSON.stringify(createPredictionVote),
  });
}
