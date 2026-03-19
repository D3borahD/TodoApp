import {ChangeDetectionStrategy, Component, effect, inject, LOCALE_ID, OnInit} from '@angular/core';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
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

  public entryTimes!: ITimeEntry;
  public dataSource!:  MatTableDataSource<ITimeEntryFull>;

  public ngOnInit() {
    this.timeEntryService.loadEntries();
    this.timeEntryService.loadPreviousMonthEntries();

    effect(() => {
      this.dataSource.data = this.timeEntryService.entries();
    });
  }
}
