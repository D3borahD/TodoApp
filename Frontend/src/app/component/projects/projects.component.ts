import {ChangeDetectionStrategy, Component,  LOCALE_ID} from '@angular/core';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MAT_DATE_LOCALE, MatNativeDateModule, MatOptionModule} from '@angular/material/core';
import { MatFormFieldModule} from '@angular/material/form-field';
import { registerLocaleData} from '@angular/common';

import {
  MatDatepickerModule,
} from '@angular/material/datepicker';
import { MatInputModule} from '@angular/material/input';
import localeFr from '@angular/common/locales/fr';


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

  ],
    providers: [
        { provide: LOCALE_ID, useValue: 'fr-FR' },
        { provide: MAT_DATE_LOCALE, useValue: 'fr-FR' }
    ],
    selector: 'app-projects',
    styleUrl: './projects.component.scss',
    templateUrl: './projects.component.html'
})
export class ProjectsComponent {


}
