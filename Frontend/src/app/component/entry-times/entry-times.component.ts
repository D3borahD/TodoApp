import {Component, effect, inject, OnInit} from '@angular/core';
import {TeamService} from '../../core/services/team.service';
import {FormBuilder, FormsModule, ReactiveFormsModule, Validators,} from '@angular/forms';
import {MatOptionModule} from '@angular/material/core';
import {MatFormField, MatLabel} from '@angular/material/form-field';
import {MatSelect} from '@angular/material/select';
import {AsyncPipe} from '@angular/common';
import {ProductService} from '../../core/services/product.service';
import {ModuleService} from '../../core/services/module.service';
import {ActivityService} from '../../core/services/activity.service';
import {ITimeEntry, ITimeEntryFull} from '../../core/models/timeEntry.model';
import {Observable, of} from 'rxjs';
import {Workload, WORKLOAD_OPTIONS} from '../../core/models/workload.model';
import {TimeEntryService} from '../../core/services/timeEntry.service';
import {MatTab, MatTabGroup} from '@angular/material/tabs';
import {
  MatCell, MatCellDef,
  MatColumnDef,
  MatHeaderCell,
  MatHeaderCellDef,
  MatHeaderRow, MatHeaderRowDef,
  MatRow, MatRowDef,
  MatTable, MatTableDataSource
} from '@angular/material/table';

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
    MatTabGroup,
    MatTab,
    MatTable,
    MatHeaderCell,
    MatCell,
    MatHeaderRow,
    MatRow,
    MatColumnDef,
    MatHeaderCellDef,
    MatHeaderRowDef,
    MatRowDef,
    MatCellDef,
  ],
  templateUrl: './entry-times.component.html',
  styleUrl: './entry-times.component.scss'
})
export class EntryTimesComponent implements OnInit {
  private teamService: TeamService = inject(TeamService);
  private productService: ProductService = inject(ProductService);
  private moduleService: ModuleService = inject(ModuleService);
  private activityService: ActivityService = inject(ActivityService);
  private formBuilder: FormBuilder = inject(FormBuilder);
  protected timeEntryService: TimeEntryService = inject(TimeEntryService);

  public readonly workloads$ = of(WORKLOAD_OPTIONS)

  public teams$ = this.teamService.getTeams$();
  public products$ = this.productService.getProducts$();
  public modules$ = this.moduleService.getModules$();
  public activities$ = this.activityService.getActivities$();
  public entryTimes!: ITimeEntry;
  public entryTimes$!: Observable<ITimeEntryFull[]>;

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

  dataSource!:  MatTableDataSource<ITimeEntryFull>;
  displayedColumns: string[] = ['product', 'module', 'activity', 'workload'];

  ngOnInit() {
    this.timeEntryService.loadEntries();

    effect(() => {
      this.dataSource.data = this.timeEntryService.entries();
    });
  }

  onSubmit() {
    if (this.entryTimesForm.invalid) return;

    this.timeEntryService.addTimeEntry(
      this.entryTimesForm.getRawValue() as any
    );
  }
}
