import {IStatus} from './status.model';

export interface IProject {
  id: number
  label: string
  description: string;
  stepsList?: string[];
  startDate: Date;
  endDate?: Date;
  status: IStatus;
}
