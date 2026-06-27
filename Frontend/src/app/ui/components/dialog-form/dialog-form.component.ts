import {Component, inject, input, Signal, signal, ChangeDetectionStrategy} from '@angular/core';
import {TitleCasePipe} from '@angular/common';
import {MatFormField, MatInput, MatInputModule} from '@angular/material/input';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {form, FormField, FormRoot, required} from '@angular/forms/signals';
import {ProjectService} from '../../../core/services/project.service';
import { MatDialogRef} from '@angular/material/dialog';
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
import {IStatus} from '../../../core/models/status.model';
import {toSignal} from '@angular/core/rxjs-interop';
import {MatSelect} from '@angular/material/select';
import {MatSelectModule} from '@angular/material/select';
import {firstValueFrom} from 'rxjs';

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
  templateUrl: './dialog-form.component.html',
  changeDetection: ChangeDetectionStrategy.Eager,
  styleUrl: './dialog-form.component.scss',
})
export class DialogFormComponent {

  private dialogRef: MatDialogRef<DialogFormComponent> = inject(MatDialogRef<DialogFormComponent>);
  private projectService: ProjectService = inject(ProjectService);
  private referentialService: ReferentialService = inject(ReferentialService);

  status: Signal<IStatus[]> = toSignal(this.referentialService.getStatus$(), { initialValue: [] });

  title = input<string>('New project');

   projectModel = signal({
    id: 1,
    label: '',
    description: '',
    startDate: new Date(),
    endDate : new Date(),
    status: this.status()[0] ?? 'NotStarted',
    stepsList: []
    });

  projectModelForm = form(this.projectModel,
    (path) => {
      required(path.label, {message: 'Label is required'});
      required(path.status, {message: 'Status is required'});
    },
    {
      submission: {
        action: async (field) => {
          const result = await firstValueFrom(this.projectService.addProject$(field().value()));
          this.dialogRef.close(DialogFormComponent);

          if (result) return;
        },
        onInvalid: (field) => {
          const firstError = field().errorSummary()[0];
          firstError?.fieldTree().focusBoundControl();
        }
      }
    });
}
