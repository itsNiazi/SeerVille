import { Combobox } from "@/components/combo-box";
import { Badge } from "@/components/ui/badge";
import { Button } from "@/components/ui/button";
import { Card, CardAction, CardHeader, CardTitle } from "@/components/ui/card";
import { IconTrendingUp } from "@tabler/icons-react";
import { createFileRoute, useLoaderData } from "@tanstack/react-router";
import { useRef, useState } from "react";
import { getPredictions, getPredictionsByTopicId } from "@/api/internal/predictions/prediction.api";
import { getTopics } from "@/api/internal/topics/topic.api";

export const Route = createFileRoute("/auth/predictions")({
  loader: async () => {
    const [predictions, topics] = await Promise.all([getPredictions(), getTopics()]);
    return { predictions, topics };
  },
  component: PredictionsComponent,
});

function PredictionsComponent() {
  const { predictions: initialPredictions, topics } = useLoaderData({ route: Route });
  const [predictions, setPredictions] = useState(initialPredictions);
  const selectedTopicRef = useRef("All");

  async function handleTopicSelect(topicId: string) {
    if (topicId == selectedTopicRef.current) {
      return;
    }
    selectedTopicRef.current = topicId;
    const newPredictions = topicId == "All" ? await getPredictions() : await getPredictionsByTopicId(topicId);
    setPredictions(newPredictions);
  }

  return (
    <>
      <Combobox data={topics} onSelectTopic={handleTopicSelect} />
      <div className="*:data-[slot=card]:from-primary/5 *:data-[slot=card]:to-card dark:*:data-[slot=card]:bg-card grid grid-cols-1 gap-4 px-4 *:data-[slot=card]:bg-gradient-to-t *:data-[slot=card]:shadow-xs lg:px-6 @xl/main:grid-cols-2 @5xl/main:grid-cols-4">
        {predictions && predictions.length > 0 ? (
          predictions.map((prediction) => (
            <Card className="@container/card" key={prediction.predictionId}>
              <CardHeader>
                <CardTitle className="text-2xl font-semibold tabular-nums @[250px]/card:text-xl">
                  {prediction.predictionName}
                </CardTitle>
                <CardAction>
                  <Badge variant="outline">
                    <IconTrendingUp />
                    New!
                  </Badge>
                </CardAction>
              </CardHeader>
              <div className="flex justify-center gap-5">
                <Button className="w-30">Yes</Button>
                <Button className="w-30">No</Button>
              </div>
            </Card>
          ))
        ) : (
          <p>error</p>
        )}
      </div>
    </>
  );
}
