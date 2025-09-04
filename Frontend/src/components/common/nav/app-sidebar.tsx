import * as React from "react";
import { IconDatabase, IconHelp, IconSearch, IconSettings } from "@tabler/icons-react";
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
  user: {
    name: "shadcn",
    email: "m@example.com",
    avatar: "/avatars/shadcn.jpg",
  },
  navMain: [
    {
      title: "Dashboard",
      url: "/auth/dashboard", //hmm
      // icon: IconDashboard,
      icon: "\u{1F3E0}",
    },
    {
      title: "Predictions",
      url: "/auth/predictions",
      // icon: IconListDetails,
      icon: "\u{1F52E}",
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
    {
      title: "Search",
      url: "#",
      icon: IconSearch,
    },
  ],
  documents: [
    {
      name: "Predictions",
      url: "#",
      icon: IconDatabase,
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
              <span className="text-base font-semibold p-1.5"> 🔭 Seerville</span>
            </a>
          </SidebarMenuItem>
        </SidebarMenu>
      </SidebarHeader>
      <SidebarContent>
        <NavMain items={data.navMain} />
        <NavSecondary items={data.navSecondary} className="mt-auto" />
      </SidebarContent>
      <SidebarFooter>
        <NavUser user={data.user} />
      </SidebarFooter>
    </Sidebar>
  );
}
