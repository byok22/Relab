/**
 * Interfaces que Sirve para cualquier elemento que estara en el menu, por lo regular es un elemento del array del menu
 * @type 'dropdown' | 'multi-select' | 'calendar' | 'button' | 'textbox'
 */
export interface GenericMenuInterface{
    item: any;
    labelText: string;
    order:number;
    type: 'dropdown' | 'multi-select' | 'calendar' | 'button' | 'textbox';
}