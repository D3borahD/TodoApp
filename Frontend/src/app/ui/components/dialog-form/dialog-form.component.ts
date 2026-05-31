import {Component, inject, input, signal} from '@angular/core';
import {TitleCasePipe} from '@angular/common';
import {MatFormField, MatInput} from '@angular/material/input';
import {ReactiveFormsModule} from '@angular/forms';
import {form, FormField} from '@angular/forms/signals';
import {Observable} from 'rxjs';
import {ProjectService} from '../../../core/services/project.service';
import {IProject} from '../../../core/models/project.model';



@Component({
  selector: 'app-dialog-form',
  imports: [
    TitleCasePipe,
    MatFormField,
    ReactiveFormsModule,
    FormField,
    MatInput
  ],
  templateUrl: './dialog-form.component.html',
  styleUrl: './dialog-form.component.scss',
})
export class DialogFormComponent {
  private projectService: ProjectService = inject(ProjectService);

  title = input<string>('New project');

  formModel = signal<IProject>(
    {
      id: 1,
      label: '',
      description: '',
      startDate: new Date(),
      endDate : new Date(),
      status: 'InProgress',
    }
  );
  formD = form(this.formModel);

  /*button= signal<IButton>(
    {
      label: 'ajouter un nouveau projet',
      icon: 'add',
     // action: () => this.save(event),
      isDisabled: false
    }
  )*/




  protected onSubmit($event: SubmitEvent) {

    $event.preventDefault();
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
