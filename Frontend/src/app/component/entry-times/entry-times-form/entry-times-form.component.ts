import {Component, inject, Input, OnInit} from '@angular/core';
import {AsyncPipe} from "@angular/common";
import {MatDatepicker, MatDatepickerInput, MatDatepickerToggle} from "@angular/material/datepicker";
import {MatFormField, MatInput, MatLabel, MatSuffix} from "@angular/material/input";
import {MatOption} from "@angular/material/core";
import {MatSelect} from "@angular/material/select";
import { FormGroup, ReactiveFormsModule} from "@angular/forms";
import {TeamService} from '../../../core/services/team.service';
import {ProductService} from '../../../core/services/product.service';
import {ModuleService} from '../../../core/services/module.service';
import {ActivityService} from '../../../core/services/activity.service';
import {filter, Observable, of, startWith, switchMap} from 'rxjs';
import {WORKLOAD_OPTIONS, WorkloadOption} from '../../../core/models/workload.model';
import {MatRadioButton, MatRadioGroup} from '@angular/material/radio';
import {Module} from '../../../core/models/module.model';
import {Product} from '../../../core/models/product.model';
import {TimeEntryFormType} from '../../../core/models/entryTimeFormType';
import {ITeam} from '../../../core/models/team.model';
import {Activity} from '../../../core/models/activity.model';

@Component({
    selector: 'app-entry-times-form',
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

  public teams$: Observable<ITeam[]>  = this.teamService.getTeams$();
  public products$: Observable<Product[]> = this.productService.getProducts$();
  public modules$: Observable<Module[]> = this.moduleService.getModules$();
  public activities$: Observable<Activity[]>  = this.activityService.getActivities$();
  public readonly workloads$: Observable<readonly WorkloadOption[]>  = of(WORKLOAD_OPTIONS)

  @Input() public form!: FormGroup<TimeEntryFormType>;

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
