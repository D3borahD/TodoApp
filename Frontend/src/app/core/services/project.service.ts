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

  private readonly projectsSignal = signal<IProject[]>([]);
  private readonly projectSignal = signal<IProject | null>(null);

  // source de vérité
  public readonly projects : Signal<IProject[]> = this.projectsSignal.asReadonly()
  public readonly project : Signal<IProject | null> = this.projectSignal.asReadonly()

  public getAllProjects$(): Observable<IProject[]> {
    return this.http.get<IProject[]>(this.baseUrl).pipe(
      tap(projects => this.projectsSignal.set(projects)));
  }

  public getProjectById$(id: string): Observable<IProject> {
    return this.http.get<IProject>(`${this.baseUrl}/${id}`).pipe(
      tap(project => this.projectSignal.set(project))
    );
  }

  public addProject$(project: IProject): Observable<IProject> {
    return this.http.post<IProject>(this.baseUrl, project).pipe(
      tap(createdProject =>
        this.projectsSignal.update(currentProjects => [...currentProjects, createdProject])
      )
    );
  }
  }
