import {ChangeDetectionStrategy, Component} from '@angular/core';
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
export class AppComponent {

  public teamList$!: Observable<ITeam[]>;

  public selectedTeam: string = 'Equipe';

  constructor(private readonly teamService: TeamService) {}

  ngOnInit(): void {
    this.teamList$ = this.teamService.getTeams$();
  }

  selectTeam(teamName: string): void {
    this.selectedTeam = teamName;
  }


}
