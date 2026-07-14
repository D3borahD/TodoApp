import { Routes } from '@angular/router';
import {ProjectsComponent} from './component/projects/projects.component';
import {ProjectDetailComponent} from './component/projects/project-detail/project-detail.component';

export const routes: Routes = [
  {path: '', component: ProjectsComponent},
  {path: 'projects', component: ProjectsComponent},

  {path: 'projectDetail/:id', component: ProjectDetailComponent}
];

