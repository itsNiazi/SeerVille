import { api } from "../internal.api";
import type { CreateTopicRequest, TopicListResponse, TopicResponse, UpdateTopicRequest } from "./topic.schema";

export function getTopics() {
  return api<TopicResponse>("/topic", { method: "GET" });
}

export function getTopicById(id: string) {
  return api<TopicListResponse>(`/topic/${id}`, { method: "GET" });
}

export function createTopic(createRequest: CreateTopicRequest) {
  return api<TopicResponse>("/topic", {
    method: "POST",
    body: JSON.stringify(createRequest),
  });
}

export function updateTopic(id: string, updateRequest: UpdateTopicRequest) {
  return api<TopicResponse>(`/topic/${id}`, {
    method: "PUT",
    body: JSON.stringify(updateRequest),
  });
}

export function deleteTopic(id: string) {
  return api<void>(`/topic/${id}`, { method: "DELETE" });
}
