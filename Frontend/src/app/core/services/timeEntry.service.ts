import { inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {APP_CONFIG} from '../../app.config';


@Injectable({
  providedIn: 'root'
})
export class TimeEntryService {

  private readonly http: HttpClient = inject(HttpClient);
  private readonly config = inject(APP_CONFIG);
  private readonly baseURL:string = `${this.config.apiBaseUrl}/TimeEntry`;




  // pas de subscribe dans le service
/*  public addTimeEntry(timeEntry: ITimeEntry): Observable<ITimeEntryFull> {
    return this.http.post<ITimeEntryFull>(this.baseURL, timeEntry).pipe(
      tap(createdEntry => {
        const normalized = this.normalizeEntry(createdEntry);
        this._entries.update(entries => [normalized, ...entries]);
        this._previousMonthEntries.update(entries => [normalized, ...entries]);
      })
    );
  }*/

  /*public updateEntryTimes(timeEntry: ITimeEntry): Observable<ITimeEntryFull> {
    return this.http
      .put<ITimeEntryFull>(`${this.baseURL}/${timeEntry.id}`, timeEntry)
      .pipe(
        tap(updatedEntry => {
          const normalized = this.normalizeEntry(updatedEntry);

          this._entries.update(entries =>
            entries.map(entry => entry.id === normalized.id ? normalized : entry)
          );

          this._previousMonthEntries.update(entries =>
            entries.map(entry => entry.id === normalized.id ? normalized : entry)
          );
        })
      );
  }*/



}
