import * as React from "react";
import { IconHelp, IconSettings } from "@tabler/icons-react";
import { NavMain } from "@/components/common/nav/nav-main";
import { NavSecondary } from "@/components/common/nav/nav-secondary";
import { NavUser } from "@/components/common/nav/nav-user";
import {
  Sidebar,
  SidebarContent,
  SidebarFooter,
  SidebarHeader,
  SidebarMenu,
  SidebarMenuItem,
} from "@/components/ui/sidebar";

const data = {
  navMain: [
    {
      title: "Dashboard",
      url: "/auth/dashboard",
      icon: "\u{1F3E0}",
    },
    {
      title: "Predictions",
      url: "/auth/predictions",

      icon: "🗂️",
    },
    {
      title: "Seer Mode",
      url: "#",
      icon: "\u{1F52E}",
    },
    {
      title: "Achievements",
      url: "#",
      icon: "🎖️",
    },
    {
      title: "Leaderboard",
      url: "#",
      icon: "🏆",
    },
  ],
  navSecondary: [
    {
      title: "Settings",
      url: "#",
      icon: IconSettings,
    },
    {
      title: "Get Help",
      url: "#",
      icon: IconHelp,
    },
  ],
};

export function AppSidebar({ ...props }: React.ComponentProps<typeof Sidebar>) {
  return (
    <Sidebar collapsible="offcanvas" {...props}>
      <SidebarHeader>
        <SidebarMenu>
          <SidebarMenuItem>
            <a href="#">
              <span className="text-xl font-semibold p-1.5">🔭 Seerville</span>
            </a>
          </SidebarMenuItem>
        </SidebarMenu>
      </SidebarHeader>
      <SidebarContent>
        <NavMain items={data.navMain} />
        <NavSecondary items={data.navSecondary} className="mt-auto" />
      </SidebarContent>
      <SidebarFooter>
        <NavUser />
      </SidebarFooter>
    </Sidebar>
  );
}
