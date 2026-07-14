import {inject, Injectable, Signal, signal} from '@angular/core';
import {IProject} from '../models/project.model';
import {HttpClient} from '@angular/common/http';
import {Observable, tap} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  private readonly http: HttpClient = inject(HttpClient);
  private readonly shortUrl = "http://localhost:5062/api"
  private readonly baseUrl = `${this.shortUrl}/Projects`

  private readonly projectsSignal = signal<IProject[]>([])

  // source de vérité
  public readonly projects : Signal<IProject[]> = this.projectsSignal.asReadonly()



  public getProject$(): Observable<IProject[]> {
    return this.http.get<IProject[]>(this.baseUrl).pipe(
      tap(projects => this.projectsSignal.set(projects)));
  }

  public addProject$(project: IProject): Observable<IProject> {
    return this.http.post<IProject>(this.baseUrl, project).pipe(
      tap(createdProject =>
        this.projectsSignal.update(currentProjects => [...currentProjects, createdProject])
      )
    );
  }
}
