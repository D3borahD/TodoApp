import {Component, computed, inject, Input, input, signal, WritableSignal} from '@angular/core';
import {Task} from '../../core/models/task.model';
import {TaskService} from '../../core/services/task.service';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [
  ],
  templateUrl: './task-list.component.html',
  styleUrl: './task-list.component.scss'
})
export class TaskListComponent{
  private taskService = inject(TaskService);

  public tasks = computed(()=> this.taskService.tasks())

  deleteTask(id: number):void {
    this.taskService.deleteTask(id)
  }
}
