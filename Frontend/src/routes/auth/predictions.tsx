import { useRef, useState } from "react";
import { createFileRoute, useLoaderData } from "@tanstack/react-router";
import { useAuth } from "@/auth";
import { getTopics } from "@/api/internal/topics/topic.api";
import {
  deletePredictionById,
  getPredictions,
  getPredictionsByTopicId,
} from "@/api/internal/predictions/prediction.api";
import type { PredictionSummaryResponse } from "@/api/internal/predictions/prediction.schema";

import { Combobox } from "@/components/common/combo-box";
import { NewPredictionButton } from "@/components/features/predictions/new-prediction-dialog";
import { PredictionList } from "@/components/features/predictions/prediction-list";

import { toast } from "sonner";
import { SelectBox } from "@/components/common/select-box";

export const Route = createFileRoute("/auth/predictions")({
  loader: async () => {
    const [predictions, topics] = await Promise.all([getPredictions(), getTopics()]);
    return { predictions, topics };
  },
  component: PredictionsComponent,
});

function PredictionsComponent() {
  const auth = useAuth();
  const { predictions: initialPredictions, topics } = useLoaderData({ route: Route });
  const [predictions, setPredictions] = useState<PredictionSummaryResponse[]>(initialPredictions);
  console.log(predictions);
  const selectedTopicRef = useRef("All");

  async function handleTopicSelect(topicId: string) {
    if (topicId == selectedTopicRef.current) {
      return;
    }
    selectedTopicRef.current = topicId;
    const topicPredictions = topicId == "All" ? await getPredictions() : await getPredictionsByTopicId(topicId);
    setPredictions(topicPredictions);
  }

  async function handleStatusChange(status: string) {
    const isResolved = status === "Resolved";
    const statusPredictions = await getPredictions(isResolved);
    setPredictions(statusPredictions);
  }

  function handlePredictionAdd(prediction: PredictionSummaryResponse) {
    setPredictions((prev) => [...prev, prediction]);
    toast.success("Prediction added.");
  }

  function handlePredictionChange(prediction: PredictionSummaryResponse) {
    setPredictions((prev) => prev.map((p) => (p.predictionId === prediction.predictionId ? prediction : p)));
  }

  async function handlePredictionDelete(predictionId: string) {
    try {
      await deletePredictionById(predictionId);
      setPredictions((prev) => prev.filter((p) => p.predictionId !== predictionId));
      toast.success("Prediction deleted!");
    } catch (error) {
      toast.error("Failed to delete prediction.");
    }
  }
  const statusFilters = ["Active", "Resolved"];
  const sortingFilters = ["Popular", "Ending Soon", "New"];

  return (
    <section className="max-w-[1400px] w-[95%] mx-auto py-6">
      <div className="flex justify-end mb-5 gap-2 ">
        <SelectBox label="Sort By:" data={sortingFilters} />
        <SelectBox label="Status:" data={statusFilters} onChange={handleStatusChange} />
        <Combobox data={topics} onChange={handleTopicSelect} />
        {auth.user && auth.user.role === "admin" ? <NewPredictionButton onAdd={handlePredictionAdd} /> : ""}
      </div>
      <PredictionList
        user={auth.user}
        predictions={predictions}
        handlePredictionChange={handlePredictionChange}
        handlePredictionDelete={handlePredictionDelete}
      />
    </section>
  );
}
