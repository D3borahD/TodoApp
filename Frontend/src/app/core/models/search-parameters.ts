import {StatusKey} from './status.model';

export interface ISearchParameters {
  label?: string;
  startDate?: Date;
  endDate?: Date;
  status?: StatusKey;
  projectTypeId?: number;

  pageSize?: number;
  pageNumber?: number;
}
