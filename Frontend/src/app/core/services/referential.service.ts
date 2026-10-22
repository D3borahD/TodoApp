import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {map, Observable} from 'rxjs';
import {StatusKey} from '../models/status.model';
import {APP_CONFIG, AppConfig} from '../../app.config';

@Injectable({
  providedIn: 'root',
})
export class ReferentialService {

  private readonly http: HttpClient = inject(HttpClient);
  private url: AppConfig = inject(APP_CONFIG)
  private baseUrl: string = `${this.url.apiBaseUrl}/Referential`

  public getStatus$(): Observable<StatusKey[]> {
    return this.http.get<StatusKey[]>(`${this.baseUrl}/status`);
  }
}
