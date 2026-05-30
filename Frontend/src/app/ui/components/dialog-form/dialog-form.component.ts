import {Component, input, signal} from '@angular/core';
import {TitleCasePipe} from '@angular/common';
import {MatFormField, MatInput} from '@angular/material/input';
import {ReactiveFormsModule} from '@angular/forms';
import {form, FormField} from '@angular/forms/signals';
import {ButtonComponent} from '../button/button.component';
import {IButton} from '../button/button.interface';

interface DialogForm<T> {
  label : string;
  type: string;
  status: string;
}

@Component({
  selector: 'app-dialog-form',
  imports: [
    TitleCasePipe,
    MatFormField,
    ReactiveFormsModule,
    FormField,
    MatInput,
    ButtonComponent,

  ],
  templateUrl: './dialog-form.component.html',
  styleUrl: './dialog-form.component.scss',
})
export class DialogFormComponent {

  title = input<string>('New project');
  formModel = signal<DialogForm<string>>({label: '', type: '', status: 'En cours'});
  formD = form(this.formModel);

  button= signal<IButton>(
    {
      label: 'ajouter un nouveau projet',
      icon: 'add',
      action: () => this.save(),
      isDisabled: false
    }
  )

  save(): void {
    console.log('open dialog');
  }
}
