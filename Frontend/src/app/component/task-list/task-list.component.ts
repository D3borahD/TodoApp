import {Component, input, InputSignal} from '@angular/core';
import {Task} from '../../core/models/task.model';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [
  ],
  templateUrl: './task-list.component.html',
  styleUrl: './task-list.component.scss'
})
export class TaskListComponent {
  task = input<Task[] | undefined>();


  constructor() {
  }

}
