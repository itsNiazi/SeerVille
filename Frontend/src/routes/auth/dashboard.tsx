import { createFileRoute, useLoaderData } from "@tanstack/react-router";
import { useAuth } from "../../auth";

import { PredictionTable } from "@/components/features/predictions/prediction-table";
import { columns } from "@/components/features/predictions/prediction-table";
import { BaseCard } from "@/components/common/base-card";
import { getUserPredictions, getUserStats, getUserTopTopics } from "@/api/internal/users/user.api";
import { abbreviateCount } from "@/lib/string";

export const Route = createFileRoute("/auth/dashboard")({
  loader: async () => {
    const [userStats, topicStats, userPredictions] = await Promise.all([
      getUserStats(),
      getUserTopTopics(),
      getUserPredictions(),
    ]);
    return { userStats, topicStats, userPredictions };
  },
  component: DashboardComponent,
});

export default function DashboardComponent() {
  const auth = useAuth();
  const { userStats, topicStats, userPredictions } = useLoaderData({ from: "/auth/dashboard" });

  return (
    <div className="max-w-[1400px] w-[95%] mx-auto py-6 space-y-6">
      <div className="text-center">
        <h1 className="text-3xl font-bold animate-gradient-x">Hi {auth.user?.username}!</h1>
        <p className="text-sm text-muted-foreground">What are you predicting today?</p>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-4 gap-6">
        <BaseCard
          label="🗳️ Total Votes"
          description="All votes you have made"
          data={abbreviateCount(userStats.totalVotes)}
        />
        <BaseCard
          label="✅ Correct Votes"
          description="Resolved predictions you got right"
          data={abbreviateCount(userStats.correctVotes)}
        />
        <BaseCard
          label="🎯 Accuracy"
          description="Correct predictions ratio"
          data={(userStats.accuracy * 100).toFixed(1) + "%"}
        />

        <BaseCard label="Top Voted Topics">
          {topicStats.map((stat) => (
            <div key={stat.topicName} className="flex justify-between items-center max-w-100 overflow-clip">
              <div className="flex gap-5">
                <p>{stat.topicIcon}</p>
                <p>{stat.topicName}</p>
              </div>

              <p className="text-lg font-semibold flex justify-between items-center gap-2 min-w-19">
                {abbreviateCount(stat.resolvedVotes)}
                <span className="font-normal text-sm text-muted-foreground">({(stat.accuracy * 100).toFixed(1)}%)</span>
              </p>
            </div>
          ))}
        </BaseCard>
      </div>
      <PredictionTable columns={columns} data={userPredictions} />
    </div>
  );
}
