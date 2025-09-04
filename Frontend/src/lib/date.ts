import { assert } from "./validation";

/** Converts calendar date into UTC (ISO) string: '2025-12-31T23:59:59.999Z'  */
export function convertToUtcIso(dateString: string): string {
  assert.isString(dateString);
  assert.isNotEmptyOrNull(dateString);
  assert.isValidDate(dateString);

  const [year, month, day] = dateString.split("-").map(Number);
  const utcEndOfDay = new Date(Date.UTC(year, month - 1, day, 23, 59, 59, 999));
  return utcEndOfDay.toISOString();
}

/** Converts "2025-12-31 23:59:59.999" to "2025-12-31T23:59:59.999Z" */
export function convertToIsoString(dateString: string): string {
  assert.isString(dateString);
  assert.isNotEmptyOrNull(dateString);
  assert.isValidDate(dateString);

  let isoString;
  dateString[dateString.length - 1] === "Z"
    ? (isoString = dateString.replace(" ", "T"))
    : (isoString = dateString.replace(" ", "T") + "Z");
  return isoString;
}

/** Returns 'Dec 29, 2025' */
export function convertToLocalDate(dateString: string) {
  assert.isString(dateString);
  assert.isNotEmptyOrNull(dateString);
  assert.isValidDate(dateString);

  let isoString = convertToIsoString(dateString);
  const utcDate = new Date(isoString).toLocaleDateString(undefined, {
    month: "short",
    day: "numeric",
    year: "numeric",
  });
  return utcDate;
}
