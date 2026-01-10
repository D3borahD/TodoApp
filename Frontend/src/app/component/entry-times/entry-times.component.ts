import {Component, inject} from '@angular/core';
import {TeamService} from '../../core/services/team.service';
import {FormBuilder, FormsModule, ReactiveFormsModule, Validators,} from '@angular/forms';
import {MatOptionModule} from '@angular/material/core';
import {MatFormField, MatLabel} from '@angular/material/form-field';
import {MatSelect} from '@angular/material/select';
import {AsyncPipe} from '@angular/common';
import {ProductService} from '../../core/services/product.service';
import {ModuleService} from '../../core/services/module.service';
import {ActivityService} from '../../core/services/activity.service';
import {ITimeEntry} from '../../core/models/timeEntry.model';
import {of} from 'rxjs';
import {Workload, WORKLOAD_OPTIONS} from '../../core/models/workload.model';
import {TimeEntryService} from '../../core/services/timeEntry.service';

@Component({
  selector: 'app-entry-times',
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule,
    AsyncPipe,
    MatFormField,
    MatSelect,
    MatLabel,
    MatOptionModule,
  ],
  templateUrl: './entry-times.component.html',
  styleUrl: './entry-times.component.scss'
})
export class EntryTimesComponent {
  private teamService: TeamService = inject(TeamService);
  private productService: ProductService = inject(ProductService);
  private moduleService: ModuleService = inject(ModuleService);
  private activityService: ActivityService = inject(ActivityService);
  private formBuilder: FormBuilder = inject(FormBuilder);
  private timeEntryService: TimeEntryService = inject(TimeEntryService);

  public readonly workloads$ = of(WORKLOAD_OPTIONS)

  public teams$ = this.teamService.getTeams$();
  public products$ = this.productService.getProducts$();
  public modules$ = this.moduleService.getModules$();
  public activities$ = this.activityService.getActivities$();
  public entryTimes!: ITimeEntry;

  public entryTimesForm = this.formBuilder.group({
    teamId: [0],
    workDate: this.formBuilder.control<Date | null>(new Date(), [Validators.required]),
    workload: this.formBuilder.control<Workload | null>(null, Validators.required),
    productId: [0, [Validators.required]],
    moduleId: [0, [Validators.required]],
    activityId: [0, [Validators.required]],
    specificProjectId: [0],
    comment: [''],
  })

  onSubmit() {
    const formValue = this.entryTimesForm.getRawValue();

    if (!formValue.workDate || !formValue.workload) {
      return;
    }

    const entry: ITimeEntry = {
      userId: 1,
      teamId: formValue.teamId,
      workDate: new Date(formValue.workDate).toISOString(),
      workload: formValue.workload,
      productId: formValue.productId,
      moduleId: formValue.moduleId,
      activityId: formValue.activityId,
      specificProjectId: formValue.specificProjectId,
      comment: formValue.comment,
    };

    this.entryTimes = entry;

    this.timeEntryService.addTimeEntry(entry).subscribe({
      next: res => console.log('Réponse API:', res),
      error: err => console.error('Erreur API:', err)
    });
  }
}
