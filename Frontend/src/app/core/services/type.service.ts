import {inject, Service, Signal, signal} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {IType} from '../models/type.model';
import {Observable, tap} from 'rxjs';

@Service()
export class TypeService {
  private readonly http: HttpClient = inject(HttpClient);
  private readonly shortUrl = "http://localhost:5062/api"
  private readonly baseUrl = `${this.shortUrl}/ProjectTypes`

  private readonly typesSignal = signal<IType[]>([]);
  private readonly typeSignal = signal<IType | null>(null);

  // source de vérité
  public readonly steps : Signal<IType[]> = this.typesSignal.asReadonly()
  public readonly step : Signal<IType | null> = this.typeSignal.asReadonly()

  public getAllTypes$(): Observable<IType[]> {
    return this.http.get<IType[]>(this.baseUrl).pipe(
      tap(types => this.typesSignal.set(types)));
  }

}
