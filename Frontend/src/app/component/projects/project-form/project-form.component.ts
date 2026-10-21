import {Component, inject, Signal, signal, ChangeDetectionStrategy} from '@angular/core';
import {TitleCasePipe} from '@angular/common';
import {MatFormField, MatInput, MatInputModule} from '@angular/material/input';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {form, FormField, FormRoot, required} from '@angular/forms/signals';
import {ProjectService} from '../../../core/services/project.service';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {MatOption, provideNativeDateAdapter} from '@angular/material/core';
import {
  MatDatepicker,
  MatDatepickerInput,
  MatDatepickerModule,
  MatDatepickerToggle
} from '@angular/material/datepicker';
import {MatIconModule} from '@angular/material/icon';
import {MatFormFieldModule} from '@angular/material/form-field';
import {ReferentialService} from '../../../core/services/referential.service';
import {StatusKey} from '../../../core/models/status.model';
import {toSignal} from '@angular/core/rxjs-interop';
import {MatSelect} from '@angular/material/select';
import {MatSelectModule} from '@angular/material/select';
import {firstValueFrom, Observable} from 'rxjs';
import {IProject} from '../../../core/models/project.model';
import {ProjectForm} from '../../../core/models/projectForm';

@Component({
  selector: 'app-dialog-form',
  imports: [
    TitleCasePipe,
    MatFormField,
    ReactiveFormsModule,
    FormField,
    MatInput,
    MatIconModule,
    MatFormField,
    MatOption,
    MatSelect,
    MatDatepickerModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerToggle,
    MatDatepicker,
    MatDatepickerInput,
    MatFormFieldModule, MatInputModule, MatDatepickerModule, FormRoot, MatSelect, MatOption, FormsModule
  ],
  providers: [provideNativeDateAdapter()],
  templateUrl: './project-form.component.html',
  changeDetection: ChangeDetectionStrategy.Eager,
  styleUrl: './project-form.component.scss',
})
export class ProjectFormComponent {

  private static readonly DEFAULT_STATUS: StatusKey = 'NotStarted';

  private dialogRef: MatDialogRef<ProjectFormComponent, IProject> = inject(MatDialogRef<ProjectFormComponent>);
  private referentialService: ReferentialService = inject(ReferentialService);
  private projectService: ProjectService = inject(ProjectService);
  private data = inject(MAT_DIALOG_DATA)

  protected readonly title = this.data.mode === 'edit' ? 'Edit project' : 'New project';
  protected status: Signal<StatusKey[]> = toSignal(this.referentialService.getStatus$(), { initialValue: [] });

  protected readonly projectModel = signal<ProjectForm>(
    this.data.mode === 'edit'
      ? this.toFormValue(this.data.project)
      : this.createEmptyFormValue()
  );

  protected readonly projectModelForm = form(this.projectModel,
    (path) => {
      required(path.label, {message: 'Label is required'});
      required(path.status, {message: 'Status is required'});
    },
    {
      submission: {
        action: async (field) => {
         const savedProject = await firstValueFrom(this.saveProject$(field().value()));
         this.dialogRef.close(savedProject);
        },
        onInvalid: (field) => {
          field().errorSummary()[0]?.fieldTree().focusBoundControl();
        }
      }
    });

  private  saveProject$(formValue: ProjectForm): Observable<IProject> {
    return this.data.mode === 'edit'
    ? this.projectService.updateProject$(this.data.project.id, formValue)
    : this.projectService.addProject$(formValue);
  }

  private createEmptyFormValue(): ProjectForm {
    return {
      label: '',
      description: '',
      startDate: new Date(),
      endDate : new Date(),
      status: ProjectFormComponent.DEFAULT_STATUS,
    };
  }

  private toFormValue(project: IProject): ProjectForm {
    return {
      label: project.label,
      description: project.description,
      stepList: project.stepList,
      startDate: project.startDate,
      endDate: project.endDate,
      status: project.status,
    }
  }
}
