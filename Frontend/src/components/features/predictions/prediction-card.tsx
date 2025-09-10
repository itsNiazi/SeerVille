// React
import { useState } from "react";

// Internal APIs & Schemas
import { createPredictionVote } from "@/api/internal/predictions/predictionVote.api";
import {
  PatchPredictionRequestSchema,
  UpdatePredictionRequestSchema,
  type PatchPredictionRequest,
  type PredictionSummaryResponse,
  type UpdatePredictionRequest,
} from "@/api/internal/predictions/prediction.schema";
import { patchPredictionById, updatePredictionById } from "@/api/internal/predictions/prediction.api";

// Utilities
import { abbreviateCount } from "@/lib/string";
import { convertToLocalDate, convertToUtcIso } from "@/lib/date";

// UI Components
import { Label } from "../../ui/label";
import { Input } from "@/components/ui/input";
import { RadioGroup, RadioGroupItem } from "../../ui/radio-group";
import { Button } from "../../ui/button";
import {
  Dialog,
  DialogClose,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from "../../ui/dialog";
import { Breadcrumb, BreadcrumbItem, BreadcrumbList } from "../../ui/breadcrumb";
import { Card, CardAction, CardHeader, CardTitle } from "../../ui/card";
import { DropdownMenu, DropdownMenuContent, DropdownMenuItem, DropdownMenuTrigger } from "../../ui/dropdown-menu";
import {
  Drawer,
  DrawerContent,
  DrawerDescription,
  DrawerHeader,
  DrawerTitle,
  DrawerTrigger,
} from "@/components/ui/drawer";
import { ChartContainer, type ChartConfig } from "@/components/ui/chart";
import { Label as RechartsLabel, PolarRadiusAxis, RadialBar, RadialBarChart } from "recharts";
import { IconArchiveFilled, IconArrowDown, IconArrowUp, IconCheckbox, IconEdit, IconTrash } from "@tabler/icons-react";
import { toast } from "sonner";
import { CreatePredictionVoteRequestSchema } from "@/api/internal/predictions/predictionVote.schema";

interface PredictionDialogProps {
  prediction: PredictionSummaryResponse | null;
  open: boolean;
  onChange: (prediction: PredictionSummaryResponse) => void;
  onClose: () => void;
}

export function EditDialog({ prediction, open, onChange, onClose }: PredictionDialogProps) {
  const [isSubmitting, setIsSubmitting] = useState(false);

  async function onFormSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    setIsSubmitting(true);

    try {
      const formData = new FormData(event.currentTarget);
      const predictionName = formData.get("predictionName")?.toString().trim();
      const dateInput = formData.get("resolutionDate")?.toString();

      if (prediction?.isResolved) {
        toast.warning("Prediction is already resolved");
        return;
      }

      if (!predictionName || !dateInput) {
        toast.warning("Please provide a prediction name & resolution date");
        return;
      }

      if (predictionName === prediction?.predictionName) {
        toast.warning("Please provide a new prediction name");
        return;
      }

      const resolutionDate = convertToUtcIso(dateInput);

      if (new Date(resolutionDate).getTime() < Date.now()) {
        toast.warning("Please provide a future resolution date");
        return;
      }

      const updateRequest: UpdatePredictionRequest = {
        predictionName: predictionName,
        resolutionDate: resolutionDate,
      };

      const result = UpdatePredictionRequestSchema.safeParse(updateRequest);
      if (result.error) {
        toast.warning(result.error.issues[0].message);
        return;
      }

      const response = await updatePredictionById(prediction.predictionId, updateRequest);
      if (response.error) {
        toast.warning("Unexpected error occurred while updating prediction");
        return;
      }
      onChange({
        ...prediction!,
        predictionName,
        resolutionDate,
      });
      onClose();
      toast.success("Prediction updated");
    } catch (error) {
      toast.warning("Unexpected error occurred");
    } finally {
      setIsSubmitting(false);
    }
  }
  return (
    <Dialog open={open} onOpenChange={(val) => !val && onClose()}>
      <DialogContent>
        <DialogHeader>
          <DialogTitle className="text-3xl text-center">Edit Prediction</DialogTitle>
        </DialogHeader>
        <form className="flex flex-col gap-4" onSubmit={onFormSubmit}>
          <Label className="text-md justify-center">Prediction Name</Label>
          <Input
            name="predictionName"
            type={"text"}
            defaultValue={prediction?.predictionName}
            placeholder={prediction?.predictionName}
            required
          />
          <Label className="text-md justify-center">Resolution Date</Label>
          <Input className="text-center" name="resolutionDate" type={"date"} required />
          <DialogFooter>
            <Button type="submit" disabled={isSubmitting}>
              Update
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

export function ResolveDialog({ prediction, open, onChange, onClose }: PredictionDialogProps) {
  const [isSubmitting, setIsSubmitting] = useState(false);

  async function onFormSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    setIsSubmitting(true);
    try {
      const formData = new FormData(event.currentTarget);
      const isResolved = formData.get("isResolved")?.toString();
      const isCorrect = formData.get("isCorrect")?.toString();

      if (!isResolved || !isCorrect) {
        return;
      }

      const resolvedBool = isResolved === "Yes";
      const correctBool = isCorrect === "Yes";

      const patchRequest: PatchPredictionRequest = { isResolved: resolvedBool, isCorrect: correctBool };
      const result = PatchPredictionRequestSchema.safeParse(patchRequest);
      if (result.error) {
        toast.warning(result.error.issues[0].message);
        return;
      }

      const response = await patchPredictionById(prediction.predictionId, patchRequest);
      if (response.error) {
        toast.warning("Unexpected error occurred");
        return;
      }

      onChange({
        ...prediction!,
        isResolved: resolvedBool,
        isCorrect: correctBool,
      });
      onClose();
      toast.success("Prediction resolved");
    } catch (error) {
      toast.warning("Unexpected error occurred");
    } finally {
      setIsSubmitting(false);
    }
  }

  return (
    <Dialog open={open} onOpenChange={(val) => !val && onClose()}>
      <DialogContent>
        <form onSubmit={onFormSubmit}>
          <DialogHeader>
            <DialogTitle className="text-3xl text-center">Resolve Prediction</DialogTitle>
            <DialogDescription className="text-lg text-center">
              {prediction?.predictionName ? `💭Resolving "${prediction?.predictionName}"` : null}
            </DialogDescription>
            <div className="flex justify-evenly my-8">
              <RadioGroup defaultValue={prediction?.isResolved ? "Yes" : "No"} name="isResolved">
                <Label className="text-md">Resolved</Label>
                <div className="flex items-center space-x-2">
                  <RadioGroupItem value="Yes" id="option-one" />
                  <Label htmlFor="Yes">✅</Label>
                </div>
                <div className="flex items-center space-x-2">
                  <RadioGroupItem value="No" id="option-two" />
                  <Label htmlFor="No">❌</Label>
                </div>
              </RadioGroup>
              <RadioGroup defaultValue={prediction?.isCorrect ? "Yes" : "No"} name="isCorrect">
                <Label className="text-md">Correct</Label>
                <div className="flex items-center space-x-2">
                  <RadioGroupItem value="Yes" id="option-one" />
                  <Label htmlFor="Yes">✅</Label>
                </div>
                <div className="flex items-center space-x-2">
                  <RadioGroupItem value="No" id="option-two" />
                  <Label htmlFor="No">❌</Label>
                </div>
              </RadioGroup>
            </div>
          </DialogHeader>
          <DialogFooter className="sm:justify-center">
            <Button type="submit" disabled={isSubmitting}>
              Save changes
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

interface PredictionCardProps {
  isAdmin?: boolean;
  prediction: PredictionSummaryResponse;
  onDelete: (predictionId: string) => void;
  onResolve: (prediction: PredictionSummaryResponse) => void;
  onEdit: (prediction: PredictionSummaryResponse) => void;
  onVote: (prediction: PredictionSummaryResponse) => void;
}

export function PredictionCard({ isAdmin, prediction, onDelete, onResolve, onEdit, onVote }: PredictionCardProps) {
  const [isSubmitting, setIsSubmitting] = useState(false);
  const resolutionTimestamp = new Date(prediction.resolutionDate).getTime();
  const tooLate = resolutionTimestamp < Date.now() || prediction.isResolved;
  const hasVoted = prediction.predictedOutcome != null;
  const totalVotes = prediction.totalVotes;
  const isDisabled = tooLate || isSubmitting || hasVoted;
  const voteCorrect = prediction.predictedOutcome === prediction.isCorrect;
  const isResolved = prediction.isResolved;

  function renderVoteUI() {
    if (isResolved && hasVoted) {
      return (
        <div className="text-sm font-medium text-center">
          {voteCorrect ? (
            <span className="text-chart-1">🎉 Your vote was correct!</span>
          ) : (
            <span className="text-chart-2">❌ Your vote was incorrect</span>
          )}
          <div className="text-muted-foreground text-xs mt-1">Overall: {getResolutionText()}</div>
        </div>
      );
    }

    if (isResolved && !hasVoted) {
      return (
        <div className="text-muted-foreground text-xs text-center">
          <p className="text-sm">Prediction is resolved</p>
          Overall: {getResolutionText()}
        </div>
      );
    }

    if (tooLate) {
      return <p className="text-sm text-muted-foreground italic">Voting closed</p>;
    }

    if (hasVoted) {
      return (
        <p className="text-sm text-foreground">
          You voted{" "}
          <span className={prediction.predictedOutcome ? "text-chart-1" : "text-chart-2"}>
            {prediction.predictedOutcome ? "Yes" : "No"}
          </span>
        </p>
      );
    }

    // Default case (Voting buttons)
    return (
      <>
        <Button
          className="w-[35%]"
          variant={"outline"}
          disabled={isDisabled}
          onClick={() => handlePredictionVote(true)}
        >
          Yes
        </Button>
        <Button
          className="w-[35%] hover:bg-[var(--chart-2)]/70"
          variant={"outline"}
          disabled={isDisabled}
          onClick={() => handlePredictionVote(false)}
        >
          No
        </Button>
      </>
    );
  }

  function getResolutionText() {
    if (prediction.isCorrect === true) return "Prediction was correct";
    if (prediction.isCorrect === false) return "Prediction was incorrect";
    return "Prediction resolved";
  }

  async function handlePredictionVote(value: boolean) {
    try {
      setIsSubmitting(true);

      if (typeof value !== "boolean") {
        return;
      }

      if (tooLate) {
        toast.warning("Prediction time ended!");
        return;
      }

      if (prediction.isResolved) {
        toast.warning("Prediction is already resolved!");
        return;
      }

      const result = CreatePredictionVoteRequestSchema.safeParse({
        predictionId: prediction.predictionId,
        predictedOutcome: value,
      });

      if (result.error) {
        toast.warning(result.error.issues[0].message);
        return;
      }

      const response = await createPredictionVote({
        predictionId: prediction.predictionId,
        predictedOutcome: value,
      });

      if (response.error) {
        toast.warning("Unexpected error occurred");
        return;
      }

      const updatedPrediction = {
        ...prediction,
        yesVotes: value ? prediction.yesVotes + 1 : prediction.yesVotes,
        noVotes: !value ? prediction.noVotes + 1 : prediction.noVotes,
        totalVotes: prediction.totalVotes + 1,
        predictedOutcome: value,
      };

      onVote(updatedPrediction);
    } catch {
      toast.warning("Unexpected error occurred");
    } finally {
      setIsSubmitting(false);
    }
  }
  return (
    <>
      <Card
        className="@container/card w-xs max-h-45 py-3 gap-3  justify-around overflow-hidden hover:shadow-lg hover:bg-card/90"
        key={prediction.predictionId}
      >
        <CardHeader>
          <div className="flex justify-between max-h-[60px] items-center">
            <PredictionDrawer prediction={prediction} />
            <ChartRadialStacked prediction={prediction} />
          </div>
          <CardAction>
            {isAdmin ? (
              <Breadcrumb>
                <BreadcrumbList>
                  <BreadcrumbItem>
                    <DropdownMenu modal={false}>
                      <DropdownMenuTrigger>...</DropdownMenuTrigger>
                      <DropdownMenuContent align="start" side="right">
                        {!prediction.isResolved && (
                          <DropdownMenuItem className="flex justify-between" onClick={() => onEdit(prediction)}>
                            Edit
                            <IconEdit />
                          </DropdownMenuItem>
                        )}
                        <DropdownMenuItem className="flex justify-between" onClick={() => onResolve(prediction)}>
                          Resolve <IconCheckbox />
                        </DropdownMenuItem>

                        <DropdownMenuItem
                          className="flex justify-between"
                          onClick={() => onDelete(prediction.predictionId)}
                        >
                          Delete <IconTrash className="text-[var(--destructive)]" />
                        </DropdownMenuItem>
                      </DropdownMenuContent>
                    </DropdownMenu>
                  </BreadcrumbItem>
                </BreadcrumbList>
              </Breadcrumb>
            ) : (
              ""
            )}
          </CardAction>
        </CardHeader>
        <div className="flex justify-center gap-5 max-h-[2.25rem] min-h-[2.25rem]">{renderVoteUI()}</div>
        <div className="flex justify-between mx-8">
          <p className="text-xs text-muted-foreground/70 text-center">
            {convertToLocalDate(prediction.resolutionDate)}
          </p>
          <p className=" flex text-xs text-amber-300 text-center gap-1 items-center">
            <IconArchiveFilled size={12} />
            {totalVotes >= 0 ? abbreviateCount(totalVotes) : "0"} {totalVotes === 1 ? "Vote" : "Votes"}
          </p>
        </div>
      </Card>
    </>
  );
}

const chartConfig = {
  yes: {
    label: "Yes",
    color: "var(--chart-4)",
  },
  no: {
    label: "No",
    color: "var(--chart-2)",
  },
} satisfies ChartConfig;

export function ChartRadialStacked({ prediction }) {
  const totalVotes = prediction.totalVotes;
  const hasVotes = totalVotes > 0;
  const chartData = [{ yes: hasVotes ? prediction.yesVotes : 1, no: hasVotes ? prediction.noVotes : 0 }];

  const yesColor = hasVotes ? "var(--chart-1)" : "var(--muted)";
  const noColor = hasVotes ? "var(--chart-2)" : "var(--muted)";

  return (
    <div className="w-[80px] h-[80px] shrink-0">
      <ChartContainer config={chartConfig} className="w-full h-full">
        <RadialBarChart data={chartData} startAngle={180} endAngle={0} innerRadius="70%" outerRadius="100%">
          <PolarRadiusAxis tick={false} tickLine={false} axisLine={false}>
            <RechartsLabel
              content={({ viewBox }) => {
                if (!viewBox || !("cx" in viewBox) || !("cy" in viewBox)) return null;
                const { cx, cy } = viewBox;
                return (
                  <text x={cx} y={cy} textAnchor="middle">
                    <tspan x={cx} dy="0" className="fill-foreground text-[12px] font-bold">
                      {totalVotes
                        ? totalVotes === 0
                          ? "-"
                          : `${((prediction.yesVotes / totalVotes) * 100).toFixed(0)}%`
                        : "-"}
                    </tspan>
                    <tspan x={cx} dy="14" className="fill-muted-foreground text-xs">
                      chance
                    </tspan>
                  </text>
                );
              }}
            />
          </PolarRadiusAxis>

          <RadialBar dataKey="yes" stackId="a" cornerRadius={3} fill={yesColor} isAnimationActive={false} />
          <RadialBar dataKey="no" stackId="a" cornerRadius={3} fill={noColor} isAnimationActive={false} />
        </RadialBarChart>
      </ChartContainer>
    </div>
  );
}

export function PredictionDrawer({ prediction }) {
  const totalVotes = abbreviateCount(prediction.totalVotes);
  const yesVotes = abbreviateCount(prediction.yesVotes);
  const noVotes = abbreviateCount(prediction.noVotes);

  return (
    <Drawer>
      <DrawerTrigger>
        <CardTitle className="tabular-nums text-[15px] leading-6 hover:underline cursor-pointer max-w-45 max-h-[48px] overflow-hidden line-clamp-2">
          {prediction.predictionName}
        </CardTitle>
      </DrawerTrigger>
      <DrawerContent className="h-1/3">
        <DrawerHeader className="mx-auto">
          <DrawerTitle>{prediction.predictionName}</DrawerTitle>
          <DrawerDescription>{convertToLocalDate(prediction.resolutionDate)}</DrawerDescription>
          <p className="text-xl text-amber-300">🗳️ {totalVotes}</p>
          <div className="flex justify-center items-center text-muted-foreground">
            {yesVotes}
            <IconArrowUp color="oklch(0.588 0.0993 245.7394)" />
            <IconArrowDown color="#ff637e" />
            {noVotes}
          </div>
        </DrawerHeader>
      </DrawerContent>
    </Drawer>
  );
}
