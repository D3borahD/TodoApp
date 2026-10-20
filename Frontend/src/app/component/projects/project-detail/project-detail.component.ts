import {Component, inject, Signal} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {DatePipe, TitleCasePipe} from '@angular/common';
import {ProjectService} from '../../../core/services/project.service';
import {IProject} from '../../../core/models/project.model';
import {MatIcon} from '@angular/material/icon';
import {MatDialog} from '@angular/material/dialog';
import StepFormComponent from './step-form/step-form.component';
import {StepService} from '../../../core/services/step.service';
import {ChipsComponent} from '../../../ui/components/chips/chips.component';
import {STATUS_COLOR_MAP} from '../../../core/models/status.model';
import {IStep} from '../../../core/models/step.model';


@Component({
  selector: 'app-project-detail',
  imports: [
    TitleCasePipe,
    DatePipe,
    MatIcon,
    ChipsComponent
  ],
  templateUrl: './project-detail.component.html',
  styleUrl: './project-detail.component.scss',
})
export class ProjectDetailComponent {
  private readonly route = inject(ActivatedRoute);
  private readonly dialog = inject(MatDialog);
  private readonly projectService: ProjectService = inject(ProjectService);
  private readonly stepService: StepService = inject(StepService);
  private readonly id :string = this.route.snapshot.params["id"];

  protected readonly statusColors = STATUS_COLOR_MAP;
  protected readonly project : Signal<IProject | null> = this.projectService.project;
  protected readonly stepList = this.stepService.steps;
  protected readonly projectTypes = this.stepService.projectTypes;

  constructor() {
    this.loadProject();
  }

  private loadProject(): void {
    this.projectService.getProjectById$(this.id).subscribe();
    this.stepService.getStepsByProjectId(this.id).subscribe();
  }

  protected addStep():void {
    this.dialog.open(StepFormComponent,
      {
        height: 'auto',
        width: '500px',
        data: {
          projectId: this.project()?.id
        }
      })
  }

  protected deleteStep$(projectId: string, stepId: string): void {
    this.stepService.deleteStep$(projectId, stepId).subscribe()
  }

  protected updateStep$(projectId: string, step: IStep) {
    this.dialog.open(StepFormComponent,
      {
        height: 'auto',
        width: '500px',
        data: {
          projectId: projectId,
          step: step
        }
      })
  }
}
