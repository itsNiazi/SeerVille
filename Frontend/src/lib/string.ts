import { assert } from "./validation";

/** Returns a string with first letter capitalized */
export function capitalize(string: string) {
  assert.isString(string);
  assert.isNotEmptyOrNull(string);

  return string[0].toUpperCase() + string.slice(1);
}

export function abbreviateCount(value: number) {
  assert.isNumber(value);

  const formatted = new Intl.NumberFormat("en-US", {
    notation: "compact",
    compactDisplay: "short",
  }).format(value);

  return formatted;
}

/** Returns a URL-encoded query string from an object of parameters, filtering out empty/null values. */
export function buildSearchParams(params: Record<string, any>): string {
  const cleanedParams = Object.fromEntries(
    Object.entries(params).filter(([_, v]) => v !== undefined && v !== null && v !== "")
  );

  return new URLSearchParams(cleanedParams).toString();
}
