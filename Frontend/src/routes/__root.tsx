import { createRootRoute, Link, Outlet } from "@tanstack/react-router";
import { TanStackRouterDevtools } from "@tanstack/react-router-devtools";

// Root Layout
export const Route = createRootRoute({
  component: () => (
    <>
      <Link to="/">Home</Link>
      <hr />
      <Outlet />
      <TanStackRouterDevtools />
    </>
  ),
});
