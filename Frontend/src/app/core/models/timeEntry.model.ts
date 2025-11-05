import {Workload} from './workload.enum';

export interface ITimeEntry{
  id: number
  userId: number
  workDate: string
  workload: Workload
  activityId: number
  teamId: number
  productId: number
  moduleId: number
  specificProjectId: number
  comment: string
}
