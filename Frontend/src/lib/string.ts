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
