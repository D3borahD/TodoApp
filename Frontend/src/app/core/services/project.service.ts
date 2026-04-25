import {inject, Injectable} from '@angular/core';
import {IProject} from '../models/project.model';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  private readonly http: HttpClient = inject(HttpClient);
  public shortUrl = "http://localhost:5062/Api/Teams"


  public getProject(): Observable<IProject[]> {
    return this.http.get<IProject[]>(`${this.shortUrl}/projects`)
  }

}
