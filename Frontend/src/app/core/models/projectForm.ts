import {IStep} from './step.model';
import {StatusKey} from './status.model';
import {IProject} from './project.model';

export interface ProjectForm {
  label: string
  description: string;
  stepList?: IStep[];
  startDate: Date;
  endDate?: Date;
  status: StatusKey;
}

export type ProjectDialogData =
  | { mode: 'create' }
  | { mode: 'edit'; project: IProject };
