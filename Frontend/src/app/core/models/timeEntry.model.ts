import {ITeam} from './team.model';
import {Product} from './product.model';
import {Module} from './module.model';
import {Activity} from './activity.model';
import {Workload} from './workload.model';

export interface ITimeEntry{
  id?: number
  userId: number
  workDate: string | null
  workload: Workload | null
  activityId: number | null
  teamId: number | null
  productId: number | null
  moduleId: number | null
  specificProjectId: number | null
  comment: string | null
}

export interface ITimeEntryFull{
  id: number
  userId: number
  workDate: string
  workload: Workload
  activity: Activity
  team: ITeam
  product: Product
  module: Module
  specificProjectId: number
  comment: string
}
