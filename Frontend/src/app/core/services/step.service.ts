import {inject, Injectable, Signal, signal} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {IStep} from '../models/step.model';
import {Observable, tap} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StepService {
  private readonly http: HttpClient = inject(HttpClient);
  private readonly shortUrl = "http://localhost:5062/api"
  private readonly baseUrl = `${this.shortUrl}/Steps`

  private readonly stepsSignal = signal<IStep[]>([]);
  private readonly stepSignal = signal<IStep | null>(null);

  // source de vérité
  public readonly steps : Signal<IStep[]> = this.stepsSignal.asReadonly()
  public readonly step : Signal<IStep | null> = this.stepSignal.asReadonly()

  public addStep$(step: IStep): Observable<IStep> {
    return this.http.post<IStep>(this.baseUrl, step).pipe(
      tap(createdStep =>
        this.stepsSignal.update(currentSteps => [createdStep, ...currentSteps ]),
      )
    );
  }

  public getStepsByProjectId(id: string): Observable<IStep[]> {
    return this.http.get<IStep[]>(`${this.shortUrl}/Projects/${id}/steps`).pipe(
      tap(steps => this.stepsSignal.set(steps))
    );
  }
}

