import {Component, inject, input, Signal, signal} from '@angular/core';
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
import {firstValueFrom} from 'rxjs';
import {StepService} from '../../../../core/services/step.service';
import {IType} from '../../../../core/models/type.model';
import {TypeService} from '../../../../core/services/type.service';

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
  private data = inject<{projectId: number}>(MAT_DIALOG_DATA);
  private stepService: StepService = inject(StepService);

  private referentialService: ReferentialService = inject(ReferentialService);
  private projectTypeService: TypeService = inject(TypeService);

  title = input<string>('New step');

  status: Signal<StatusKey[]> = toSignal(this.referentialService.getStatus$(), { initialValue: [] });
  types: Signal<IType[]> = toSignal(this.projectTypeService.getAllTypes$(), { initialValue: [] });


  stepModel = signal({
    id: 1,
    label: '',
    description: '',
    rank: 0,
    startDate: new Date(),
    endDate : new Date(),
    status: this.status()[0] ?? 'NotStarted',
    type: {
      id: 1,
      label: ''
    },
    duration: 0,
    projectId : this.data.projectId,
  });


  stepModelForm = form(this.stepModel,
    (path) => {
      required(path.label, {message: 'Label is required'});
      required(path.status, {message: 'Status is required'});
    },
    {
      submission: {
        action: async (field) => {
          const result = await firstValueFrom(this.stepService.addStep$(field().value()));
          this.dialogRef.close(StepFormComponent);

          if (result) return;
        },
        onInvalid: (field) => {
          const firstError = field().errorSummary()[0];
          firstError?.fieldTree().focusBoundControl();
        }
      }
    });
}
