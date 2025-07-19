import { createRootRoute, Outlet } from "@tanstack/react-router";
import { TanStackRouterDevtools } from "@tanstack/react-router-devtools";

import type { AuthContext } from "../auth";

interface MyRouterContext {
  auth: AuthContext;
}

// Root Layout
export const Route = createRootRoute<MyRouterContext>({
  component: () => (
    <>
      <Outlet />
      <TanStackRouterDevtools />
    </>
  ),
});
