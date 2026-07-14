import {ChangeDetectionStrategy, Component, inject, LOCALE_ID, signal, Signal} from '@angular/core';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MAT_DATE_LOCALE, MatNativeDateModule, MatOptionModule} from '@angular/material/core';
import { MatFormFieldModule} from '@angular/material/form-field';
import { registerLocaleData} from '@angular/common';

import {
  MatDatepickerModule,
} from '@angular/material/datepicker';
import { MatInputModule} from '@angular/material/input';
import localeFr from '@angular/common/locales/fr';
import {ChipsComponent} from '../../ui/components/chips/chips.component';
import {
  MatCard,
  MatCardContent,
  MatCardFooter,
  MatCardHeader,
  MatCardSubtitle,
  MatCardTitle
} from '@angular/material/card';
import {MatIcon} from '@angular/material/icon';
import {StatusKey} from '../../core/models/status.model';
import {toSignal} from '@angular/core/rxjs-interop';
import {IProject} from '../../core/models/project.model';
import {ProjectService} from '../../core/services/project.service';
import {ReferentialService} from '../../core/services/referential.service';
import {RouterLink} from '@angular/router';

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
    ChipsComponent,
    MatCard,
    MatCardContent,
    MatCardFooter,
    MatCardHeader,
    MatCardSubtitle,
    MatCardTitle,
    MatIcon,
    RouterLink,
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

  private readonly projectService: ProjectService = inject(ProjectService);
  private readonly referentialService: ReferentialService = inject(ReferentialService);

  readonly projectList: Signal<IProject[]> = this.projectService.projects;

  status: Signal<StatusKey[]> = toSignal(this.referentialService.getStatus$(), { initialValue: [] });


  constructor() {
    // Déclenche le chargement initial; le signal se met à jour via le service
    this.projectService.getProject$().subscribe();
  }
}
