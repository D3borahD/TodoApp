import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {APP_CONFIG, AppConfig} from '../../app.config';
import {Observable} from 'rxjs';
import {Activity} from '../models/activity.model';

@Injectable({
  providedIn: 'root'
})
export class ActivityService {
  private readonly baseURL:string;

  constructor(
    private http: HttpClient,
    @Inject(APP_CONFIG) private config: AppConfig
  ) {
    this.baseURL = `${config.apiBaseUrl}/Activity`;
  }

  public getActivities(): Observable<Activity[]>{
    return this.http.get<Activity[]>(this.baseURL);
  }
}
