import {Component, input, signal} from '@angular/core';
import {TitleCasePipe} from '@angular/common';
import {MatFormField, MatInput, MatLabel} from '@angular/material/input';
import {ReactiveFormsModule} from '@angular/forms';
import {form, FormField} from '@angular/forms/signals';

interface DialogForm<T> {
  label : string;
  type: string;
}

@Component({
  selector: 'app-dialog-form',
  imports: [
    TitleCasePipe,
    MatLabel,
    MatFormField,
    ReactiveFormsModule,
    FormField,
    MatInput
  ],
  templateUrl: './dialog-form.component.html',
  styleUrl: './dialog-form.component.scss',
})
export class DialogFormComponent {

  title = input<string>('New project');

  formModel = signal<DialogForm<string>>({label: '', type: ''});

  formD = form(this.formModel);
}
