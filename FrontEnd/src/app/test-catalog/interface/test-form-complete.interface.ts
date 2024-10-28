import { EventEmitter } from "@angular/core";
import { Test } from "../../shared/interfaces/test-interfaces/test.interface";
import { FormGroup } from "@angular/forms";
import { Attachment } from "../../shared/interfaces/test-interfaces/attachment.interface";

export interface testFormCompleteInterface{

    /**
     * Const to now if the form is for edit or Add
     */
    addEdit: string;

    /**
     * The var serve for save url to download the profile
     */

    profileAttachment?: Attachment;

    
    /**
     * Emiiter of the test  when form is submited
     */
    formSubmit: EventEmitter<Test>;


    /**
     * Emiiter when cancel the form
     */
    formCancel: EventEmitter<void>;

    /**
     * Form group for Reactive Form
     */
    testForm: FormGroup;

    /**
     *  Data of Form
     */
    testData: Test;


    /**
     * If edit a row this function Fill all the fields
     * @type Void
     * @example
     *  fillForm() {
            if(!this.testData)
                return;
            this.testForm = this.fb.group({
            id: [this.testData.id,{ value: null, disabled: true }],
            name: [this.testData.name, Validators.required],
            desc: [this.testData.desc, Validators.required],
            start: [fechaYYYMMDD,''],
            end: [fechaYYYMMDDEnd,''],
            idRequest: [this.testData.idRequest],
            profile: [this.testData.profile],
            specialInstructions:[this.testData.specialInstructions],
            selectedRow: [null]
            });
            this.testForm.get('end')?.setValidators([this.endDateValidator.bind(this)]);

        }
     */
    fillForm():void;

    /**
     * Init the form
     * @type Void
     * @example
     * initReactiveForm(){
        this.testForm = this.fb.group({
        id: [{ value: null, disabled: true }],
        name: ['', Validators.required],
        desc: ['', Validators.required],
        start: [''],
        end: [''],
        idRequest: [null],
        profile: [null],
        specialInstructions:[null],
        selectedRow: [null]
    });
    this.testForm.get('end')?.setValidators([this.endDateValidator.bind(this)]);

  }
     * 
     */
    initReactiveForm():void;

    /**
     * The principal Form have subForms for select in table or something
     * This Function Fill this subForms
     * @example
     *  fillBaseForms(){
    
            this.usersService.getEmployeesByType(EmployeeType.Engineer).subscribe({
            next: (employee) =>{
                this.engs = employee

            },
            error: (erorr) =>{

            },
            complete: () => { }
            });
        }
     */
    fillBaseForms():void;


    /**
     * This Fuction Submit the principal Form
     * @example
         if (this.testForm.valid) {
        const test: Test = {
            id: this.testForm.get('id')?.value,
            name: this.testForm.get('name')?.value,
            desc: this.testForm.get('desc')?.value,
            start: this.testForm.get('start')?.value ? new Date(this.testForm.get('start')?.value) : undefined,
            end: this.testForm.get('end')?.value ? new Date(this.testForm.get('end')?.value) : undefined,
            specialInstructions: this.testForm.get('specialInstructions')?.value,
            profile: this.testForm.get('profile')?.value, // Actualizado aquí
            Lastupdated: new Date().toISOString(),
            LastUpdatedBy: 1, // Aquí deberías usar el ID del usuario actualmente autenticado
            LastUpdatedMessage: 'Updated test form'
        };
    
        // Emite el evento formSubmit con el formulario válido
        this.formSubmit.emit(test);
        }
     */
    onSubmit():void;
}