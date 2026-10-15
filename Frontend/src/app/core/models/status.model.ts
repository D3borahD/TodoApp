import {ChipColor} from '../../ui/components/chips/chipColor';

export type StatusKey = keyof typeof STATUS_COLOR_MAP;

export const STATUS_COLOR_MAP = {
  InProgress:  'purple',
  Finished:    'green',
  Cancelled:   'red',
  OnABreak:    'orange',
  NotStarted:  'gray',
} as const satisfies Record<string, ChipColor>;

