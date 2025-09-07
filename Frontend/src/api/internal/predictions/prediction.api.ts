import { api } from "../internal.api";
import type {
  PredictionResponse,
  CreatePredictionRequest,
  PatchPredictionRequest,
  UpdatePredictionRequest,
  PredictionSummaryListResponse,
  PredictionSummaryRequest,
} from "./prediction.schema";

export function getPredictions(filter?: PredictionSummaryRequest) {
  return api<PredictionSummaryListResponse>("/prediction", {
    method: "GET",
    params: filter,
  });
}

export function getPredictionById(id: string) {
  return api<PredictionResponse>(`/prediction/${id}`, { method: "GET" });
}

export function getPredictionsByTopicId(id: string) {
  return api<PredictionResponse>(`/prediction?topicid=${id}`, { method: "GET" });
}

export function createPrediction(createPrediction: CreatePredictionRequest) {
  return api<PredictionResponse>("/prediction", {
    method: "POST",
    body: JSON.stringify(createPrediction),
  });
}

export function updatePredictionById(id: string, updatePrediction: UpdatePredictionRequest) {
  return api<PredictionResponse>(`/prediction/${id}`, {
    method: "PUT",
    body: JSON.stringify(updatePrediction),
  });
}

export function patchPredictionById(id: string, patchPrediction: PatchPredictionRequest) {
  return api<PredictionResponse>(`/prediction/${id}`, {
    method: "PATCH",
    body: JSON.stringify(patchPrediction),
  });
}

export function deletePredictionById(id: string) {
  return api<void>(`/prediction/${id}`, { method: "DELETE" });
}
