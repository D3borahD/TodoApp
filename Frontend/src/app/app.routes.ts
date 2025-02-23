import { Routes } from '@angular/router';
import { provideRouter } from '@angular/router';
import {TasksComponent} from './tasks/tasks.component';

export const routes: Routes = [
  {path: '', component: TasksComponent},
  {path: 'tasks', component: TasksComponent}
];

