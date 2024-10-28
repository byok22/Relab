import { Component, Input, Output, EventEmitter, OnInit, SimpleChanges, OnChanges } from '@angular/core';
import { SelectOption } from '../../interfaces/select-option.interface';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'select-dropdown-2',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule
  ],
  templateUrl: './select-dropdown-2.component.html',
  styleUrls: ['./select-dropdown-2.component.css']
})
export class SelectDropdownComponent2 implements OnInit, OnChanges{
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
  @Input() label: string = '';
  @Input() options: SelectOption[] = [];
  @Output() onChange = new EventEmitter<string>();

  

  selectId: string = `select-${Math.random().toString(36).substring(2, 9)}`;
  @Input() selectedOption: string ='';

  ngOnChanges(changes: SimpleChanges) {
    if (changes['options'] || changes['selectedOption']) {
      // Handle changes in options or selected option
      console.log('Options or selectedOption changed:', this.options, this.selectedOption);
    }
  }

}
