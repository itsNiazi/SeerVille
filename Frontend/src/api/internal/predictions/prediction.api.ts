import { api } from "../internal.api";
import type {
  PredictionListResponse,
  PredictionResponse,
  CreatePredictionRequest,
  PatchPredictionRequest,
  UpdatePredictionRequest,
} from "./prediction.schema";

export function getPredictions() {
  return api<PredictionListResponse>("/prediction", { method: "GET" });
}

export function getPredictionById(id: string) {
  return api<PredictionResponse>(`/prediction/${id}`, { method: "GET" });
}

export function getPredictionsByTopicId(id: string) {
  //should be query param?
  return api<PredictionResponse>(`/prediction/topic/${id}`, { method: "GET" });
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
