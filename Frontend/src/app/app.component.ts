import {ChangeDetectionStrategy, Component, signal} from '@angular/core';
import {RouterLink, RouterOutlet} from '@angular/router';
import {TitleCasePipe} from '@angular/common';

@Component({
    selector: 'app-root',
  imports: [
    RouterOutlet,
    TitleCasePipe,
    RouterLink
  ],
    changeDetection: ChangeDetectionStrategy.OnPush,
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
})
export class AppComponent {
  protected readonly title =  signal('kairos');
}
