import {Component, Input, input, InputSignal} from '@angular/core';
import {ITask} from '../../core/models/task.model';
import {AsyncPipe} from '@angular/common';
import {TaskService} from '../../core/services/task.service';


@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [
    AsyncPipe
  ],
  templateUrl: './task-list.component.html',
  styleUrl: './task-list.component.scss'
})
export class TaskListComponent {
  task:InputSignal<ITask[] | undefined> = input()


  constructor(
    private readonly taskService:TaskService,
  ) {}

  ngOnInit(): void {

  }


  isCompleted(todo: any) {

  }
}
