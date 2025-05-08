import {EventEmitter, Injectable, WritableSignal} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {BehaviorSubject, Observable} from 'rxjs';
import {ITask} from '../models/task.model';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  constructor(private readonly http: HttpClient) {}

  public shortUrl: string = "http://localhost:5062/Api/Tasks"

  public getTasks(): Observable<ITask[]> {
    return this.http.get<ITask[]>(`${this.shortUrl}/tasks`)
  }

  public addTask(task: ITask): Observable<ITask> {
    return this.http.post<ITask>(`${this.shortUrl}/tasks`, task)
  }

  public updateTask(task: ITask): Observable<ITask> {
    const body = {
      title: task.title,
      isCompleted: task.isCompleted
    };

    console.log('URL :', `${this.shortUrl}/tasks/${task.id}`, 'Body :', body);

    return this.http.patch<ITask>(`${this.shortUrl}/tasks/${task.id}`, body)
  }
}
