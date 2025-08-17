import {Component, computed, inject} from '@angular/core';
import {Task} from '../../core/models/task.model';
import {TaskService} from '../../core/services/task.service';
import {TitleCasePipe} from '@angular/common';
import {FormsModule} from '@angular/forms';



@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [
    TitleCasePipe,
    FormsModule
  ],
  templateUrl: './task-list.component.html',
  styleUrl: './task-list.component.scss'
})
export class TaskListComponent{
  private taskService = inject(TaskService);

  public tasks = computed(()=> this.taskService.tasks())
  maTodo: string = 'je suis une todo';


  deleteTask(id: number):void {
    this.taskService.deleteTask(id)
  }

  updateCompleted(task: Task):void {
    task.isCompleted = !task.isCompleted;
    this.taskService.updateTask(task.id!, task);

    console.log(task.isCompleted);
  }

  updateTaskTitle(task: Task):void {
   // task.title = task.title;
    // todo : un système pour éditer la tache (activer l'input ? ouvrir une pop up)
    this.maTodo = task.title;
    task.title = 'test task edit';
    this.taskService.updateTask(task.id!, task);
  }

  onSubmitForm() {

  }
}
