import { z } from "zod";

export const TopicResponseSchema = z.object({
  topicId: z.string(),
  name: z.string(),
  Description: z.string(),
});

export const TopicListResponseSchema = z.array(TopicResponseSchema);

export const CreateTopicRequestSchema = z.object({
  name: z
    .string()
    .min(2)
    .regex(/^[a-zA-Z0-9_.-]+$/),
  Description: z.string(),
});

export const UpdateTopicRequestSchema = z.object({
  name: z
    .string()
    .min(2)
    .regex(/^[a-zA-Z0-9_.-]+$/),
  Description: z.string(),
});

export type TopicResponse = z.infer<typeof TopicResponseSchema>;
export type TopicListResponse = z.infer<typeof TopicListResponseSchema>;
export type CreateTopicRequest = z.infer<typeof CreateTopicRequestSchema>;
export type UpdateTopicRequest = z.infer<typeof UpdateTopicRequestSchema>;
