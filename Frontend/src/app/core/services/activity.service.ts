import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {APP_CONFIG, AppConfig} from '../../app.config';
import {Observable} from 'rxjs';
import {Activity} from '../models/activity.model';

@Injectable({
  providedIn: 'root'
})
export class ActivityService {

  private readonly http: HttpClient = inject(HttpClient);
  private readonly config: AppConfig = inject(APP_CONFIG);
  private readonly baseURL:string = `${this.config.apiBaseUrl}/Activity`;

  public getActivities$(): Observable<Activity[]>{
    return this.http.get<Activity[]>(this.baseURL);
  }
}
