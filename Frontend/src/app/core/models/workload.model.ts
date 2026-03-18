export type Workload = 1 | 0.75 | 0.5 | 0.25 | 0;

export interface WorkloadOption {
  value: Workload;
  label: string
}

export const WORKLOAD_OPTIONS: readonly WorkloadOption[] = [
  { value: 0.25, label: '0,25'  },
  { value: 0.5,  label: '0,50'  },
  { value: 0.75, label: '0,75'  },
  { value: 1,    label: '1' }
] as const;

