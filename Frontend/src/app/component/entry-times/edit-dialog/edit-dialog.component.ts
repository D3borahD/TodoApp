import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialogContent, MatDialogRef,
  MatDialogTitle
} from '@angular/material/dialog';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';

import {EditDialogData} from './EditDialogData';
import {TimeEntryService} from '../../../core/services/timeEntry.service';
import {ITimeEntry} from '../../../core/models/timeEntry.model';
import {EntryTimesFormComponent} from '../entry-times-form/entry-times-form.component';

@Component({
  selector: 'edit-dialog',
  standalone: true,
  imports: [
    MatDialogContent,
    FormsModule,
    ReactiveFormsModule,
    MatDialogTitle,
    EntryTimesFormComponent,
  ],
  templateUrl: './edit-dialog.component.html',
  styleUrl: './edit-dialog.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EditDialogComponent {
  public data = inject<EditDialogData>(MAT_DIALOG_DATA);
  protected timeEntryService: TimeEntryService = inject(TimeEntryService);
  private dialogRef = inject(MatDialogRef<EditDialogComponent>);

  public id = this.data.timeEntry.id;


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
