import { Routes } from '@angular/router';
import {ProjectsComponent} from './component/projects/projects.component';

export const routes: Routes = [
  {path: '', component: ProjectsComponent},
  {path: 'projects', component: ProjectsComponent}
];

