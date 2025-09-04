export const toastOptions = {
  style: {
    background: "var(--card)",
    color: "var(--card-foreground)",
    borderColor: "var(--muted)",
  },
};

export const customToastOptions = {
  classNames: {
    toast: "toast",
    title: "title",
    description: "description", //need to add these classes
    actionButton: "action-button",
    cancelButton: "cancel-button",
    closeButton: "close-button",
  },
};
