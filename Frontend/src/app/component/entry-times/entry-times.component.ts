import {Component, Inject, OnInit} from '@angular/core';
import {DatePipe} from '@angular/common';
import {MatCard, MatCardActions, MatCardContent, MatCardHeader, MatCardTitle} from '@angular/material/card';
import {MatFormField, MatLabel} from '@angular/material/form-field';
import {MatOption, MatSelect} from '@angular/material/select';
import {ITeam} from '../../core/models/team.model';
import {TeamService} from '../../core/services/team.service';
import {Module} from '../../core/models/module.model';
import {ModuleService} from '../../core/services/module.service';
import {Activity} from '../../core/models/activity.model';
import {ActivityService} from '../../core/services/activity.service';
import {Workload} from '../../core/models/workload.enum';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {Product} from '../../core/models/product.model';
import {ProductService} from '../../core/services/product.service';
import {TimeEntryService} from '../../core/services/timeEntry.service';
import {ITimeEntry} from '../../core/models/timeEntry.model';
import {MatOptionModule} from '@angular/material/core';

@Component({
  selector: 'app-entry-times',
  standalone: true,
  imports: [
    DatePipe,
    MatCard,
    MatCardTitle,
    MatCardHeader,
    MatCardActions,
    MatCardContent,
    MatLabel,
    MatFormField,
    MatSelect,
    MatOption,
    MatOptionModule,
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './entry-times.component.html',
  styleUrl: './entry-times.component.scss'
})
export class EntryTimesComponent implements OnInit {
  constructor(
    @Inject(TeamService) private teamService: TeamService,
    @Inject(ModuleService) private moduleService: ModuleService,
    @Inject(ActivityService) private activityService: ActivityService,
    @Inject(ProductService) private productService: ProductService,
    @Inject(TimeEntryService) private timeEntryService: TimeEntryService,
  ) {}

  public currentDate = new Date();
  public teams: ITeam[] = [];
  public currentUserTeam!: ITeam;
  public products!: Product[];
  public modules!: Module[];
  public activities!: Activity[];
  public timeEntry!: ITimeEntry;
  public workloads:(string | Workload)[] = Object.values(Workload).filter(v => typeof v === 'number') as Workload[];


  public entryTimesForm= new FormGroup({
    id: new FormControl(1),
    userId: new FormControl(1),
    workDate: new FormControl(new Date().toString()),
    workload: new FormControl<Workload | null>(Workload.None, [Validators.required]),
    activityId: new FormControl<number | null>(null, [Validators.required]),
    teamId: new FormControl<number | null>(null, [Validators.required]),
    productId: new FormControl<number | null>(null, [Validators.required]),
    moduleId: new FormControl<number | null>(null, [Validators.required]),
    specificProjectId: new FormControl<number | null>(null),
    comment: new FormControl(null)
  });


  ngOnInit() {
    this.loadDatas();


    // fake data
    this.currentUserTeam = {
      id: 7,
      label: 'panther',
      imageUrl: this.getTeamImageUrl('panther'),
    }
  }

  public loadDatas(): void {
    this.loadTeams();
    this.loadProducts();
    this.loadModules();
    this.loadActivities();
  }

  public loadTeams() {
    this.teamService.getTeams().subscribe(teams =>
      {
        this.teams = teams.map(team => ({
            ...team,
            imageUrl: this.getTeamImageUrl(team.label)
          })
        )
      }
    );
  }

  public loadProducts() {
    this.productService.getProducts().subscribe(
      products => this.products = products
    );
  }
  public loadModules() {
    this.moduleService.getModules().subscribe(
      modules => this.modules = modules
    )
  }

  public loadActivities() {
    this.activityService.getActivities().subscribe(
      activities => this.activities = activities
    )
  }

  private getTeamImageUrl(teamLabel: string) {
    return `/assets/images/teams/${teamLabel.toLowerCase()}.png`;
  }


  addEntryTimes() {
    if (this.entryTimesForm.invalid) {
      console.warn('Le formulaire est invalide');
      return;
    }

    const formValue = this.entryTimesForm.value;
    console.log('Form values:', formValue);

    // ✅ Met à jour ton modèle de manière typée
    const timeEntry: ITimeEntry = {
      id: 1,
      userId: 1,
      workDate: formValue.workDate ? new Date(formValue.workDate).toISOString() : new Date().toISOString(),
      workload: formValue.workload!,
      activityId: formValue.activityId!,
      teamId: formValue.teamId!,
      productId: formValue.productId!,
      moduleId: formValue.moduleId!,
      specificProjectId: formValue.specificProjectId ?? 0,
      comment: formValue.comment ?? '',
    };

    console.log('Updated timeEntry:', timeEntry);

    this.timeEntryService.addTimeEntry(timeEntry).subscribe({
      next: res => console.log('Success', res),
      error: err => console.error('Error', err)
    });

  }
}
