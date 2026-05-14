import {ChangeDetectionStrategy, Component} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {ReactiveFormsModule} from '@angular/forms';
import {MatIcon} from '@angular/material/icon';

@Component({
    selector: 'app-root',
  imports: [
    RouterOutlet,
    ReactiveFormsModule,
    MatIcon,
  ],
    changeDetection: ChangeDetectionStrategy.OnPush,
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss'
})
export class AppComponent {

}
