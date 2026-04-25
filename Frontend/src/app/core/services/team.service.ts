import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {ITeam} from '../models/team.model';
import {APP_CONFIG} from '../../app.config';
import {Product} from '../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class TeamService {

  private readonly http: HttpClient = inject(HttpClient);
  private readonly config = inject(APP_CONFIG);
  private readonly baseURL:string = `${this.config.apiBaseUrl}/Teams`;

  public getTeams$(): Observable<ITeam[]>
  {
    return this.http.get<ITeam[]>(this.baseURL)
  }

  public getProductsByTeam(teamId:number | null): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.baseURL}/${teamId}/product`);
  }
}
