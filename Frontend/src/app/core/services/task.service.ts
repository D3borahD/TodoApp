import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {ITask} from '../models/task.model';


@Injectable({
  providedIn: 'root'
})
export class TaskService {

  constructor(private readonly http: HttpClient) { }
  public url : string = "http://localhost:5062/tasks"

  public getTasks(name: ITask | undefined): Observable<ITask>
  {
    return this.http.get<ITask>(this.url)
  }
}
