import { Injectable } from '@angular/core';
import {IProject} from '../models/project.model';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private readonly http: HttpClient) { }
  public shortUrl: string = "http://localhost:5062/Api/Teams"

  public getProject(): Observable<IProject[]> {
    return this.http.get<IProject[]>(`${this.shortUrl}/projects`)
  }

}
