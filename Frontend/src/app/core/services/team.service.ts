import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {ITeam} from '../models/team.model';
import {APP_CONFIG, AppConfig} from '../../app.config';
import {Product} from '../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class TeamService {
  private readonly baseURL!:string;

  constructor(
    private readonly http: HttpClient,
    @Inject(APP_CONFIG) private config: AppConfig
  ) {
    this.baseURL = `${config.apiBaseUrl}/Teams`;
  }

  public getTeams$(): Observable<ITeam[]>
  {
    return this.http.get<ITeam[]>(this.baseURL)
  }

  public getProductsByTeam(teamId:number | null): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.baseURL}/${teamId}/product`);
  }
}
