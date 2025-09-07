import type { PredictionSummaryRequest } from "./predictions/prediction.schema";
import { buildSearchParams } from "@/lib/string";

const BASE_URL = import.meta.env.VITE_INTERNAL_BASE_URL;
const storageKey = import.meta.env.VITE_INTERNAL_STORAGE_KEY;
const contentType = "application/json";

type ApiError = { error: boolean; status: number };

function getAuthToken(storageKey: string): string | null {
  const raw = localStorage.getItem(storageKey);
  return raw ? JSON.parse(raw).accessToken : null;
}

interface ApiOptions extends RequestInit {
  params?: PredictionSummaryRequest;
}

export async function api<T>(endpoint: string, options: ApiOptions = {}): Promise<T | ApiError> {
  const { params, ...fetchOptions } = options;
  const authToken = getAuthToken(storageKey);

  // Build query string if params exist
  const url = params ? `${BASE_URL}${endpoint}?${buildSearchParams(params)}` : `${BASE_URL}${endpoint}`;

  const headers: HeadersInit = {
    "Content-Type": contentType,
    ...(fetchOptions.headers || {}),
    ...(authToken ? { Authorization: `Bearer ${authToken}` } : {}),
  };

  const response = await fetch(url, {
    headers,
    ...fetchOptions,
  });

  if (response.status === 204) return;

  if (!response.ok) {
    return { error: true, status: response.status };
  }

  return response.json();
}
