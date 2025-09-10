import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "../ui/card";

interface CardProps {
  label: string;
  description?: string;
  data?: string | number;
  children?: React.ReactNode;
}
export function BaseCard({ label, description, data, children }: CardProps) {
  return (
    <>
      <Card className="hover:shadow-lg transition">
        <CardHeader className="flex items-center space-x-2">
          <CardTitle>{label}</CardTitle>
        </CardHeader>
        <CardContent>
          {data && <p className="text-2xl font-semibold">{data}</p>}
          {children && <div>{children}</div>}
          {description && <CardDescription>{description}</CardDescription>}
        </CardContent>
      </Card>
    </>
  );
}
