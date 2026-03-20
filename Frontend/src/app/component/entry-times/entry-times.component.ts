import {ChangeDetectionStrategy, Component, effect, inject, LOCALE_ID, OnInit} from '@angular/core';
import {FormBuilder, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {MAT_DATE_LOCALE, MatNativeDateModule, MatOptionModule} from '@angular/material/core';
import { MatFormFieldModule} from '@angular/material/form-field';
import { registerLocaleData} from '@angular/common';
import {ITimeEntry, ITimeEntryFull} from '../../core/models/timeEntry.model';
import {TimeEntryService} from '../../core/services/timeEntry.service';
import {MatTab, MatTabGroup} from '@angular/material/tabs';
import {
  MatTableDataSource,
} from '@angular/material/table';
import {
  MatDatepickerModule,
} from '@angular/material/datepicker';
import { MatInputModule} from '@angular/material/input';
import localeFr from '@angular/common/locales/fr';
import {TableContentComponent} from './table-content/table-content.component';
import {EntryTimesFormComponent} from './entry-times-form/entry-times-form.component';
import {Workload} from '../../core/models/workload.model';

registerLocaleData(localeFr);

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [
    FormsModule,
    ReactiveFormsModule,
    MatOptionModule,
    MatTabGroup,
    MatTab,
    MatDatepickerModule,
    MatNativeDateModule,
    MatFormFieldModule,  // Required for mat-form-field and mat-hint
    MatInputModule,
    TableContentComponent,
    EntryTimesFormComponent,
  ],
  providers: [
    {provide: LOCALE_ID, useValue: 'fr-FR'},
    {provide: MAT_DATE_LOCALE, useValue: 'fr-FR'}
  ],
  selector: 'app-entry-times',
  standalone: true,
  styleUrl: './entry-times.component.scss',
  templateUrl: './entry-times.component.html',
})
export class EntryTimesComponent implements OnInit {

  protected timeEntryService: TimeEntryService = inject(TimeEntryService);
  private formBuilder: FormBuilder = inject(FormBuilder);

  public entryTimes!: ITimeEntry;
  public dataSource!:  MatTableDataSource<ITimeEntryFull>;

  public ngOnInit() {
    this.timeEntryService.loadEntries();
    this.timeEntryService.loadPreviousMonthEntries();

    effect(() => {
      this.dataSource.data = this.timeEntryService.entries();
    });
  }

  public entryTimesForm = this.formBuilder.group({
    activityId: [null, [Validators.required]],
    comment: [''],
    moduleId: [null, [Validators.required]],
    productId: [null, [Validators.required]],
    specificProjectId: [null],
    teamId: [null],
    workDate: this.formBuilder.control<Date | null>(new Date(), [Validators.required]),
    workload: this.formBuilder.control<Workload | null>(1, Validators.required),
  })
  protected form: any;

  public onSubmit() {

    // CHECK FORM
    /*console.log('invalid : ', this.entryTimesForm.invalid)
    this.entryTimesForm.statusChanges.subscribe(status => {
      console.log('Form status:', status);
    });
    this.entryTimesForm.valueChanges.subscribe(() => {
      console.log('Form errors:', this.entryTimesForm.errors);
    });

    this.entryTimesForm.valueChanges.subscribe(() => {
      Object.keys(this.entryTimesForm.controls).forEach(key => {
        const control = this.entryTimesForm.get(key);

        console.log({
          field: key,
          value: control?.value,
          valid: control?.valid,
          errors: control?.errors
        });
      });
    });*/

    if (this.entryTimesForm.invalid) return;

    this.timeEntryService.addTimeEntry(
      this.entryTimesForm.getRawValue() as any
    ).subscribe({
      next: () => {
        this.entryTimesForm.reset({
          workDate: new Date(),
          workload: 1,
          teamId: null,
          productId: null,
          moduleId: null,
          activityId: null,
          comment: ''
        });
      },
      error: (err) => {
        console.error(err);
      }
    });

  }
}
