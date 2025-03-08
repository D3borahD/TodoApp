import { Component } from '@angular/core';
import {ITask} from '../core/models/task.model';
import {Observable} from 'rxjs';
import {TaskService} from '../core/services/task.service';
import {AsyncPipe} from '@angular/common';

@Component({
  selector: 'app-tasks',
  standalone: true,
  imports: [
    AsyncPipe
  ],
  templateUrl: './tasks.component.html',
  styleUrl: './tasks.component.scss'
})
export class TasksComponent {
  public title:string = 'Tasks';

  public taskList$!:Observable<ITask[]>;

  constructor(private readonly taskService:TaskService,) {}

  ngOnInit() {
    this.taskList$ = this.taskService.getTasks();
    console.log(this.taskList$.pipe());
  }
}
