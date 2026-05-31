import {inject, Injectable, WritableSignal} from '@angular/core';
import {IProject} from '../models/project.model';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';



@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  private readonly http: HttpClient = inject(HttpClient);
  private shortUrl = "http://localhost:5062/api"
  private baseUrl = `${this.shortUrl}/Projects`


  public getProject$(): Observable<IProject[]> {
    return this.http.get<IProject[]>(this.baseUrl)
  }

  public addProject$(project: IProject): Observable<IProject> {
    return this.http.post<IProject>(this.baseUrl, project)
  }

}
