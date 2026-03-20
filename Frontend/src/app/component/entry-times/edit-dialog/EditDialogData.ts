import {Observable} from 'rxjs';
import {ITimeEntryFull} from '../../../core/models/timeEntry.model';
import {ITeam} from '../../../core/models/team.model';
import {Product} from '../../../core/models/product.model';
import {Activity} from '../../../core/models/activity.model';
import {WorkloadOption} from '../../../core/models/workload.model';
import {Module} from '../../../core/models/module.model';

export interface EditDialogData {
  timeEntry: ITimeEntryFull;
  teams$: Observable<ITeam[]>;
  products$: Observable<Product[]>;
   modules$: Observable<Module[]>;
  activities$: Observable<Activity[]>;
  workloads$: Observable<WorkloadOption[]>;
}
