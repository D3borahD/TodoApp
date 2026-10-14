import {StatusKey} from './status.model';
import {IProjectType} from './projectType.model';

export interface  IStep {
  id: string;
  label: string;
  description: string;
  rank?: number | null;
  startDate?: Date;
  endDate?: Date;
  type:IProjectType;
  status:StatusKey;
  duration: number;
  projectId:number;
}
