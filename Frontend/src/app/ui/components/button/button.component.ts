import { Component } from '@angular/core';
import {UpperCasePipe} from '@angular/common';
import {MatIcon} from '@angular/material/icon';

@Component({
  selector: 'app-button',
  imports: [
    UpperCasePipe,
    MatIcon
  ],
  templateUrl: './button.component.html',
  styleUrl: './button.component.scss',
})
export class ButtonComponent {

  action = 'button works!';

}
