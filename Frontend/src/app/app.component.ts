import {ChangeDetectionStrategy, Component} from '@angular/core';
import {RouterLink, RouterLinkActive, RouterOutlet} from '@angular/router';
import {Observable, of, Subscription} from 'rxjs';
import {AsyncPipe} from '@angular/common';
import {TeamService} from './core/services/team.service';
import {ITeam} from './core/models/team.model';
import {NgFor} from '@angular/common';
import {TasksComponent} from './tasks/tasks.component';
import {ReactiveFormsModule} from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    RouterLink,
    ReactiveFormsModule,
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {

  public teamList$!: Observable<ITeam[]>;
  public active = false;
  public selectedTeam: string = 'Equipe';

  constructor(private readonly teamService: TeamService) {}

  ngOnInit(): void {
    this.teamList$ = this.teamService.getTeams();
  }

  selectTeam(teamName: string): void {
    this.selectedTeam = teamName;
  }


}
