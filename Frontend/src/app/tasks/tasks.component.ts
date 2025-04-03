import {Component, inject} from '@angular/core';
import {ITask} from '../core/models/task.model';
import {Observable} from 'rxjs';
import {TaskService} from '../core/services/task.service';
import {AsyncPipe} from '@angular/common';
import {FormBuilder, FormControl, FormGroup, ReactiveFormsModule} from '@angular/forms';



@Component({
  selector: 'app-tasks',
  standalone: true,
  imports: [
    AsyncPipe,
    ReactiveFormsModule,
  ],
  templateUrl: './tasks.component.html',
  styleUrl: './tasks.component.scss'
})
export class TasksComponent {

  public taskList$!:Observable<ITask[]>;
  public title:string | null = 'Saisie des temps'
  public task:ITask| null | undefined = {
    id:0,
    title: '',
    estimatedTime:'',
    realisedTime :'',
    remainedTime : '',
    createdDate : '',
    closedDate : ''
  }

  constructor(
    private readonly taskService:TaskService,
  ) {}

  public name = new FormControl('');

  public addTaskForm = new FormGroup({
    title: new FormControl(''),
    estimatedTime: new FormControl('')
  })


  ngOnInit() {
    this.taskList$ = this.taskService.getTasks();
    console.log(this.taskList$.pipe());
  }


  updateName() {
    this.title = this.name.value;
  }

  addTask() {
    console.warn(this.addTaskForm.value);
    this.task = {
      id: 0,
      title: this.addTaskForm.get('title')?.value || '',
      estimatedTime: this.addTaskForm.get('estimatedTime')?.value || '01:30:00',
      realisedTime: this.addTaskForm.get('realisedTime')?.value || '01:30:00',
      remainedTime: this.addTaskForm.get('remainedTime')?.value || '01:30:00',
      createdDate: '2025-04-02T18:58:47.252Z',
      closedDate: '2025-04-02T18:58:47.252Z'
    }
    this.taskService.addTask(this.task).subscribe({
      next: (response) => {
        console.log("✅ Task added successfully:", response);
        this.taskList$ = this.taskService.getTasks(); // 🔄 Mise à jour de la liste
      },
      error: (error) => console.error("❌ Error while adding task:", error)
    });

  }
}
