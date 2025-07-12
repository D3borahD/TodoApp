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

  public taskTitle: string = '';

  private taskService = inject(TaskService)


  public addTask() {
    const newTask: Task = {
      id: 0, // l'API devrait l'écraser
      title: this.taskTitle,
      isCompleted: false
    };
    this.taskService.addTask(newTask);
    this.taskTitle = ''; // reset input
  }
}
