import { FormGroup, Validators } from "@angular/forms";
import { SelectOption } from "../../interfaces/select-option.interface";

export interface GenericFormInterface<T> {

    tittle: string;
    data?: T;
    editAdd:string;
    fields:GenericFormFieldsInterface []; 
    customFromGroup?: FormGroup;
    submitFunction?: any;  
    customButton?: string;
}

export interface GenericFormFieldsInterface {

    field:string;
    /**
     * Cuando es un Select aqui mandas el id del select, si no el texto
     */
    value?:string|number|Date|boolean|Blob;
    label:string;
    type:string;
    /**
     * Aqui se ponen las opciones que tendra el Select
     */
    options?: SelectOption[];
    show?:boolean;
    enable?: boolean;
    validationRequired: boolean;
    required:boolean;
    validatorType?: Validators;   
    order:number;
    funcion?: any;
    onInputChange?: any;

}
