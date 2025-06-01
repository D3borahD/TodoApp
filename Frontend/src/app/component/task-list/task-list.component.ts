import {Component, input, InputSignal, OnChanges, SimpleChanges} from '@angular/core';
import {Task} from '../../core/models/task.model';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [
  ],
  templateUrl: './task-list.component.html',
  styleUrl: './task-list.component.scss'
})
export class TaskListComponent implements OnChanges {
  task = input<Task[] | undefined>();
  title = input<string | undefined>();


  constructor() {
  }

  ngOnChanges(changes: SimpleChanges): void {
   if (changes['title']) {

    }
  }

}
