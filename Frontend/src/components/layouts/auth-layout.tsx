import { Link } from "@tanstack/react-router";
import React from "react";

export default function AuthLayout({ children }: { children: React.ReactNode }) {
  return (
    <main className="grid min-h-svh lg:grid-cols-2">
      <section className="flex flex-col gap-4 p-6 md:p-10">
        <header className="flex justify-center gap-2 md:justify-start">
          <Link to="/" className="flex items-center gap-2 font-medium">
            <h1 className="flex items-center justify-center gap-2 text-xl font-semibold ">
              🔭
              <p>SeerVille</p>
            </h1>
          </Link>
        </header>
        <section className="flex flex-1 items-center justify-center">
          <article className="w-full max-w-xs h-[500px] items-start">{children}</article>
        </section>
      </section>

      <section className="bg-muted relative hidden lg:block">
        <img src="/img.jpg" alt="Background image." className="absolute inset-0 h-full w-full object-cover" />
      </section>
    </main>
  );
}
