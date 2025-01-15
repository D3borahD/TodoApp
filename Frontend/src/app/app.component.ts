import {ChangeDetectionStrategy, Component, viewChild} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {Observable, Subscription} from 'rxjs';
import {AsyncPipe, NgClass, TitleCasePipe} from '@angular/common';
import {TeamService} from './core/services/team.service';
import {ITeam} from './core/models/team.model';
import {MatIconModule} from '@angular/material/icon';
import {NgFor} from '@angular/common';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    MatIconModule,
    RouterOutlet,
    NgFor,
    TitleCasePipe,
    AsyncPipe,
    NgClass,

  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {

  public teamList$?: Observable<ITeam[]>;
  public active = false;
  private _event: any;

  constructor(private readonly teamService: TeamService) {}

  ngOnInit(): void {
    this.teamList$ = this.teamService.getTeams();

  }

  public toggleSubMenu(event){
    this._event = event;
    console.log('test')
    this.active = !this.active;
  }

}
