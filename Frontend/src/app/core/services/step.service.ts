import {computed, inject, Injectable, Signal, signal} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {IStep} from '../models/step.model';
import {Observable, tap} from 'rxjs';
import {IProjectType} from '../models/projectType.model';
import {StepForm} from '../models/stepForm';

@Injectable({
  providedIn: 'root'
})
export class StepService {
  private readonly http: HttpClient = inject(HttpClient);
  private readonly shortUrl = "http://localhost:5062/api"
  private readonly baseUrl = `${this.shortUrl}/Projects`

  private readonly stepsSignal = signal<IStep[]>([]);
  private readonly stepSignal = signal<IStep | null>(null);

  // source de vérité
  public readonly steps : Signal<IStep[]> = this.stepsSignal.asReadonly()
  public readonly step : Signal<IStep | null> = this.stepSignal.asReadonly()

  public readonly projectTypes: Signal<IProjectType[]> = computed(() => {
    const typesById = new Map(
      this.stepsSignal()
        .filter(step => step.type)
        .map(step => [step.type.id, step.type] as const)
    );
    return [...typesById.values()];
  });

  public addStep$(step: StepForm): Observable<IStep> {
    return this.http.post<IStep>(`${this.baseUrl}/${step.projectId}/steps`, step).pipe(
      tap(createdStep =>
        this.stepsSignal.update(currentSteps => [createdStep, ...currentSteps ]),
      )
    );
  }

  public getStepsByProjectId(id: string): Observable<IStep[]> {
    return this.http.get<IStep[]>(`${this.baseUrl}/${id}/steps`).pipe(
      tap(steps => this.stepsSignal.set(steps))
    );
  }

  public deleteStep$(projectId: string, stepId: string): Observable<boolean> {
    return this.http.delete<boolean>(`${this.baseUrl}/${projectId}/steps/${stepId}`).pipe(
      tap(() => this.stepsSignal.update(currentSteps => currentSteps.filter(step => step.id !== stepId)))
    );
  }

  public updateStep$(projectId: number, step: StepForm): Observable<IStep> {
       return this.http.put<IStep>(`${this.baseUrl}/${projectId}/steps/${step.id}`, step).pipe(
      tap(updatedStep =>
        this.stepsSignal.update(currentSteps =>
          currentSteps.map(current => (current.id === updatedStep.id ? updatedStep : current))
        )
      )

    );
  }
}
