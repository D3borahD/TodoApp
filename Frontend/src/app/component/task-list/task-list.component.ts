import {Component, Input, input, signal, WritableSignal} from '@angular/core';
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
  public task = input<Task[] | undefined>();
  public title = input<string | undefined>();
  public newTask = input<string | undefined>();

  public taskListSignal = input<WritableSignal<Task[]>>();

  constructor(private taskService: TaskService) {}

  deleteTask(id: number):void {
    this.taskService.deleteTask(id).subscribe({
      next: () => {
        console.log('Tâche supprimée');
        const signal = this.taskListSignal()
        if (signal) {
          this.taskService.refreshTasks(signal);
        }
      },
      error: (err) => {
        console.error('Erreur lors de la suppression', err);
      }
    });
  }
}
