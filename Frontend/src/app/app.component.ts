import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {Subscription} from 'rxjs';
import {TitleCasePipe} from '@angular/common';
import {TeamService} from './core/services/team.service';
import {ITeam} from './core/models/team.model';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, TitleCasePipe],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {

  public team : ITeam[] | undefined
  public teamSubscription: Subscription | undefined

  constructor(private readonly teamService: TeamService) { }

  ngOnInit(): void {
    this.getTeams()
  }

  public getTeams():void{
    this.teamSubscription = this.teamService
      .getTeams()
      .subscribe(
        (_team) => {
          this.team = _team;
          console.log("team :", this.team);
        });
  }

}
