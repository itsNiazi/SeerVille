import * as React from "react";
import { Check, ChevronsUpDown } from "lucide-react";

import { cn } from "@/lib/utils";
import { Button } from "@/components/ui/button";
import { Command, CommandEmpty, CommandGroup, CommandInput, CommandItem, CommandList } from "@/components/ui/command";
import { Popover, PopoverContent, PopoverTrigger } from "@/components/ui/popover";
import { SelectSeparator } from "./ui/select";

export function Combobox({ data, onSelectTopic }) {
  const [open, setOpen] = React.useState(false);
  const [value, setValue] = React.useState("");

  return (
    <Popover open={open} onOpenChange={setOpen}>
      <PopoverTrigger asChild>
        <Button variant="outline" role="combobox" aria-expanded={open} className="w-[200px] justify-between">
          {/* {value ? data.find((data) => data.name === value)?.name : "All"} */}
          {value === "All" || value === "" ? "All" : data.find((data) => data.name === value)?.name}
          <ChevronsUpDown className="opacity-50" />
        </Button>
      </PopoverTrigger>
      <PopoverContent className="w-[200px] p-0">
        <Command>
          <CommandInput placeholder="Topics" className="h-9" />
          <CommandList>
            <CommandEmpty>No topic found.</CommandEmpty>
            <CommandGroup>
              <CommandItem
                value="All"
                onSelect={(currentValue) => {
                  setValue(currentValue);
                  setOpen(false);
                  onSelectTopic("All");
                }}
              >
                All
                <Check className={cn("ml-auto", value === "All" ? "opacity-100" : "opacity-0")} />
              </CommandItem>

              <SelectSeparator />
              {data.map((data) => (
                <CommandItem
                  key={data.topicId}
                  value={data.name}
                  onSelect={(currentValue) => {
                    setValue(currentValue);
                    setOpen(false);
                    onSelectTopic(data.topicId);
                  }}
                >
                  {data.name}
                  <Check className={cn("ml-auto", value === data.name ? "opacity-100" : "opacity-0")} />
                </CommandItem>
              ))}
            </CommandGroup>
          </CommandList>
        </Command>
      </PopoverContent>
    </Popover>
  );
}
