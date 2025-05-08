import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Task} from '../models/task.model';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  public taskList: Task[] = []

  constructor(private readonly http: HttpClient) {
    this.load()
  }

  public shortUrl: string = "http://localhost:5062/Api/Tasks"

  private loadTasks(): void {
    const taskData = localStorage.getItem("taskList");
    if (taskData) {
      this.taskList = JSON.parse(taskData).map((taskJson:any) => Object.assign(new Task(),taskJson ));
    }
    else {
      this.init()
      this.save()
    }
  }

  private init() {
    this.taskList = []

    const task1: Task = {
      id : 1,
      title : "course",
      isCompleted : false
    }
    this.taskList.push(task1);

    const task2: Task = {
      id : 2,
      title : "menage",
      isCompleted : false
    }
    this.taskList.push(task2);

    const task3: Task = {
      id : 3,
      title : "lessive",
      isCompleted : false
    }
    this.taskList.push(task3);
  }

  getAllTasks(): Task[] {
    console.log('je suis dans le service' + this.taskList[0].title)
    return this.taskList
  }

  private save(){
    localStorage.setItem("tasklist", JSON.stringify(this.taskList));
  }

  private load(){
    const taskData = localStorage.getItem("tasklist");
    if(taskData){
      this.taskList = JSON.parse(taskData).map((taskJson:any)=> Object.assign(new Task(),taskJson));
    }
  }


/*
  public getTasks(): Observable<Task[]> {
    return this.http.get<Task[]>(`${this.shortUrl}/tasks`)
  }
*/

/*
  public addTask(task: Task): Observable<Task> {
    return this.http.post<Task>(`${this.shortUrl}/tasks`, task)
  }
*/

/*  public updateTask(task: Task): Observable<Task> {
    const body = {
      title: task.title,
      isCompleted: task.isCompleted
    };

    console.log('URL :', `${this.shortUrl}/tasks/${task.id}`, 'Body :', body);

    return this.http.patch<Task>(`${this.shortUrl}/tasks/${task.id}`, body)
  }*/
  add(task:Task):Task {
    const newTask = task

    this.taskList.push(newTask);
    this.save();

    return task
  }
}
