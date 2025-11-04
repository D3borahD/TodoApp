import { Routes } from '@angular/router';
import {EntryTimesComponent} from './component/entry-times/entry-times.component';

export const routes: Routes = [
  {path: '', component: EntryTimesComponent},
  {path: 'entryTimes', component: EntryTimesComponent}
];

