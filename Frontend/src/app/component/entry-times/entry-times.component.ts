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
import {FormsModule} from '@angular/forms';
import {Product} from '../../core/models/product.model';
import {ProductService} from '../../core/services/product.service';

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
    FormsModule
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
  ) {}

  public currentDate = new Date();
  public teams: ITeam[] = [];
  public currentUserTeam!: ITeam;
  public products!: Product[];
  public modules!: Module[];
  public activities!: Activity[];
  public workloads:(string | Workload)[] = Object.values(Workload).filter(v => typeof v === 'number') as Workload[];


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



}
