import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {IStatus} from '../models/status.model';

@Injectable({
  providedIn: 'root',
})
export class ReferentialService {

  private readonly http: HttpClient = inject(HttpClient);
  private shortUrl = "http://localhost:5062/api"
  private baseUrl = `${this.shortUrl}/Referential`

  public getStatus$(): Observable<IStatus[]> {
    return this.http.get<IStatus[]>(`${this.baseUrl}/status`);
  }

}
