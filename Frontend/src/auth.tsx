import React from "react";
import type { SigningResponse } from "./api/internal/auth/auth.schema";

export interface AuthContext {
  isAuthenticated: boolean;
  doSignIn: (signIn: SigningResponse) => void;
  doSignOut: () => void;
  user: SigningResponse | null;
}

const AuthContext = React.createContext<AuthContext | null>(null);

const storageKey = import.meta.env.VITE_INTERNAL_STORAGE_KEY;

function getStoredUser(): SigningResponse | null {
  const stored = localStorage.getItem(storageKey);
  return stored ? JSON.parse(stored) : null;
}

function setStoredUser(user: SigningResponse | null) {
  if (user) {
    localStorage.setItem(storageKey, JSON.stringify(user));
  } else {
    localStorage.removeItem(storageKey);
  }
}

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const [user, setUser] = React.useState<SigningResponse | null>(getStoredUser());
  const isAuthenticated = !!user;

  function doSignOut() {
    setStoredUser(null);
    setUser(null);
  }

  function doSignIn(signIn: SigningResponse) {
    setStoredUser(signIn);
    setUser(signIn);
  }

  React.useEffect(() => {
    setUser(getStoredUser());
  }, []);

  return <AuthContext.Provider value={{ isAuthenticated, user, doSignIn, doSignOut }}>{children}</AuthContext.Provider>;
}

export function useAuth() {
  const context = React.useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider.");
  }
  return context;
}
