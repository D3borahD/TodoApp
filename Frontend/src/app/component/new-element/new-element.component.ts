import {Component, model, output, OutputEmitterRef} from '@angular/core';
import { FormsModule } from '@angular/forms';
import {TaskService} from '../../core/services/task.service';
import {Task} from '../../core/models/task.model';

@Component({
  selector: 'app-new-element',
  standalone: true,
  imports: [
    FormsModule
  ],
  templateUrl: './new-element.component.html',
  styleUrl: './new-element.component.scss'
})
export class NewElementComponent {

  public taskTitle = model('')

  addNewTask:OutputEmitterRef<Task> = output()

  constructor(private readonly taskService:TaskService) {}

  addTask() {
    const task = {
      id:0,
      title: this.taskTitle(),
      isCompleted: false
    }

    this.taskService.addTask(task).subscribe({
      next: (response) => {
        this.addNewTask.emit(response);

        // Réinitialise l'input'
        this.taskTitle.set('')
      },
      error: (err) => {
        console.error('Erreur lors de l\'ajout de la tâche :', err);
      }
    });
  }
}
