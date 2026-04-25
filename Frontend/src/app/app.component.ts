import {ChangeDetectionStrategy, Component, inject, OnInit} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {Observable,} from 'rxjs';

import {TeamService} from './core/services/team.service';
import {ITeam} from './core/models/team.model';

import {ReactiveFormsModule} from '@angular/forms';

@Component({
    selector: 'app-root',
    imports: [
        RouterOutlet,
        ReactiveFormsModule,
    ],
    changeDetection: ChangeDetectionStrategy.OnPush,
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {

  private readonly teamService: TeamService = inject(TeamService);

  public teamList$!: Observable<ITeam[]>;

  public selectedTeam = 'Equipe';


  ngOnInit(): void {
    this.teamList$ = this.teamService.getTeams$();
  }

  selectTeam(teamName: string): void {
    this.selectedTeam = teamName;
  }


}
