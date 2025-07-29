import { api } from "../internal.api";
import type { SigningResponse, SignInRequest, SignUpRequest } from "./auth.schema";

export function signUp(signUpRequest: SignUpRequest) {
  return api<SigningResponse>("/user", {
    method: "POST",
    body: JSON.stringify(signUpRequest),
  });
}

export function signIn(signInRequest: SignInRequest) {
  return api<SigningResponse>("/user/auth", {
    method: "POST",
    body: JSON.stringify(signInRequest),
  });
}
