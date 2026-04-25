import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialogContent, MatDialogRef,
  MatDialogTitle
} from '@angular/material/dialog';
import {FormBuilder, FormControl, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';

import {EditDialogData} from './EditDialogData';
import {TimeEntryService} from '../../../core/services/timeEntry.service';
import {ITimeEntry} from '../../../core/models/timeEntry.model';
import {EntryTimesFormComponent} from '../entry-times-form/entry-times-form.component';
import {TimeEntryFormType} from '../../../core/models/entryTimeFormType';

@Component({
    selector: 'app-edit-dialog',
    imports: [
        MatDialogContent,
        FormsModule,
        ReactiveFormsModule,
        MatDialogTitle,
        EntryTimesFormComponent,
    ],
    templateUrl: './edit-dialog.component.html',
    styleUrl: './edit-dialog.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class EditDialogComponent {

  private dialogRef = inject(MatDialogRef<EditDialogComponent>);
  private formBuilder: FormBuilder = inject(FormBuilder);
  private timeEntryService: TimeEntryService = inject(TimeEntryService);

  public data = inject<EditDialogData>(MAT_DIALOG_DATA);

  public entryTime = this.formBuilder.group<TimeEntryFormType>({
    activityId: new FormControl<number| null>(this.data.timeEntry.activity.id??null, [Validators.required]),
    comment: new FormControl<string|null>(this.data.timeEntry.comment??null),
    moduleId: new FormControl<number| null>(this.data.timeEntry.module.id??null, [Validators.required]),
    productId: new FormControl<number| null>(this.data.timeEntry.product.id??null, [Validators.required]),
    specificProjectId: new FormControl<number| null>(this.data.timeEntry.product.id??null),
    teamId: new FormControl<number|null>(this.data.timeEntry.team?.id??null),
    workDate: new FormControl<Date | null>(
      this.data.timeEntry.workDate
        ? new Date(this.data.timeEntry.workDate)
        : null,
      Validators.required
    ),
    workload: new FormControl(this.data.timeEntry.workload??null, [Validators.required]),
  })

  public onSubmit() {
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
