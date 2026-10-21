import {ChangeDetectionStrategy, Component, inject, signal} from '@angular/core';
import {RouterLink, RouterOutlet} from '@angular/router';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {ButtonComponent} from './ui/components/button/button.component';
import {TitleCasePipe} from '@angular/common';
import {IButton} from './ui/components/button/button.interface';
import {MatIconModule} from '@angular/material/icon';
import {ProjectDialogService} from './core/services/project-dialog.service';

@Component({
    selector: 'app-root',
  imports: [
    RouterOutlet,
    ReactiveFormsModule,
    ButtonComponent,
    TitleCasePipe,
    MatIconModule,
    FormsModule,
    RouterLink
  ],
    changeDetection: ChangeDetectionStrategy.OnPush,
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
})
export class AppComponent {


  protected readonly title =  signal('kairos');

  button= signal<IButton>(
    {
      label: 'ajouter un nouveau projet',
      icon: 'add',
      action: () => this.projectDialogService.openCreate().subscribe(),
      isDisabled: false
    }
  )
  private projectDialogService = inject(ProjectDialogService);

}
