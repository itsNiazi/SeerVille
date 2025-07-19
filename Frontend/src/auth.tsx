import React from "react";

export type userLoginDto = {
  email: string;
  password: string;
};

export type userDto = {
  userId: string;
  username: string;
  email: string;
  role: string;
  accessToken: string;
  createdAt: string;
};

export interface AuthContext {
  isAuthenticated: boolean;
  login: (username: string) => Promise<void>;
  logout: () => Promise<void>;
  user: userDto | null; //null?
}

const AuthContext = React.createContext<AuthContext | null>(null);

const key = "seerville.auth.user";

function getStoredUser(): userDto | null {
  const stored = localStorage.getItem(key);
  return stored ? JSON.parse(stored) : null;
}

function setStoredUser(user: userDto | null) {
  if (user) {
    localStorage.setItem(key, JSON.stringify(user));
  } else {
    localStorage.removeItem(key);
  }
}

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const [user, setUser] = React.useState<userDto | null>(getStoredUser());
  const isAuthenticated = !!user;

  function logout() {
    setStoredUser(null);
    setUser(null);
  }

  function login(userDTO: userDto) {
    setStoredUser(userDTO);
    setUser(userDTO);
  }

  React.useEffect(() => {
    setUser(getStoredUser());
    console.log(user);
  }, []);

  return <AuthContext.Provider value={{ isAuthenticated, user, login, logout }}>{children}</AuthContext.Provider>;
}

export function useAuth() {
  const context = React.useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used withing an AuthProvider.");
  }
  return context;
}
