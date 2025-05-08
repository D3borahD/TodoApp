import {Component, inject, signal} from '@angular/core';
import {Task} from '../core/models/task.model';
import {TaskService} from '../core/services/task.service';
import {TaskListComponent} from '../component/task-list/task-list.component';
import {NewElementComponent} from '../component/new-element/new-element.component';


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

  taskList= signal<Task[]>([])


  constructor(
  ) {
    console.log(this.taskService.getAllTasks())

    this.taskList.set(this.taskService.getAllTasks())
  }


  setNewTask($event: Task) {

    this.taskService.add($event)
  }
}
