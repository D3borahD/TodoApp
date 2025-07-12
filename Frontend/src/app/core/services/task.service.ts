import {Injectable, signal, WritableSignal} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Task} from '../models/task.model';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  public taskList: Task[] = []
  private _tasks = signal<Task[]>([]);

  constructor(private readonly http: HttpClient) {
    this.getTasks()
  }

  public shortUrl: string = "http://localhost:5062/api/Tasks"

  public getTasks(): Observable<Task[]> {
    return this.http.get<Task[]>(`${this.shortUrl}/tasks`)
  }

  public addTask(task: Task): Observable<Task> {
    return this.http.post<Task>(`${this.shortUrl}/tasks`, task)
  }

  public deleteTask(id: number): Observable<void> {
    console.log('id : ' + id);
    return this.http.delete<void>(`${this.shortUrl}/tasks/${id}`)
  }

  public refreshTasks(taskListSignal:WritableSignal<Task[]>): void {
    this.getTasks().subscribe(tasks => {
      taskListSignal.set(tasks);
    });
  }

/*  public updateTask(task: Task): Observable<Task> {
    const body = {
      title: task.title,
      isCompleted: task.isCompleted
    };

    console.log('URL :', `${this.shortUrl}/tasks/${task.id}`, 'Body :', body);

    return this.http.patch<Task>(`${this.shortUrl}/tasks/${task.id}`, body)
  }*/




}
