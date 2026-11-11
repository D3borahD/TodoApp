import {inject, Injectable, Signal, signal} from '@angular/core';
import {IProject} from '../models/project.model';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable, tap} from 'rxjs';
import {ProjectForm} from '../models/projectForm';
import {APP_CONFIG} from '../../app.config';
import {ISearchParameters} from '../models/search-parameters';


@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  private readonly http: HttpClient = inject(HttpClient);
  private url = inject(APP_CONFIG)
  private readonly baseUrl = `${this.url.apiBaseUrl}/Projects`

  private readonly projectsSignal = signal<IProject[]>([]);
  private readonly projectSignal = signal<IProject | null>(null);

  // source de vérité
  public readonly projects : Signal<IProject[]> = this.projectsSignal.asReadonly()
  public readonly project : Signal<IProject | null> = this.projectSignal.asReadonly()

  public getAllProjects$(searchParameters?: ISearchParameters): Observable<IProject[]> {
    const params = this.buildHttpParams(searchParameters);

    return this.http.get<IProject[]>(this.baseUrl, {params: params}).pipe(
      tap(projects => this.projectsSignal.set(projects)));
  }

  public getProjectById$(id: string): Observable<IProject> {
    return this.http.get<IProject>(`${this.baseUrl}/${id}`).pipe(
      tap(project => this.projectSignal.set(project))
    );
  }

  public addProject$(project: ProjectForm): Observable<IProject> {
    return this.http.post<IProject>(this.baseUrl, project).pipe(
      tap(createdProject =>
        this.projectsSignal.update(currentProjects => [...currentProjects, createdProject])
      )
    );
  }

  public updateProject$(projectId: number, formValue: ProjectForm): Observable<IProject> {
    return this.http.put<IProject>(`${this.baseUrl}/${projectId}`, formValue).pipe(
      tap(updatedProject => {
        this.projectsSignal.update(currentProjects =>
          currentProjects.map(current => (current.id === updatedProject.id ? updatedProject : current))
        );
        this.projectSignal.update(current =>
          current?.id === updatedProject.id ? updatedProject : current
        );
      })
    );
  }

  private buildHttpParams(searchParameters?: ISearchParameters): HttpParams {
    const queryParams: Record<string, string | number> = {};

    if (searchParameters?.label) {
      queryParams['label'] = searchParameters.label;
    }
    if (searchParameters?.status) {
      queryParams['status'] = searchParameters.status;
    }
    if (searchParameters?.projectTypeId != null) {
      queryParams['projectTypeId'] = searchParameters.projectTypeId;
    }
    if (searchParameters?.startDate) {
      queryParams['startDate'] = searchParameters.startDate.toISOString();
    }
    if (searchParameters?.endDate) {
      queryParams['endDate'] = searchParameters.endDate.toISOString();
    }
    if (searchParameters?.pageNumber != null) {
      queryParams['pageNumber'] = searchParameters.pageNumber;
    }
    if (searchParameters?.pageSize != null) {
      queryParams['pageSize'] = searchParameters.pageSize;
    }

    return new HttpParams({ fromObject: queryParams });
  }
}
