import {ChangeDetectionStrategy, Component,  LOCALE_ID} from '@angular/core';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MAT_DATE_LOCALE, MatNativeDateModule, MatOptionModule} from '@angular/material/core';
import { MatFormFieldModule} from '@angular/material/form-field';
import {DatePipe, registerLocaleData, TitleCasePipe} from '@angular/common';

import {
  MatDatepickerModule,
} from '@angular/material/datepicker';
import { MatInputModule} from '@angular/material/input';
import localeFr from '@angular/common/locales/fr';

import {MatIcon} from '@angular/material/icon';

registerLocaleData(localeFr);

@Component({
    changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [
    FormsModule,
    ReactiveFormsModule,
    MatOptionModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatFormFieldModule, // Required for mat-form-field and mat-hint
    MatInputModule,
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

  public currentMonth = new Date();


}
