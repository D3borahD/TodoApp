import {Inject, Injectable, signal} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {APP_CONFIG, AppConfig} from '../../app.config';
import {ITimeEntry, ITimeEntryFull} from '../models/timeEntry.model';
import {Observable, tap} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TimeEntryService {

  private readonly baseURL!:string;
  private readonly _entries = signal<ITimeEntryFull[]>([]);
  public readonly entries = this._entries.asReadonly();

  private constructor(
    private readonly http: HttpClient,
    @Inject(APP_CONFIG) private config: AppConfig
  ) {
    this.baseURL = `${config.apiBaseUrl}/TimeEntry`;
  }

  public loadEntries(): void {
    this.http.get<ITimeEntryFull[]>(this.baseURL)
      .subscribe(entries => {
        this._entries.set(entries);
      });
  }

  public addTimeEntry(timeEntry:ITimeEntry):void
  {
    this.http.post<ITimeEntryFull>(this.baseURL, timeEntry)
      .subscribe(createdEntry => {
        this._entries.update(entries => [
          {
            ...createdEntry,
            team: createdEntry.team ?? { id: 0, label: 'inconnu' },
            product: createdEntry.product ?? { id: 0, label: 'inconnu' },
            module: createdEntry.module ?? { id: 0, label: 'inconnu' },
            activity: createdEntry.activity ?? { id: 0, label: 'inconnu' },
            workDate : createdEntry.workDate ?? new Date
          },
          ...entries
        ]);
      });
  }

  public updateEntryTimes(timeEntry: ITimeEntry): Observable<ITimeEntryFull> {
    return this.http
      .put<ITimeEntryFull>(`${this.baseURL}/${timeEntry.id}`, timeEntry)
      .pipe(
        tap(updatedEntry => {
          this._entries.update(entries =>
            entries.map(entry =>
              entry.id === updatedEntry.id
                ? { ...entry, ...updatedEntry }
                : entry
            )
          );
        })
      );
  }

  // use Signal to update table
  public deleteTimeEntry(id:number):void{
    console.log('id ', id)
    this.http.delete<ITimeEntryFull[]>(`${this.baseURL}/${id}`)
      .subscribe({
        next: () => {
          this._entries.update(entries => entries.filter(entry => entry.id !== id))
        }}
    )
  }

}
