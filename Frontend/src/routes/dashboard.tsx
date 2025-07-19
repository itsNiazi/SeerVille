import { createFileRoute } from "@tanstack/react-router";
import { useAuth } from "../auth";

export const Route = createFileRoute("/dashboard")({
  component: DashboardComponent,
});

function DashboardComponent() {
  const auth = useAuth();
  const navigate = Route.useNavigate();

  async function handleLogout() {
    await auth.logout();
    await navigate({ to: "/" });
  }
  return (
    <div>
      <button onClick={handleLogout}>Logout</button>
      {auth.user ? <h1>Hi {auth.user?.username}</h1> : <p>You need to login</p>}
    </div>
  );
}
