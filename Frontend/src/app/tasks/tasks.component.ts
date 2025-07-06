import {
  Component,
  inject,
  Injector,
  input,
  InputSignal,
  OnChanges,
  OnInit, runInInjectionContext,
  Signal,
  signal,
  SimpleChanges, WritableSignal
} from '@angular/core';
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
  taskListSignal:WritableSignal<Task[]> = signal([]);

  taskTitle: string = '';
  viewNewTask:boolean = false;

  constructor(
  ) {
    this.taskService.getTasks().subscribe(tasks => {
      this.taskListSignal.set(tasks);
    });
  }


  ngOnChanges(changes: SimpleChanges): void {
    /*if(this.viewNewTask == true) {
      this.taskListSignal = toSignal(this.taskService.getTasks())
    }*/
    this.taskService.getTasks().subscribe(tasks => {
      this.taskListSignal.set(tasks);
    });
  }

  newTask($event: Boolean) {
    console.log('new task', $event);
  }

  onTaskAdded($event: Task) {
    console.log('Nouvelle tâche ajoutée :', $event);
    this.refreshTasks();
  }

  private refreshTasks() {
    this.taskService.getTasks().subscribe(tasks => {
      this.taskListSignal.set(tasks);
    });
  }
}
