import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {ITeam} from '../models/team.model';


@Injectable({
  providedIn: 'root'
})
export class TeamService {

  constructor(private readonly http: HttpClient) { }
  public url : string = "http://localhost:5062/Api/Teams/teams"

  public getTeams(): Observable<ITeam[]>
  {
    return this.http.get<ITeam[]>(this.url)
  }
}
