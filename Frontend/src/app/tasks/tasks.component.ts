import {Component, inject, input, InputSignal, OnChanges, OnInit, Signal, signal, SimpleChanges} from '@angular/core';
import {Task} from '../core/models/task.model';
import {TaskService} from '../core/services/task.service';
import {TaskListComponent} from '../component/task-list/task-list.component';
import {NewElementComponent} from '../component/new-element/new-element.component';
import {toSignal} from '@angular/core/rxjs-interop';
import {Observable} from 'rxjs';


@Component({
  selector: 'app-tasks',
  standalone: true,
  imports: [
    TaskListComponent,
    NewElementComponent
  ],
  templateUrl: './tasks.component.html',
  styleUrl: './tasks.component.scss'
})
export class TasksComponent {
  public title:string | null = 'Saisie des temps'

  taskService = inject(TaskService)
  taskListSignal:Signal<Task[] | undefined >

  taskTitle: string = '';

  constructor(
  ) {
    this.taskListSignal = toSignal(this.taskService.getTasks())
  }

}
