import {ChangeDetectionStrategy, Component, inject, Signal, signal} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {ButtonComponent} from './ui/components/button/button.component';
import {TitleCasePipe} from '@angular/common';
import {DialogFormComponent} from './ui/components/dialog-form/dialog-form.component';
import {MatDialog} from '@angular/material/dialog';
import {IButton} from './ui/components/button/button.interface';
import {MatIconModule} from '@angular/material/icon';
import {toSignal} from '@angular/core/rxjs-interop';
import {ReferentialService} from './core/services/referential.service';
import {StatusKey} from './core/models/status.model';
import {ProjectService} from './core/services/project.service';
import {IProject} from './core/models/project.model';
import {MatCard, MatCardContent, MatCardFooter, MatCardHeader, MatCardTitle} from '@angular/material/card';
import {ChipsComponent} from './ui/components/chips/chips.component';

@Component({
    selector: 'app-root',
    imports: [
      RouterOutlet,
      ReactiveFormsModule,
      ButtonComponent,
      TitleCasePipe,
      MatIconModule,
      FormsModule,
      MatCardFooter,
      MatCardContent,
      MatCardTitle,
      MatCardHeader,
      MatCard,
      ChipsComponent,
    ],
    changeDetection: ChangeDetectionStrategy.OnPush,
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
})
export class AppComponent {

  private readonly dialog = inject(MatDialog);
  private readonly referentialService: ReferentialService = inject(ReferentialService);
  private readonly projectService: ProjectService = inject(ProjectService);

  readonly title =  signal('kairos');
  readonly projectList: Signal<IProject[]> = this.projectService.projects;
  status: Signal<StatusKey[]> = toSignal(this.referentialService.getStatus$(), { initialValue: [] });

  button= signal<IButton>(
    {
      label: 'ajouter un nouveau projet',
      icon: 'add',
      action: () => this.openDialog(),
      isDisabled: false
    }
  )

  constructor() {
    // Déclenche le chargement initial; le signal se met à jour via le service
    this.projectService.getProject$().subscribe();
  }

  openDialog(): void {
    this.dialog.open(DialogFormComponent,
      {
        height: 'auto',
        width: '500px'
      })
  }
}
