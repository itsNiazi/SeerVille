import { api } from "../internal.api";
import type {
  ListUserTopTopicsResponse,
  UserStatsResponse,
  UserListResponse,
  UserResponse,
  ListUserPredictionsResponse,
} from "./user.schema";

export function getUsers() {
  return api<UserListResponse>("/user", { method: "GET" });
}

export function getUserById(id: string) {
  return api<UserResponse>(`/user/${id}`, { method: "GET" });
}

export function getUserStats() {
  return api<UserStatsResponse>("/user/stats", { method: "GET" });
}

// Parameter for how many topics change to /user/topics?sort=""&size=""
export function getUserTopTopics() {
  return api<ListUserTopTopicsResponse>("/user/topics", { method: "GET" });
}

// make queryable (sorting, status, name etc.)
export function getUserPredictions() {
  return api<ListUserPredictionsResponse>("/user/predictions", { method: "GET" });
}
