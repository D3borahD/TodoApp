import {Component, input} from '@angular/core';
import {UpperCasePipe} from '@angular/common';
import {MatIcon} from '@angular/material/icon';
import {IButton} from './button.interface';

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
  button = input.required<IButton>()

  protected onClick(): void {
    this.button().action();
  }
}
