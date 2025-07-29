import { createFileRoute } from "@tanstack/react-router";
import { useAuth } from "../../auth";
import { Button } from "@/components/ui/button";

export const Route = createFileRoute("/auth/dashboard")({
  component: DashboardComponent,
});

function DashboardComponent() {
  const auth = useAuth();
  const navigate = Route.useNavigate();

  async function handleLogout() {
    await auth.doSignOut();
    await navigate({ to: "/" });
  }
  return (
    <div className="text-center">
      <Button size={"default"} onClick={handleLogout}>
        Log out
      </Button>
      {auth.user ? <h1>Hi {auth.user?.username}</h1> : <p>You need to login</p>}
    </div>
  );
}
