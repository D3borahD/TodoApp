import {ChangeDetectionStrategy, Component, inject, LOCALE_ID} from '@angular/core';
import {FormBuilder, FormControl, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {MAT_DATE_LOCALE, MatNativeDateModule, MatOptionModule} from '@angular/material/core';
import { MatFormFieldModule} from '@angular/material/form-field';
import {DatePipe, registerLocaleData, TitleCasePipe} from '@angular/common';
import {ITimeEntry} from '../../core/models/timeEntry.model';
import {TimeEntryService} from '../../core/services/timeEntry.service';
import {MatTab, MatTabGroup} from '@angular/material/tabs';
import {
  MatDatepickerModule,
} from '@angular/material/datepicker';
import { MatInputModule} from '@angular/material/input';
import localeFr from '@angular/common/locales/fr';
import {TableContentComponent} from './table-content/table-content.component';
import {EntryTimesFormComponent} from './entry-times-form/entry-times-form.component';
import {Workload} from '../../core/models/workload.model';
import {TimeEntryFormType} from '../../core/models/entryTimeFormType';
import {MatIcon} from '@angular/material/icon';

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
    MatFormFieldModule, // Required for mat-form-field and mat-hint
    MatInputModule,
    TableContentComponent,
    EntryTimesFormComponent,
    MatIcon,
    DatePipe,
    TitleCasePipe,
  ],
    providers: [
        { provide: LOCALE_ID, useValue: 'fr-FR' },
        { provide: MAT_DATE_LOCALE, useValue: 'fr-FR' }
    ],
    selector: 'app-entry-times',
    styleUrl: './entry-times.component.scss',
    templateUrl: './entry-times.component.html'
})
export class EntryTimesComponent {

  private timeEntryService: TimeEntryService = inject(TimeEntryService);
  private formBuilder: FormBuilder = inject(FormBuilder);

  currentWeekEntries = this.timeEntryService.currentWeekEntries;
  entries = this.timeEntryService.entries;
  previousMonthEntries = this.timeEntryService.previousMonthEntries;


  public currentMonth = new Date();

  public entryTimes!: ITimeEntry;

  constructor() {
    this.timeEntryService.loadEntries();
    this.timeEntryService.loadPreviousMonthEntries();

    console.log('month : ', this.currentMonth)
  }

  public entryTimesForm = this.formBuilder.group<TimeEntryFormType>({
    activityId: new FormControl<number| null>(null, [Validators.required]),
    comment: new FormControl<string|null>(null),
    moduleId: new FormControl<number| null>(null, [Validators.required]),
    productId: new FormControl<number| null>(null, [Validators.required]),
    specificProjectId: new FormControl<number| null>(null),
    teamId: new FormControl<number|null>(null),
    workDate: this.formBuilder.control<Date | null>(new Date(), [Validators.required]),
    workload: this.formBuilder.control<Workload | null>(1, Validators.required),
  })



  public onSubmit() {
    if (this.entryTimesForm.invalid) return;

    this.timeEntryService.addTimeEntry(
      this.entryTimesForm.getRawValue() as ITimeEntry
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
