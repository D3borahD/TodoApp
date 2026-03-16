import {Inject, Injectable, signal} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
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
  private readonly _previousMonthEntries = signal<ITimeEntryFull[]>([]);
  public readonly previousMonthEntries = this._previousMonthEntries.asReadonly();
  private readonly _currentWeekEntries = signal<ITimeEntryFull[]>([]);
  public readonly currentWeekEntries = this._currentWeekEntries.asReadonly();

  private constructor(
    private readonly http: HttpClient,
    @Inject(APP_CONFIG) private config: AppConfig
  ) {
    this.baseURL = `${config.apiBaseUrl}/TimeEntry`;
  }

  public loadEntries(): void {
    let currentDate = new Date()
    const startDate = new Date(currentDate.getFullYear(), currentDate.getMonth(), 1)
    const endDate = new Date(currentDate.getFullYear(), currentDate.getMonth() + 1, 0)

   this.http.get<ITimeEntryFull[]>(`${this.baseURL}/Range`, {
     params: this.getParams(startDate, endDate)
   })
      .subscribe(entries => {
        this._entries.set(entries);
      });
  }

  public loadPreviousMonthEntries(): void {
    let currentDate = new Date()
    const startDate = new Date(currentDate.getFullYear(), currentDate.getMonth() -1 , 1)
    const endDate = new Date(currentDate.getFullYear(), currentDate.getMonth() , 0)

    this.http.get<ITimeEntryFull[]>(`${this.baseURL}/Range`, {
      params: this.getParams(startDate, endDate)
    })
      .subscribe(entries => {
        this._previousMonthEntries.set(entries);
      });
  }

  public loadCurrentWeekEntries(): void {
    let currentDate = new Date()

    const day = currentDate.getDay()
    const startDate = new Date(currentDate)
    startDate.setDate(currentDate.getDate() - day + 1)

    const endDate = new Date(startDate)
    endDate.setDate(startDate.getDate() + 6)


    this.http.get<ITimeEntryFull[]>(`${this.baseURL}/Range`, {
      params: this.getParams(startDate, endDate)
    })
      .subscribe(entries => {
        this._currentWeekEntries.set(entries);
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

  // use Signal to update a table
  public deleteTimeEntry(id:number):void{
    console.log('id ', id)
    this.http.delete<ITimeEntryFull[]>(`${this.baseURL}/${id}`)
      .subscribe({
        next: () => {
          this._entries.update(entries => entries.filter(entry => entry.id !== id))
        }}
      )
  }

  private getParams(startDate: Date, endDate: Date):HttpParams{
    return new HttpParams({
      fromObject: {
        start: this.formatDate(startDate),
        end: this.formatDate(endDate)
      }
    });
  }

  private formatDate(date: Date): string {
    const year = date.getFullYear()
    const month = String(date.getMonth() + 1).padStart(2, '0')
    const day = String(date.getDate()).padStart(2, '0')

    return `${year}-${month}-${day}`
  }
}
