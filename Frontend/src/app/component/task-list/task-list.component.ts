import {Component, input} from '@angular/core';
import {Task} from '../../core/models/task.model';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [
  ],
  templateUrl: './task-list.component.html',
  styleUrl: './task-list.component.scss'
})
export class TaskListComponent{
  task = input<Task[] | undefined>();
  title = input<string | undefined>();
  newTask = input<string | undefined>();
}
