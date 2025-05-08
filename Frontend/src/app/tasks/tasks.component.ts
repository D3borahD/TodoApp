import {Component, input, Input, model} from '@angular/core';
import {ITask} from '../core/models/task.model';
import {Observable} from 'rxjs';
import {TaskService} from '../core/services/task.service';
import {AsyncPipe} from '@angular/common';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule} from '@angular/forms';
import {NewElementComponent} from '../component/new-element/new-element.component';
import {TaskListComponent} from '../component/task-list/task-list.component';

@Component({
  selector: 'app-tasks',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    FormsModule,
    NewElementComponent,
    TaskListComponent,
  ],
  templateUrl: './tasks.component.html',
  styleUrl: './tasks.component.scss'
})
export class TasksComponent {
  public title:string | null = 'Saisie des temps'

  public taskList!: ITask[]

  public newTask: ITask = {
    id: 0,
    title:  '',
    isCompleted : false
  }

  constructor(
    private readonly taskService:TaskService,
  ) {
    this.taskList = []

    const task1: ITask = {
      id : 1,
      title : "course",
      isCompleted : false
    }
    this.taskList.push(task1);

    const task2: ITask = {
      id : 2,
      title : "menage",
      isCompleted : false
    }
    this.taskList.push(task2);

    const task3: ITask = {
      id : 3,
      title : "lessive",
      isCompleted : false
    }
    this.taskList.push(task3);

  }

  setNewTask($event: ITask) {
    this.newTask = $event
  }
}
