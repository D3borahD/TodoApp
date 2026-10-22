import {Component, computed, input} from '@angular/core';
import {ChipColor} from './chipColor';


@Component({
  selector: 'app-chips',
  imports: [],
  templateUrl: './chips.component.html',
  styleUrl: './chips.component.scss',
})
export class ChipsComponent {
  label = input.required<string>();
  color = input<ChipColor>('gray');
  isButton = input<boolean>(false)
  protected colorClass = computed(() => `chip--${this.color()}`);
}
