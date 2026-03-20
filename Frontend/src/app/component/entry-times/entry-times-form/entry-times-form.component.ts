import {Component, inject, Input, OnInit} from '@angular/core';
import {AsyncPipe} from "@angular/common";
import {MatDatepicker, MatDatepickerInput, MatDatepickerToggle} from "@angular/material/datepicker";
import {MatFormField, MatInput, MatLabel, MatSuffix} from "@angular/material/input";
import {MatOption} from "@angular/material/core";
import {MatSelect} from "@angular/material/select";
import {FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
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
  private timeEntryService: TimeEntryService = inject(TimeEntryService);
  private formBuilder: FormBuilder = inject(FormBuilder);

  public teams$ = this.teamService.getTeams$();
  public products$: Observable<Product[]> = this.productService.getProducts$();
   public modules$: Observable<Module[]> = this.moduleService.getModules$();
  public activities$ = this.activityService.getActivities$();
  public readonly workloads$ = of(WORKLOAD_OPTIONS)

    // Données initiales du formulaire
  // si ID présent alors récupère les info, sinon, form de saisie
  @Input() public form = this.formBuilder.group({
    activityId: [null, [Validators.required]],
    comment: [''],
    moduleId: [null, [Validators.required]],
    productId: [null, [Validators.required]],
    specificProjectId: [null],
    teamId: [null],
    workDate: this.formBuilder.control<Date | null>(new Date(), [Validators.required]),
    workload: this.formBuilder.control<Workload | null>(1, Validators.required),
  })


  @Input() entryTime!: FormGroup<{
    teamId: FormControl<number | null>;
    productId: FormControl<number | null>;
    moduleId: FormControl<number | null>;
    activityId: FormControl<number | null>;
    workload: FormControl<1 | 0.75 | 0.5 | 0.25 | 0 | null>;
    workDate: FormControl<Date | null>
  }>;

  public ngOnInit() {
    this.modules$ = this.form.get('productId')!.valueChanges.pipe(
      startWith(this.form.get('productId')!.value), // pour initialisation
      filter(productId => !!productId),
      switchMap(productId =>
        this.productService.getModulesByProducts(productId)
      )
    );

    this.products$ = this.form.get('teamId')!.valueChanges.pipe(
      startWith(this.form.get('teamId')!.value),
      switchMap(teamId =>
        teamId == null || teamId == 0 ? this.productService.getProducts$() :  this.teamService.getProductsByTeam(teamId)
      )
    );
  }
}
