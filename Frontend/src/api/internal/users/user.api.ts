import { api } from "../internal.api";
import type { UserListResponse, UserResponse } from "./user.schema";

export function getUsers() {
  return api<UserListResponse>("/user", { method: "GET" });
}

export function getUserById(id: string) {
  return api<UserResponse>(`/user/${id}`, { method: "GET" });
}
