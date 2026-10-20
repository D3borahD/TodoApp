import {StatusKey} from './status.model';
import {IProjectType} from './projectType.model';

export interface StepForm {
  id?: string;
  label: string;
  description: string;
  startDate: Date;
  endDate: Date;
  status: StatusKey;
  typeId: number | null;
  projectId: number;
}
