import {Component, computed, input} from '@angular/core';
import {STATUS_COLOR_MAP, StatusKey} from '../../../core/models/status.model';

@Component({
  selector: 'app-chips',
  imports: [

  ],
  templateUrl: './chips.component.html',
  styleUrl: './chips.component.scss',
})
export class ChipsComponent {
  chips = input.required<StatusKey>()
  protected colorClass = computed(() => STATUS_COLOR_MAP[this.chips()]);
}
