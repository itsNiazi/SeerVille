import { createFileRoute, Outlet, redirect, useRouterState } from "@tanstack/react-router";
import { AppSidebar } from "@/components/app-sidebar";
import { SiteHeader } from "@/components/site-header";
import { SidebarInset, SidebarProvider } from "@/components/ui/sidebar";

export const Route = createFileRoute("/auth")({
  //Investigate pathless layout routes
  beforeLoad: ({ context }) => {
    if (!context.auth.isAuthenticated) {
      throw redirect({ to: "/" });
    }
    if (location.pathname === "/auth") {
      throw redirect({ to: "/auth/dashboard" }); // temp fix
    }
  },
  component: RouteComponent,
});

function RouteComponent() {
  const pathname = useRouterState({ select: (s) => s.location.pathname });
  const currPath = pathname.split("/").filter(Boolean).at(-1);
  return (
    <SidebarProvider
      style={
        {
          "--sidebar-width": "calc(var(--spacing) * 72)",
          "--header-height": "calc(var(--spacing) * 12)",
        } as React.CSSProperties
      }
    >
      <AppSidebar variant="inset" />
      <SidebarInset>
        <SiteHeader location={currPath} />
        <div className="flex flex-1 flex-col">
          <div className="@container/main flex flex-1 flex-col gap-2">
            <div className="flex flex-col gap-4 py-4 md:gap-6 md:py-6">
              <Outlet />
            </div>
          </div>
        </div>
      </SidebarInset>
    </SidebarProvider>
  );
}
