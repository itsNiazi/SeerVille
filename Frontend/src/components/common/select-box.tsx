import { useState } from "react";
import { Select, SelectContent, SelectGroup, SelectItem, SelectTrigger } from "../ui/select";

interface SelectBoxProps {
  label?: string;
  data: string[];
  defaultValue?: string;
  onChange?: (value: string) => void;
}
export function SelectBox({ label = "Select", data, defaultValue, onChange }: SelectBoxProps) {
  const [status, setStatus] = useState(defaultValue ?? data[0]);

  function handleChange(value: string) {
    setStatus(value);
    onChange?.(value);
  }
  return (
    <>
      <Select value={status} onValueChange={handleChange}>
        <SelectTrigger className="w-[180px]">
          <span className="flex gap-1 text-sm">
            <span className="text-muted-foreground">{label}</span>
            <span>{status}</span>
          </span>
        </SelectTrigger>
        <SelectContent>
          <SelectGroup>
            {data.map((item) => (
              <SelectItem key={item} value={item}>
                {item}
              </SelectItem>
            ))}
          </SelectGroup>
        </SelectContent>
      </Select>
    </>
  );
}
