import {Injectable, signal, WritableSignal} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Task} from '../models/task.model';


@Injectable({
  providedIn: 'root'
})
export class TaskService {

  private _tasks = signal<Task[]>([]);
  public readonly tasks = this._tasks.asReadonly();

  private shortUrl: string = "http://localhost:5062/api/Tasks"

  constructor(private readonly http: HttpClient) {
    this.loadTasks();
  }

  private loadTasks() {
    this.http.get<Task[]>(`${this.shortUrl}/tasks`).subscribe(tasks => {
      this._tasks.set(tasks);
    });
  }

  public deleteTask(id: number){
    this.http.delete<void>(`${this.shortUrl}/tasks/${id}`).subscribe({
      next: () => {
        this.loadTasks();
      },
      error: err => {
        console.error('Erreur de suppression : ', err);
      }
    })
  }

  public addTask(task: Task){
    this.http.post<Task>(`${this.shortUrl}/tasks`, task).subscribe({
      next: () => {
        this.loadTasks();
      },
      error: err => {
        console.error('Erreur de suppression : ', err);
      }
    })
  }

  public updateTask(id: number, task: Task){
    this.http.patch<Task>(`${this.shortUrl}/tasks/${id}`, task).subscribe({
      next: () => {
        this.loadTasks();
      },
      error: err => {}
    })
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
