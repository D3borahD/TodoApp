import {Component, EventEmitter, model, output, Output, OutputEmitterRef, signal} from '@angular/core';
import { FormsModule } from '@angular/forms';
import {TaskService} from '../../core/services/task.service';
import {ITask} from '../../core/models/task.model';

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
  addNewTask:OutputEmitterRef<ITask> = output()

  public newTask = signal<ITask>(
    {
      id:0,
      title: '',
      isCompleted: false
    }
  );


  constructor(
    private readonly taskService:TaskService,
  ) {}

  addTask(): void {

    const task = {
      id:0,
      title: this.newTask().title,
      isCompleted: false
    }

    this.taskService.addTask(task).subscribe({
      next: (response) => {
        console.log(response);
        this.addNewTask.emit(response);
        // Réinitialise le signal
        this.newTask.set({
          id: 0,
          title: '',
          isCompleted: false
        });
      },
      error: (err) => {
        console.error('Erreur lors de l\'ajout de la tâche :', err);
      }
    });

    console.log('test ' + task.title)
    this.addNewTask.emit(task);
  }
}
