import { useState } from "react";
import { EditDialog, PredictionCard, ResolveDialog } from "./prediction-card";
import type { UserResponse } from "@/api/internal/users/user.schema";
import type { PredictionResponse } from "@/api/internal/predictions/prediction.schema";

interface PredictionListProps {
  user: UserResponse | null; //?
  predictions: PredictionResponse[];
  handlePredictionChange: (prediction: PredictionResponse) => void;
  handlePredictionDelete: (predictionId: string) => void;
}

export function PredictionList({
  user,
  predictions,
  handlePredictionChange,
  handlePredictionDelete,
}: PredictionListProps) {
  const [resolveTarget, setResolveTarget] = useState<PredictionResponse | null>(null);
  const [editTarget, setEditTarget] = useState<PredictionResponse | null>(null);

  async function handleTargetResolve(value: PredictionResponse | null) {
    setResolveTarget(value);
  }
  async function handleTargetEdit(value: PredictionResponse | null) {
    setEditTarget(value);
  }

  return (
    <div className="flex flex-col justify-center items-center sm:flex-row flex-wrap gap-4">
      {predictions && predictions.length > 0 ? (
        predictions.map((prediction) => (
          <PredictionCard
            key={prediction.predictionId}
            isAdmin={user?.role === "admin"}
            prediction={prediction}
            onDelete={handlePredictionDelete}
            onResolve={handleTargetResolve}
            onEdit={handleTargetEdit}
            onVote={handlePredictionChange}
          />
        ))
      ) : (
        <p>No predictions found</p>
      )}
      <EditDialog
        prediction={editTarget}
        open={!!editTarget}
        onChange={handlePredictionChange}
        onClose={() => handleTargetEdit(null)}
      />
      <ResolveDialog
        prediction={resolveTarget}
        open={!!resolveTarget}
        onChange={handlePredictionChange}
        onClose={() => handleTargetResolve(null)}
      />
    </div>
  );
}
