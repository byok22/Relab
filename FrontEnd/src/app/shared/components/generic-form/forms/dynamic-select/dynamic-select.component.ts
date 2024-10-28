import { AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { GenericFormFieldsInterface } from '../../generic-form.interface';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'dynamic-select',
  standalone: true,
  imports:[ReactiveFormsModule, CommonModule],
  templateUrl: './dynamic-select.component.html',
  styleUrls: ['./dynamic-select.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DynamicSelectComponent implements OnInit, AfterViewInit {
  @Input() 
  customForm!: FormGroup;

  @Input()
  field!: GenericFormFieldsInterface;

  constructor(private cdr: ChangeDetectorRef) {}
  ngAfterViewInit(): void {
    setTimeout(() => {

       // Verificar si el control existe en el formulario
      if (this.customForm.get(this.field.field)) {
        this.customForm.patchValue({ [this.field.field]: this.field.value });
        this.cdr.markForCheck(); 
      }
    });
  }

  ngOnInit(): void {
   
  }

  hasError(controlName: string, errorName: string) {
    const control = this.customForm.get(controlName);
    return control?.invalid && (control?.dirty || control?.touched) && control?.hasError(errorName);
  }
}