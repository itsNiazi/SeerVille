interface Assert {
  isString: (value: string, name?: string) => void;
  isNumber: (value: number, name?: string) => void;
  isBoolean: (value: boolean, name?: string) => void;
  isArray: (value: Array<any>, name?: string) => void;
  isObject: (value: object, name?: string) => void;
  isNotEmptyOrNull: (value: string, name?: string) => void;
  isValidDate: (value: string, name?: string) => void;
}

export const assert: Assert = {
  isString: (value, name = "value") => {
    if (typeof value != "string") {
      throw new TypeError(`${name} must be a string, got ${value}`);
    }
  },
  isNumber: (value, name = "value") => {
    if (typeof value !== "number" || isNaN(value)) {
      throw new TypeError(`${name} must be a valid number, got ${typeof value}`);
    }
  },
  isBoolean: (value, name = "value") => {
    if (typeof value !== "boolean") {
      throw new TypeError(`${name} must be boolean, got ${value}`);
    }
  },
  isArray: (value, name = "value") => {
    if (!Array.isArray(value)) {
      throw new TypeError(`${name} must be an array, got ${typeof value}`);
    }
  },
  isObject: (value, name = "value") => {
    if (typeof value !== "object" || value == null || Array.isArray(value)) {
      throw new TypeError(`${name} must be an object, got ${typeof value}`);
    }
  },
  isNotEmptyOrNull: (value, name = "value") => {
    if (typeof value === "string" && value.trim() == "") {
      throw new Error(`${name} cannot be empty`);
    }

    if (value == null || value == undefined) {
      throw new Error(`${name} cannot be null or undefined`);
    }
  },

  isValidDate: (value, name = "date") => {
    const date = new Date(value);
    if (isNaN(date.getTime())) {
      throw new Error(`${name} is not a valid date: "${value}"`);
    }
  },
};
