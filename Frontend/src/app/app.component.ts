import {ChangeDetectionStrategy, Component, inject, signal} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {ReactiveFormsModule} from '@angular/forms';
import {ButtonComponent} from './ui/components/button/button.component';
import {TitleCasePipe} from '@angular/common';
import {DialogFormComponent} from './ui/components/dialog-form/dialog-form.component';
import {MatDialog} from '@angular/material/dialog';
import {IButton} from './ui/components/button/button.interface';
import {MatIconModule} from '@angular/material/icon';

@Component({
    selector: 'app-root',
  imports: [
    RouterOutlet,
    ReactiveFormsModule,
    ButtonComponent,
    TitleCasePipe,
    MatIconModule,
  ],
    changeDetection: ChangeDetectionStrategy.OnPush,
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss'
})
export class AppComponent {

  readonly title =  signal('kairos');

  private readonly dialog = inject(MatDialog);

  button= signal<IButton>(
    {
      label: 'ajouter un nouveau projet',
      icon: 'add',
      action: () => this.openDialog(),
      isDisabled: false
    }
  )

  openDialog(): void {
    console.log('open dialog');
    const dialogRef = this.dialog.open(DialogFormComponent)

    dialogRef.afterClosed().subscribe(result => {

      console.log('The dialog was closed');
    });


  }
}
