import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogClose,
  MatDialogContent, MatDialogRef,
  MatDialogTitle
} from '@angular/material/dialog';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {AsyncPipe} from '@angular/common';
import {MatFormField, MatLabel} from '@angular/material/form-field';
import {MatOption} from '@angular/material/core';
import {MatSelect} from '@angular/material/select';
import {MatButton} from '@angular/material/button';
import {EditDialogData} from './EditDialogData';
import {ITimeEntry, ITimeEntryFull} from '../../core/models/timeEntry.model';
import {TimeEntryService} from '../../core/services/timeEntry.service';
import {MatDatepicker, MatDatepickerInput, MatDatepickerToggle} from '@angular/material/datepicker';
import {MatInput} from '@angular/material/input';

@Component({
  selector: 'edit-dialog',
  standalone: true,
  imports: [
    MatDialogContent,
    MatDialogActions,
    AsyncPipe,
    FormsModule,
    MatFormField,
    MatLabel,
    MatOption,
    MatSelect,
    ReactiveFormsModule,
    MatDialogTitle,
    MatButton,
    MatDialogClose,
    MatDatepickerInput,
    MatDatepickerToggle,
    MatDatepicker,
    MatInput,
  ],
  templateUrl: './edit-dialog.component.html',
  styleUrl: './edit-dialog.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EditDialogComponent {
  public data = inject<EditDialogData>(MAT_DIALOG_DATA);
  protected timeEntryService: TimeEntryService = inject(TimeEntryService);
  private dialogRef = inject(MatDialogRef<EditDialogComponent>);

  public entryTime = new FormGroup({
      teamId: new FormControl<number|null>(this.data.timeEntry.team?.id??null),
      productId: new FormControl(this.data.timeEntry.product.id??null, [Validators.required]),
      moduleId: new FormControl(this.data.timeEntry.module.id??null, [Validators.required]),
      activityId: new FormControl(this.data.timeEntry.activity.id??null, [Validators.required]),
      workload: new FormControl(this.data.timeEntry.workload??null, [Validators.required]),
      workDate: new FormControl<Date | null>(
      this.data.timeEntry.workDate
        ? new Date(this.data.timeEntry.workDate)
        : null,
      Validators.required
    )
  })

  public products$ = this.data.products$;
  public workloads$ = this.data.workloads$;
  public teams$ = this.data.teams$;
  public modules$ = this.data.modules$;
  public activities$ = this.data.activities$;

  public update() {
    if (this.entryTime.invalid) return;

    const formValue = this.entryTime.getRawValue();

    const updatedEntry: ITimeEntry = {
      id: this.data.timeEntry.id,
      userId: this.data.timeEntry.userId,
      specificProjectId: this.data.timeEntry.specificProjectId,
      comment: this.data.timeEntry.comment,
      teamId: formValue.teamId,
      productId: formValue.productId,
      moduleId: formValue.moduleId,
      activityId: formValue.activityId,
      workload: formValue.workload,
      workDate: formValue.workDate
        ? formValue.workDate.toISOString()
        : null
    };

    this.timeEntryService.updateEntryTimes(updatedEntry).subscribe(() => {
      this.dialogRef.close(true);
    });
  }
}
