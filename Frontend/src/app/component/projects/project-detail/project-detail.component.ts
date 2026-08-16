import {Component, inject, Signal} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {DatePipe, TitleCasePipe} from '@angular/common';

import {ProjectService} from '../../../core/services/project.service';
import {IProject} from '../../../core/models/project.model';


import {MatIcon} from '@angular/material/icon';
import {MatDialog} from '@angular/material/dialog';
import {StepFormComponent} from './step-form/step-form.component';


@Component({
  selector: 'app-project-detail',
  imports: [
    TitleCasePipe,
    DatePipe,
    MatIcon
  ],
  templateUrl: './project-detail.component.html',
  styleUrl: './project-detail.component.scss',
})
export class ProjectDetailComponent {
  private readonly projectService: ProjectService = inject(ProjectService);
  private readonly route = inject(ActivatedRoute);
  private readonly id :string = this.route.snapshot.params["id"];

  protected readonly project : Signal<IProject | null> = this.projectService.project;
  private readonly dialog = inject(MatDialog);

  constructor() {
    this.loadProject();
  }

  private loadProject(): void {
    this.projectService.getProjectById$(this.id).subscribe();

  }

  protected addStep():void {
    this.dialog.open(StepFormComponent,
      {
        height: 'auto',
        width: '500px'
      })
  }
}
