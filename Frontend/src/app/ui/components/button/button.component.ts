import {Component, inject, input} from '@angular/core';
import {UpperCasePipe} from '@angular/common';
import {MatIcon} from '@angular/material/icon';
import {DialogFormComponent} from '../dialog-form/dialog-form.component';
import {MatDialog} from '@angular/material/dialog';

@Component({
  selector: 'app-button',
  imports: [
    UpperCasePipe,
    MatIcon
  ],
  templateUrl: './button.component.html',
  styleUrl: './button.component.scss',
})
export class ButtonComponent {

  action = input<string>();
  icon = input<string>();


  readonly dialog = inject(MatDialog);

  protected openDialog(): void {
    console.log('click on button: ' )
    const dialogRef = this.dialog.open(DialogFormComponent)

  }
}
