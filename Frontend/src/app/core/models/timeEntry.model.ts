import {Activity} from './activity.model';
import {Module} from './module.model';
import {Product} from './product.model';
import {ITeam} from './team.model';
import {Workload} from './workload.enum';

export interface ITimeEntry{
  id: number
  userId: number
  workDate: string
  workload: Workload
  activity: Activity
  team?: ITeam
  product?: Product
  module?: Module
  specificProjectId: number
  comment: string
}
