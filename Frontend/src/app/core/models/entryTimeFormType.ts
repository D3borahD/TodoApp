import {FormControl} from '@angular/forms';
import {Workload} from './workload.model';

export type TimeEntryFormType = {
  activityId: FormControl<number | null>;
  comment: FormControl<string | null>;
  moduleId: FormControl<number | null>;
  productId: FormControl<number | null>;
  specificProjectId: FormControl<number | null>;
  teamId: FormControl<number | null>;
  workDate: FormControl<Date | null>;
  workload: FormControl<Workload | null>;
};
