import {Component, inject, OnInit, signal} from '@angular/core';
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
export class TasksComponent implements OnInit {
  public title:string | null = 'Saisie des temps'

  taskService = inject(TaskService)

  //taskList= signal<Task[]>([])
 // taskList: Task[]
  taskListSignal
  //taskList$: Observable<Task[]> = this.taskService.getTasks()


  constructor(
  ) {
   // console.log(this.taskService.getAllTasks())
  //  this.taskList= this.taskService.getAllTasks()
    this.taskListSignal = toSignal(this.taskService.getTasks())

  }

  ngOnInit() {
    //this.taskList = this.taskService.getAllTasks()

  }


  setNewTask($event: Task) {

    this.taskService.add($event)
  }
}
