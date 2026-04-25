import {computed, inject, Injectable, signal, WritableSignal} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {APP_CONFIG} from '../../app.config';
import {ITimeEntry, ITimeEntryFull} from '../models/timeEntry.model';
import {Observable, tap} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TimeEntryService {

  private readonly http: HttpClient = inject(HttpClient);
  private readonly config = inject(APP_CONFIG);
  private readonly baseURL:string = `${this.config.apiBaseUrl}/TimeEntry`;

  private readonly _entries = signal<ITimeEntryFull[]>([]);
  private readonly _previousMonthEntries = signal<ITimeEntryFull[]>([]);
  public readonly entries = this._entries.asReadonly();
  public readonly previousMonthEntries = this._previousMonthEntries.asReadonly();

  public readonly currentWeekEntries = computed(() => {
    const now = new Date();
    const firstDayOfWeek = new Date(now);

    firstDayOfWeek.setDate(now.getDate() - now.getDay() + 1);
    firstDayOfWeek.setHours(0,0,0,0);

    const lastDayOfWeek = new Date(firstDayOfWeek);

    lastDayOfWeek.setDate(firstDayOfWeek.getDate() + 6);
    lastDayOfWeek.setHours(23,59,59,999);

    return this._entries().filter(entry => {
      const workDate = new Date(entry.workDate);
      return workDate >= firstDayOfWeek && workDate <= lastDayOfWeek;
    });
  });

  public loadEntries(): void {
    const currentDate = new Date()
    const startDate = new Date(currentDate.getFullYear(), currentDate.getMonth(), 1)
    const endDate = new Date(currentDate.getFullYear(), currentDate.getMonth() + 1, 0)
    this.loadRange(startDate, endDate, this._entries);
  }

  public loadPreviousMonthEntries(): void {
    const now = new Date();
    const startDate = new Date(now.getFullYear(), now.getMonth() - 1, 1);
    const endDate = new Date(now.getFullYear(), now.getMonth(), 0);
    this.loadRange(startDate, endDate, this._previousMonthEntries);
  }

  // pas de subscribe dans le service
  public addTimeEntry(timeEntry: ITimeEntry): Observable<ITimeEntryFull> {
    return this.http.post<ITimeEntryFull>(this.baseURL, timeEntry).pipe(
      tap(createdEntry => {
        const normalized = this.normalizeEntry(createdEntry);
        this._entries.update(entries => [normalized, ...entries]);
        this._previousMonthEntries.update(entries => [normalized, ...entries]);
      })
    );
  }

  public updateEntryTimes(timeEntry: ITimeEntry): Observable<ITimeEntryFull> {
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
  }

  // use Signal to update a table
  public deleteTimeEntry(id:number):void{
    this.http.delete<ITimeEntryFull[]>(`${this.baseURL}/${id}`)
      .subscribe({
        next: () => {
          this._entries.update(entries => entries.filter(entry => entry.id !== id))
          this._previousMonthEntries.update(entries => entries.filter(entry => entry.id !== id));
        }}
      )
  }

  // -------------------
  // UTILS
  // -------------------
  private loadRange(startDate: Date, endDate: Date, targetSignal: WritableSignal<ITimeEntryFull[]>): void {
    this.http.get<ITimeEntryFull[]>(`${this.baseURL}/Range`, {
      params: this.getParams(startDate, endDate)
    }).subscribe(entries => targetSignal.set(entries.map(e => this.normalizeEntry(e))));
  }

  private normalizeEntry(entry: ITimeEntryFull): ITimeEntryFull {
    return {
      ...entry,
      team: entry.team ?? { id: 0, label: 'inconnu' },
      product: entry.product ?? { id: 0, label: 'inconnu' },
      module: entry.module ?? { id: 0, label: 'inconnu' },
      activity: entry.activity ?? { id: 0, label: 'inconnu' },
      workDate: entry.workDate ?? new Date()
    };
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
