import { IconPencilPlus } from "@tabler/icons-react";
import { Button } from "../../ui/button";
import { useState } from "react";
import { Dialog, DialogClose, DialogContent, DialogFooter, DialogHeader, DialogTitle } from "../../ui/dialog";
import { Input } from "../../ui/input";
import { Label } from "../../ui/label";
import {
  CreatePredictionRequestSchema,
  type CreatePredictionRequest,
  type PredictionResponse,
} from "@/api/internal/predictions/prediction.schema";
import { createPrediction } from "@/api/internal/predictions/prediction.api";
import { convertToUtcIso } from "@/lib/date";
import { toast } from "sonner";

interface NewPredictionBtnProps {
  onAdd: (prediction: PredictionResponse) => void;
}
export function NewPredictionButton({ onAdd }: NewPredictionBtnProps) {
  const [open, setOpen] = useState(false);
  return (
    <>
      <Button className="max-w-30" onClick={() => setOpen(true)}>
        New <IconPencilPlus />
      </Button>
      <NewPredictionDialog open={open} onClose={() => setOpen(false)} onAdd={onAdd} />
    </>
  );
}

interface NewPredictionDialogProps {
  open: boolean;
  onClose: () => void;
  onAdd: (prediction: PredictionResponse) => void;
}

export function NewPredictionDialog({ open, onClose, onAdd }: NewPredictionDialogProps) {
  const [isSubmitting, setIsSubmitting] = useState(false);
  async function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
    try {
      e.preventDefault();
      setIsSubmitting(true);
      const formData = new FormData(e.currentTarget);
      const topicId = formData.get("topicId")?.toString().trim() ?? "";
      const predictionName = formData.get("predictionName")?.toString().trim() ?? "";
      const dateInput = formData.get("resolutionDate")?.toString();

      if (!dateInput) {
        return;
      }

      let resolutionDate = convertToUtcIso(dateInput);
      if (Date.now() > new Date(resolutionDate).getTime()) {
        toast.warning("Please provide a future resolution date");
        return;
      }

      const createRequest: CreatePredictionRequest = {
        topicId,
        predictionName,
        resolutionDate,
      };

      const result = CreatePredictionRequestSchema.safeParse(createRequest);
      if (result.error) {
        toast.warning(result.error.issues[0].message);
        return;
      }

      const response = await createPrediction(createRequest);
      if (response.error) {
        toast.warning("Unexpected error occurred");
        return;
      }

      // Had to ensure the votes are included, since that endpoint doesn't send them [TEMP]
      const newPrediction = {
        ...response,
        yesVotes: response.yesVotes ?? 0,
        noVotes: response.noVotes ?? 0,
      };

      onAdd(newPrediction);
      onClose();
    } catch (error) {
      console.log(error);
      toast.warning("Unexpected error occurred");
    } finally {
      setIsSubmitting(false);
    }
  }
  return (
    <Dialog open={open} onOpenChange={(val) => !val && onClose()}>
      <DialogContent>
        <DialogHeader>
          <DialogTitle className="text-3xl text-center">Create Prediction</DialogTitle>
        </DialogHeader>
        <form className="flex flex-col gap-4" onSubmit={handleSubmit}>
          <Label className="text-md justify-center">Topic ID</Label>
          <Input name="topicId" type={"text"} required />
          <Label className="text-md justify-center">Prediction Name</Label>
          <Input name="predictionName" type={"text"} required />
          <Label className="text-md justify-center">Resolution Date</Label>
          <Input className="text-center" name="resolutionDate" type={"date"} required />
          <DialogFooter>
            <Button type="submit" disabled={isSubmitting}>
              Create
            </Button>
            <DialogClose asChild>
              <Button variant="outline" disabled={isSubmitting}>
                Cancel
              </Button>
            </DialogClose>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
}
