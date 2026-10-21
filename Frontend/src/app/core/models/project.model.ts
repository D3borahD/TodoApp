import {StatusKey} from './status.model';
import {IStep} from './step.model';
import {IProjectType} from './projectType.model';

export interface IProject {
  id: number;
  label: string
  description: string;
  stepList?: IStep[];
  startDate: Date;
  endDate?: Date;
  status: StatusKey;
  projectTypes?: IProjectType[];
}
