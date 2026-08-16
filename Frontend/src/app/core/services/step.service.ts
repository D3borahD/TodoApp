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
  public readonly projects : Signal<IStep[]> = this.stepsSignal.asReadonly()
  public readonly project : Signal<IStep | null> = this.stepSignal.asReadonly()

  public addStep$(project: IStep): Observable<IStep> {
    return this.http.post<IStep>(this.baseUrl, project).pipe(
      tap(createdStep =>
        this.stepsSignal.update(currentSteps => [...currentSteps, createdStep])
      )
    );
  }
}

