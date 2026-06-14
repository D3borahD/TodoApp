import {Component, inject, input, signal} from '@angular/core';
import {TitleCasePipe} from '@angular/common';
import {MatFormField, MatInput, MatInputModule} from '@angular/material/input';
import {ReactiveFormsModule} from '@angular/forms';
import {form, FormField, FormRoot} from '@angular/forms/signals';
import {ProjectService} from '../../../core/services/project.service';
import {IProject} from '../../../core/models/project.model';
import {MatDialogClose} from '@angular/material/dialog';
import {provideNativeDateAdapter} from '@angular/material/core';
import {
  MatDatepicker,
MatDatepickerInput, MatDatepickerModule,
  MatDatepickerToggle
} from '@angular/material/datepicker';

import {MatIconModule} from '@angular/material/icon';
import {MatFormFieldModule} from '@angular/material/form-field';



@Component({
  selector: 'app-dialog-form',
  imports: [
    TitleCasePipe,
    MatFormField,
    ReactiveFormsModule,
    FormField,
    MatInput,
    MatIconModule,
    MatDatepickerModule,
    MatDialogClose,
    MatDatepickerToggle,
    MatDatepicker,
    MatDatepickerInput,
    MatFormFieldModule, MatInputModule, MatDatepickerModule, FormRoot
  ],
  providers: [provideNativeDateAdapter()],
  templateUrl: './dialog-form.component.html',
  styleUrl: './dialog-form.component.scss',
})
export class DialogFormComponent {
  private projectService: ProjectService = inject(ProjectService);

  title = input<string>('New project');


   projectModel: IProject = {
    id: 1,
    label: '',
    description: '',
    startDate: new Date(),
    endDate : new Date(),
    status: 'InProgress',
    stepsList: []
  };

  projectModelForm = signal<IProject>(this.projectModel);

  formD = form(this.projectModelForm,
    {

      submission: {
        action: async (field) => {



          field().reset({...this.projectModel});
          return {kind: 'serverError', message: 'Failed to submit form'};
        }
      }
    });



  async onSubmit() {


    const form = this.formD();


    if (!form.valid) return;

    const project = form.value() as IProject;

    console.log('Payload:', JSON.stringify(project));

    this.projectService.addProject$(project).subscribe(
      {
        next: (created) => console.log('Project created:', created),
        error: (err) => console.error('Error:', err),
      }
    )
  }
}
