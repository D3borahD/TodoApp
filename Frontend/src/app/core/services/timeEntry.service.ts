import {Inject, Injectable, signal, WritableSignal} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {APP_CONFIG, AppConfig} from '../../app.config';
import {ITimeEntry, ITimeEntryFull} from '../models/timeEntry.model';

@Injectable({
  providedIn: 'root'
})
export class TimeEntryService {
  private readonly baseURL!:string;

  constructor(
    private readonly http: HttpClient,
    @Inject(APP_CONFIG) private config: AppConfig
  ) {
    this.baseURL = `${config.apiBaseUrl}/TimeEntry`;
  }

  public getTimeEntries(): Observable<ITimeEntryFull[]> {
    return this.http.get<ITimeEntryFull[]>(this.baseURL);
  }

  public addTimeEntry(timeEntry:ITimeEntry):Observable<ITimeEntry>
  {
    return this.http.post<ITimeEntry>(this.baseURL, timeEntry)
  }

}
