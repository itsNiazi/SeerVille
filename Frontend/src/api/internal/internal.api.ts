const BASE_URL = import.meta.env.VITE_INTERNAL_BASE_URL;
const storageKey = import.meta.env.VITE_INTERNAL_STORAGE_KEY;
const contentType = "application/json";

type ApiError = { error: boolean; status: number };

function getAuthToken(storageKey: string): string | null {
  const raw = localStorage.getItem(storageKey);
  return raw ? JSON.parse(raw).accessToken : null;
}

export async function api<T>(endpoint: string, options: RequestInit = {}): Promise<T | ApiError> {
  const authToken = getAuthToken(storageKey);
  const headers: HeadersInit = {
    "Content-Type": contentType,
    ...(options.headers || {}),
    ...(authToken ? { Authorization: `Bearer ${authToken}` } : {}),
  };

  const apiResponse = await fetch(`${BASE_URL}${endpoint}`, {
    headers,
    ...options,
  });

  if (apiResponse.status === 204) return; // REVISIT, without this refresh with usestate creates problems for deletes

  if (!apiResponse.ok) {
    return {
      error: true,
      status: apiResponse.status,
    };
  }

  return apiResponse.json();
}
