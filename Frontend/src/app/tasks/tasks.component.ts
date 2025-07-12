import {
  Component,
  inject,
  signal,
  WritableSignal
} from '@angular/core';
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

  public taskService = inject(TaskService)
  public taskListSignal:WritableSignal<Task[]> = signal([]);

  public taskTitle: string = '';

  constructor(
  ) {
    this.taskService.getTasks().subscribe(tasks => {
      this.taskListSignal.set(tasks);
    });
  }

  public onTaskAdded($event: Task) {
    this.taskService.refreshTasks(this.taskListSignal);
  }
}
