import {Component, inject, Input, Signal, signal} from '@angular/core';
import {
  MatCell,
  MatCellDef,
  MatColumnDef,
  MatHeaderCell, MatHeaderCellDef,
  MatHeaderRow, MatHeaderRowDef,
  MatRow, MatRowDef,
  MatTable
} from '@angular/material/table';
import {MatIcon} from '@angular/material/icon';
import {ITimeEntry, ITimeEntryFull} from '../../../core/models/timeEntry.model';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {TimeEntryService} from '../../../core/services/timeEntry.service';
import {EditDialogComponent} from '../../edit-dialog/edit-dialog.component';
import {MatDialog} from '@angular/material/dialog';
import {TeamService} from '../../../core/services/team.service';
import {ProductService} from '../../../core/services/product.service';
import {ModuleService} from '../../../core/services/module.service';
import {ActivityService} from '../../../core/services/activity.service';
import {of} from 'rxjs';
import {WORKLOAD_OPTIONS} from '../../../core/models/workload.model';
import {DatePipe} from '@angular/common';

@Component({
  selector: 'app-table-content',
  standalone: true,
  imports: [
    MatTable,
    MatColumnDef,
    MatHeaderCell,
    MatIcon,
    DatePipe,
    MatCell, MatCellDef,
    MatHeaderCellDef,
    MatHeaderRow, MatHeaderRowDef,
    MatRow, MatRowDef,
  ],
  templateUrl: './table-content.component.html',
  styleUrl: './table-content.component.scss'
})
export class TableContentComponent {
  protected timeEntryService: TimeEntryService = inject(TimeEntryService);
  private readonly dialog = inject(MatDialog);
  private teamService: TeamService = inject(TeamService);
  private productService: ProductService = inject(ProductService);
  private moduleService: ModuleService = inject(ModuleService);
  private activityService: ActivityService = inject(ActivityService);
  private formBuilder: FormBuilder = inject(FormBuilder);
  @Input() public value!: Signal<ITimeEntryFull[]>;

  public readonly workloads$ = of(WORKLOAD_OPTIONS)

  private readonly _entries = signal<ITimeEntryFull[]>([]);

  public displayedColumns: string[] = ['id', 'workDate','product', 'module', 'activity', 'workload', 'delete', 'edit'];
  private selectedRow: ITimeEntryFull | null = null;
  public teams$ = this.teamService.getTeams$();
  public products$ = this.productService.getProducts$();
  public modules$ = this.moduleService.getModules$();
  public activities$ = this.activityService.getActivities$();
  public entryTimes!: ITimeEntry;


  public update(row:ITimeEntryFull) {
    this.selectedRow = row;
    const update = {
      id: row.id,
      userId: row.userId,
      workDate: row.workDate,
      workload: row.workload,
      productId: row.product?.id ?? 0,
      teamId: row.team?.id ?? 0,
      moduleId: row.module.id ?? 0,
      activityId: row.activity.id ?? 0,
      specificProjectId: row.specificProjectId ?? 0,
      comment: row.comment,
    }
    this.updateEntriesForm.setValue(update);
  }

  public updateEntriesForm: FormGroup = new FormGroup({
    id: new FormControl(Number, [Validators.required]),
    userId: new FormControl(null, [Validators.required]),
    workDate: new FormControl(null, [Validators.required]),
    workload: new FormControl(null, [Validators.required]),
    productId: new FormControl('', [Validators.required]),
    teamId: new FormControl('', [Validators.required]),
    moduleId: new FormControl('', [Validators.required]),
    activityId: new FormControl('', [Validators.required]),
    specificProjectId: new FormControl(''),
    comment: new FormControl(null),
  });


  public delete(element:ITimeEntryFull) {
    this.timeEntryService.deleteTimeEntry(element.id);
  }
  public openDialog(element:ITimeEntryFull) {
    this.dialog.open(EditDialogComponent, {
      data: {
        timeEntry: element,
        activities$: this.activities$,
        workloads$: this.workloads$,
        teams$: this.teams$,
        modules$: this.modules$,
        products$: this.products$,
      },

    })
      .afterClosed()
      .subscribe();
  }

}
