import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Attachment } from '../../../shared/interfaces/test-interfaces/attachment.interface';

interface ChangeStatus<T> {
  status: T;
  message: string;
  attachment?: Attachment;
}

@Component({
  selector: 'change-status-form',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
  ],
  templateUrl: './change-status-form.component.html',
  styleUrls: ['./change-status-form.component.scss'],
})
export class ChangeStatusFormComponent<T> implements OnInit {

  @Input() currentStatus!: T;
  @Input() statuses!: T[]; 
  @Output() statusChanged = new EventEmitter<ChangeStatus<T>>(); 

  selectedStatus!: T;
  attachment?: Attachment;
  statusMessage: string = '';
  showConfirmation = false;
  showTransitionMessage = false;
  transitionMessage: string = '';
  errorMessage = false;

  ngOnInit(): void {
    this.selectedStatus = this.currentStatus;
  }

  onFileChange(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length) {
      const file = input.files[0];
      this.attachment = {
        name: file.name,
        file: file,
        url: URL.createObjectURL(file)
      };
    }
  }

  download() {
    if (this.attachment && this.attachment.url) {
      const link = document.createElement('a');
      link.href = this.attachment.url;
      link.download = this.attachment.name;
      link.click();
    }
  }

  delete(fileInput: HTMLInputElement) {
    this.attachment = undefined;
    this.showConfirmation = false;
    fileInput.value = '';
  }

  confirmChange() {
    if (this.statusMessage !== '') {
      this.showConfirmation = true;
      return;
    }
    this.errorMessage = true;
    setTimeout(() => {
      this.errorMessage = false;
    }, 3000);
  }

  cancelChange() {
    this.showConfirmation = false;
  }

  applyChange() {
    this.showConfirmation = false;
    this.showTransitionMessage = true;
    this.transitionMessage = `Estatus cambiado a ${this.selectedStatus}.`;
    
    this.statusChanged.emit({ 
      status: this.selectedStatus, 
      message: this.statusMessage,
      attachment: this.attachment
    });

    setTimeout(() => {
      this.showTransitionMessage = false;
    }, 3000);
  }

  dismissConfirmation() {
    this.showConfirmation = false;
  }

  clearForm() {
    this.selectedStatus = this.currentStatus;
    this.statusMessage = '';
    this.showConfirmation = false;
  }
}
