import {Component, inject, Signal, signal} from '@angular/core';
import {form, FormField, FormRoot, required} from '@angular/forms/signals';
import {FormsModule} from '@angular/forms';
import {MatDatepicker, MatDatepickerInput, MatDatepickerToggle} from '@angular/material/datepicker';
import {MatError, MatFormField, MatInput, MatSuffix} from '@angular/material/input';
import {MatOption} from '@angular/material/core';
import {MatSelect} from '@angular/material/select';
import {TitleCasePipe} from '@angular/common';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {StatusKey} from '../../../../core/models/status.model';
import {toSignal} from '@angular/core/rxjs-interop';
import {ReferentialService} from '../../../../core/services/referential.service';
import {firstValueFrom, Observable} from 'rxjs';
import {StepService} from '../../../../core/services/step.service';
import {TypeService} from '../../../../core/services/type.service';
import {IProjectType} from '../../../../core/models/projectType.model';
import {IStep} from '../../../../core/models/step.model';
import {StepForm} from '../../../../core/models/stepForm';

@Component({
  selector: 'app-step-form',
  imports: [
    FormRoot,
    FormsModule,
    MatDatepicker,
    MatDatepickerInput,
    MatDatepickerToggle,
    MatError,
    MatFormField,
    MatInput,
    MatOption,
    MatSelect,
    MatSuffix,
    TitleCasePipe,
    FormField
  ],
  templateUrl: './step-form.component.html',
  styleUrl: './step-form.component.scss',
})
export class StepFormComponent {

  private dialogRef: MatDialogRef<StepFormComponent> = inject(MatDialogRef<StepFormComponent>);
  private data = inject<{projectId: number, step?: IStep}>(MAT_DIALOG_DATA);
  private stepService: StepService = inject(StepService);
  private referentialService: ReferentialService = inject(ReferentialService);
  private projectTypeService: TypeService = inject(TypeService);

  protected readonly isEditMode = this.data.step !== undefined;
  protected readonly title = this.isEditMode ? 'Edit step' : 'New step';
  protected readonly stepModel = signal<StepForm>(
    this.data.step ? this.toFormValue(this.data.step) : this.createEmptyFormValue()
  );

  status: Signal<StatusKey[]> = toSignal(this.referentialService.getStatus$(), { initialValue: [] });
  types: Signal<IProjectType[]> = toSignal(this.projectTypeService.getAllTypes$(), { initialValue: [] });
  type: IProjectType = this.data.step?.type ?? this.types()[0];

  protected readonly stepModelForm = form(
    this.stepModel,
    (path) => {
      required(path.label, {message: 'Label is required'});
      required(path.status, {message: 'Status is required'});
    },
    {
      submission: {
        action: async (field) => {
          const savedStep = await firstValueFrom(this.saveStep$(field().value()));
          this.dialogRef.close(savedStep);
        },
        onInvalid: (field) => {
          field().errorSummary()[0]?.fieldTree().focusBoundControl();
        }
      }
    });

  private saveStep$(formValue: StepForm): Observable<IStep> {
    console.log('save step', {formValue});
    return this.isEditMode
      ? this.stepService.updateStep$(this.data.projectId, formValue)
      : this.stepService.addStep$(formValue);
  }

  private createEmptyFormValue(): StepForm {
    return {
      label: '',
      description: '',
      startDate: new Date(),
      endDate: new Date(),
      status: 'NotStarted',
      type: this.type,
      projectId: this.data.projectId,
    };
  }

  private toFormValue(step: IStep): StepForm{
    return {
      id: step.id,
      label: step.label,
      description: step.description,
      startDate: step.startDate,
      endDate: step.endDate,
      status: step.status,
      type: step.type,
      projectId: step.projectId,
    };
  }
}
