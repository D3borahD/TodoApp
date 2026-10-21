import {inject, Service} from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import {Observable} from 'rxjs';
import {IProject} from '../models/project.model';
import {ProjectDialogData} from '../models/projectForm';
import {ProjectFormComponent} from '../../component/projects/project-form/project-form.component';

@Service()
export class ProjectDialogService {
  private readonly dialog = inject(MatDialog);

  openCreate(): Observable<IProject | undefined> {
    return this.open({ mode: 'create' });
  }

  openEdit(project: IProject): Observable<IProject | undefined> {
    return this.open({ mode: 'edit', project });
  }

  private open(data: ProjectDialogData): Observable<IProject | undefined> {
    return this.dialog
      .open<ProjectFormComponent, ProjectDialogData, IProject>(ProjectFormComponent,
        { data,
          height: 'auto',
          width: '500px'
        })
      .afterClosed();
  }
}
