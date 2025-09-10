import { flexRender, getCoreRowModel, useReactTable } from "@tanstack/react-table";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";
import type { ColumnDef } from "@tanstack/react-table";
import { Badge } from "@/components/ui/badge";
import { IconCircleCheckFilled, IconLoader } from "@tabler/icons-react";
import { ConvertToYMD } from "@/lib/date";
import type { UserPredictionResponse } from "@/api/internal/users/user.schema";

interface DataTableProps<TData, TValue> {
  columns: ColumnDef<TData, TValue>[];
  data: TData[];
}

export const columns: ColumnDef<UserPredictionResponse>[] = [
  {
    accessorKey: "predictionName",
    header: "Prediction",
    cell: ({ row }) => (
      <div className="w-32 hover:cursor-pointer hover:underline">
        <a className="px-1.5">{row.original.predictionName}</a>
      </div>
    ),
  },
  {
    accessorKey: "topicName",
    header: "Topic",
    cell: ({ row }) => (
      <div className="w-32">
        <Badge variant="outline" className="text-muted-foreground px-1.5">
          {row.original.topicName}
        </Badge>
      </div>
    ),
  },
  {
    accessorKey: "isResolved",
    header: "Status",
    cell: ({ row }) => (
      <div className="w-32">
        <Badge variant="outline" className="text-muted-foreground px-1.5">
          {row.original.isResolved === true ? (
            <>
              <IconCircleCheckFilled className="fill-green-500 dark:fill-green-400" />
              <span>Done</span>
            </>
          ) : (
            <>
              <IconLoader />
              <span>Unresolved</span>
            </>
          )}
        </Badge>
      </div>
    ),
  },
  {
    accessorKey: "votedDate",
    header: "Vote Date",
    cell: ({ row }) => (
      <div className="w-32 text-muted-foreground">
        <a className="px-1.5">{ConvertToYMD(row.original.votedDate)}</a>
      </div>
    ),
  },
];

export function PredictionTable<TData, TValue>({ columns, data }: DataTableProps<TData, TValue>) {
  const table = useReactTable({
    data,
    columns,
    getCoreRowModel: getCoreRowModel(),
  });

  return (
    <div className="overflow-hidden rounded-md border">
      <Table>
        <TableHeader className="bg-card">
          {table.getHeaderGroups().map((headerGroup) => (
            <TableRow className="hover:bg-transparent" key={headerGroup.id}>
              {headerGroup.headers.map((header) => {
                return (
                  <TableHead className="px-5" key={header.id}>
                    {header.isPlaceholder ? null : flexRender(header.column.columnDef.header, header.getContext())}
                  </TableHead>
                );
              })}
            </TableRow>
          ))}
        </TableHeader>
        <TableBody>
          {table.getRowModel().rows?.length ? (
            table.getRowModel().rows.map((row) => (
              <TableRow key={row.id} data-state={row.getIsSelected() && "selected"}>
                {row.getVisibleCells().map((cell) => (
                  <TableCell className="px-5 truncate hover:text-amber-300" key={cell.id}>
                    {flexRender(cell.column.columnDef.cell, cell.getContext())}
                  </TableCell>
                ))}
              </TableRow>
            ))
          ) : (
            <TableRow>
              <TableCell colSpan={columns.length} className="h-24 text-muted-foreground text-center">
                {"No predictions \u203C\uFE0F"}
              </TableCell>
            </TableRow>
          )}
        </TableBody>
      </Table>
    </div>
  );
}
