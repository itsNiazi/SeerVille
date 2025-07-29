import React from "react";
import ReactDOM from "react-dom/client";
import { RouterProvider, createRouter } from "@tanstack/react-router";
import { Toaster } from "sonner";
import "./styles/global.css";

// Import the generated route tree
import { routeTree } from "./routeTree.gen";
import { AuthProvider, useAuth } from "./auth";

// Set up a Router instance from generated routerTree
const router = createRouter({
  routeTree,
  context: {
    auth: undefined!, // This will be set after wrapping the app in an AuthProvider
  },
});

// Register things for typesafety here
declare module "@tanstack/react-router" {
  interface Register {
    router: typeof router;
  }
}

// Inner application component
// With Auth context from parent
function InnerApp() {
  const auth = useAuth();
  return <RouterProvider router={router} context={{ auth }} />;
}

// Application Root Component
// With Auth context provided to children
function App() {
  return (
    <AuthProvider>
      <InnerApp />
      {/* Toaster here or in layout? */}
      <Toaster />
    </AuthProvider>
  );
}

const rootElement = document.getElementById("root")!;
if (!rootElement.innerHTML) {
  const root = ReactDOM.createRoot(rootElement);
  root.render(
    <React.StrictMode>
      <App />
    </React.StrictMode>
  );
}
