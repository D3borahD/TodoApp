import {inject, Service, Signal, signal} from '@angular/core';
import {HttpClient} from '@angular/common/http';

import {Observable, tap} from 'rxjs';
import {IProjectType} from '../models/projectType.model';

@Service()
export class TypeService {
  private readonly http: HttpClient = inject(HttpClient);
  private readonly shortUrl = "http://localhost:5062/api"
  private readonly baseUrl = `${this.shortUrl}/ProjectTypes`

  private readonly typesSignal = signal<IProjectType[]>([]);
  private readonly typeSignal = signal<IProjectType | null>(null);

  // source de vérité
  public readonly steps : Signal<IProjectType[]> = this.typesSignal.asReadonly()
  public readonly step : Signal<IProjectType | null> = this.typeSignal.asReadonly()

  public getAllTypes$(): Observable<IProjectType[]> {
    return this.http.get<IProjectType[]>(this.baseUrl).pipe(
      tap(types => this.typesSignal.set(types)));
  }

}
