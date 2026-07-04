export type StatusKey = 'InProgress' | 'Finished' | 'Cancelled' | 'OnABreak' | 'NotStarted';

export const STATUS_COLOR_MAP: Record<StatusKey, string> = {
  InProgress:  'inProgress',
  Finished:    'finished',
  Cancelled:   'cancelled',
  OnABreak:    'onABreak',
  NotStarted:  'notStarted',
};
