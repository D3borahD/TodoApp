import {Component, inject, OnInit} from '@angular/core';
import {AsyncPipe} from "@angular/common";
import {MatDatepicker, MatDatepickerInput, MatDatepickerToggle} from "@angular/material/datepicker";
import {MatFormField, MatInput, MatLabel, MatSuffix} from "@angular/material/input";
import {MatOption} from "@angular/material/core";
import {MatSelect} from "@angular/material/select";
import {FormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";
import {TeamService} from '../../../core/services/team.service';
import {ProductService} from '../../../core/services/product.service';
import {ModuleService} from '../../../core/services/module.service';
import {ActivityService} from '../../../core/services/activity.service';
import {filter, Observable, of, startWith, switchMap} from 'rxjs';
import {Workload, WORKLOAD_OPTIONS} from '../../../core/models/workload.model';
import {TimeEntryService} from '../../../core/services/timeEntry.service';
import {MatRadioButton, MatRadioGroup} from '@angular/material/radio';
import {Module} from '../../../core/models/module.model';
import {Product} from '../../../core/models/product.model';

@Component({
  selector: 'app-entry-times-form',
  standalone: true,
  imports: [
    AsyncPipe,
    MatDatepicker,
    MatDatepickerInput,
    MatDatepickerToggle,
    MatFormField,
    MatInput,
    MatLabel,
    MatOption,
    MatSelect,
    MatSuffix,
    ReactiveFormsModule,
    MatRadioGroup,
    MatRadioButton
  ],
  templateUrl: './entry-times-form.component.html',
  styleUrl: './entry-times-form.component.scss'
})
export class EntryTimesFormComponent implements OnInit {

  private teamService: TeamService = inject(TeamService);
  private productService: ProductService = inject(ProductService);
  private moduleService: ModuleService = inject(ModuleService);
  private activityService: ActivityService = inject(ActivityService);
  protected timeEntryService: TimeEntryService = inject(TimeEntryService);
  private formBuilder: FormBuilder = inject(FormBuilder);

  public teams$ = this.teamService.getTeams$();
  public products$: Observable<Product[]> = this.productService.getProducts$();
  public modules$: Observable<Module[]> = this.moduleService.getModules$();
  public activities$ = this.activityService.getActivities$();
  public readonly workloads$ = of(WORKLOAD_OPTIONS)

  public entryTimesForm = this.formBuilder.group({
    activityId: [0, [Validators.required]],
    comment: [''],
    moduleId: [0, [Validators.required]],
    productId: [0, [Validators.required]],
    specificProjectId: [0],
    teamId: [0],
    workDate: this.formBuilder.control<Date | null>(new Date(), [Validators.required]),
    workload: this.formBuilder.control<Workload | null>(null, Validators.required),
  })


  public ngOnInit() {
    this.modules$ = this.entryTimesForm.get('productId')!.valueChanges.pipe(
      startWith(this.entryTimesForm.get('productId')!.value), // pour initialisation
      filter(productId => !!productId),
      switchMap(productId =>
        this.productService.getModulesByProducts(productId)
      )
    );

    this.products$ = this.entryTimesForm.get('teamId')!.valueChanges.pipe(
      startWith(this.entryTimesForm.get('teamId')!.value),
      switchMap(teamId =>
        teamId == null || teamId == 0 ? this.productService.getProducts$() :  this.teamService.getProductsByTeam(teamId)
      )
    );
  }

  public onSubmit() {
    if (this.entryTimesForm.invalid) return;

    this.timeEntryService.addTimeEntry(
      this.entryTimesForm.getRawValue() as any
    ).subscribe({
      next: () => {
        this.entryTimesForm.reset({
          workDate: new Date(),
          teamId: 0,
          productId: 0,
          moduleId: 0,
          activityId: 0,
          comment: ''
          });
      },
      error: (err) => {
        console.error(err);
      }
    });

  }
}
