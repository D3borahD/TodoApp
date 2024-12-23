import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {ITask} from './core/models/task.model';
import {Subscription} from 'rxjs';
import {TaskService} from './core/services/task.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {

  public task : ITask | undefined
  public taskSubscription: Subscription | undefined
  constructor(private readonly taskService: TaskService) { }

  ngOnInit(): void {
    this.getTask()
  }

  public getTask():void{
    this.taskSubscription = this.taskService
      .getTasks(this.task)
      .subscribe(
        (_task) => {
          this.task = _task;
          console.log("task :", this.task);
        });
  }

}
