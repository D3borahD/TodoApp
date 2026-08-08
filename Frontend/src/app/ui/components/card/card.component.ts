import { Component } from '@angular/core';
import {DatePipe, TitleCasePipe} from '@angular/common';
import {MatCardFooter} from '@angular/material/card';
import {MatIcon} from '@angular/material/icon';
import {ButtonComponent} from '../button/button.component';

@Component({
  selector: 'app-card',
  imports: [
    DatePipe,
    MatCardFooter,
    MatIcon,
    TitleCasePipe,
    ButtonComponent
  ],
  templateUrl: './card.component.html',
  styleUrl: './card.component.scss',
})
export class CardComponent {

}
