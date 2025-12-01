export const StatusType = {
    Published: 1,
    Unpublished: 2,
} as const;

export type StatusType = typeof StatusType[keyof typeof StatusType];