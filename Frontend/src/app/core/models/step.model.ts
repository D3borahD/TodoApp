import {IType} from './type.model';
import {StatusKey} from './status.model';

export interface  IStep {
  id: number;
  label: string;
  description: string;
  rank?: number | null;
  startDate?: Date;
  endDate?: Date;
  type:IType;
  status:StatusKey;
  duration: number;
  projectId:number;
}
