import { Injectable } from '@angular/core';
import { Apollo, gql } from 'apollo-angular';
import {  Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { SelectOption } from '../../shared/interfaces/select-option.interface';
import { EquipmentDto } from '../interfaces/equipment-dto';
import { GenericResponse } from '../../shared/interfaces/response/generic-response';
import { DateStringService } from '../../shared/services/date-string.service';

@Injectable({
  providedIn: 'root'
})
export class EquipmentService {

  constructor(private apollo: Apollo,
    private dateStringService: DateStringService
  ) { }

  createEquipment(equipment: EquipmentDto): Observable<GenericResponse> {
    const addEquipmentMutation = gql`
      mutation AddEquipment($equipment: EquipmentInput) {
        equipmentMutation {
          addEquipment(equipment: $equipment) {
           message
            isSuccessful
          }
        }
      }
    `;   

    const calibration = this.formatDateToYYYYMMDD( new Date(equipment.calibrationDate));
    
    const inputEquipment: EquipmentDto = {
      name: equipment.name,
      calibrationDate: this.formatToISOString( calibration),
      description: equipment.description,
      id: 0
    };
       // Convertir el objeto a una cadena JSON
       const jsonString = JSON.stringify(inputEquipment);



    return this.apollo.use('equipmentService').mutate({
      mutation: addEquipmentMutation,
      variables: {
      equipment: inputEquipment
      }
    }).pipe(
      map((result: any) => result.data.equipmentMutation.addEquipment)
    );
  };


  updateEquipment( equipment: EquipmentDto) :Observable<GenericResponse>{
    const updateEquipmentMutation = gql`
      mutation($equipment: EquipmentInput){
        equipmentMutation{
          updateEquipment(equipment: $equipment){
            message
            isSuccessful
          }
        }
      }
    `;

    const calibration = equipment.calibrationDate==''?'2024-01-01': equipment.calibrationDate;
    
    const inputEquipment: EquipmentDto = {
      name: equipment.name,
      calibrationDate: this.formatToISOString( calibration),
      description: equipment.description,
      id: equipment.id
    };

     // Convertir el objeto a una cadena JSON
     const jsonString = JSON.stringify(inputEquipment);


    return this.apollo.use('equipmentService').mutate({
      mutation: updateEquipmentMutation,
      variables: {
        equipment: inputEquipment
      }
    }).pipe(
      map((result: any) => result.data.equipmentMutation.updateEquipment)
    );
  }
  /**
   * 
   * @returns Regresa un Observable con los estatus de los equipos
   */
  getStatus(): Observable<SelectOption[]> {
    return this.apollo.use('equipmentService').query({
      query: gql`
         query{
            equipmentQuery{
              equipmentDropDown{
                id
                text
              }
            }
          }
      `
    }).pipe(
      map((result: any) => result.data.equipmentQuery.equipmentDropDown)
    );
  }

  getEquipmentByID(equipmentId: number): Observable<EquipmentDto> {
  return this.apollo.use('equipmentService').query({
    query: gql`
      query GetEquipmentByID($id: Int!) {
        equipmentQuery {
          equipment(equipmentId: $id) {
            id
            name
            calibrationDate
            description
          }
        }
      }
    `,
    variables: {
      id: equipmentId
    }
  }).pipe(
    map((result: any) => this.transformToDto(result.data.equipmentQuery.equipment))
  );
}


  getEquipmentsByStatus(status: string): Observable<EquipmentDto[]> {
    return this.apollo.use('equipmentService').query({
      query: gql`
        query {
          equipmentQuery {
            equipments {
              id
              name
              calibrationDate
              description            
            }
          }
        }
      `,
      fetchPolicy: 'network-only'
    }).pipe(
      map((result: any) => this.transformToDtoArray(result.data.equipmentQuery.equipments))
    );
  }

  private transformToDto(equipment: any): EquipmentDto {
    return {
      id: equipment.id,
      name: equipment.name,
      calibrationDate: this.formatToISOString(equipment.calibrationDate),
      description: equipment.description
   //   employeeAccount: equipment.user?equipment.user.employeeAccount??'':'',
     // userName: equipment.user?equipment.user.userName??'':''
    };
  }

  private transformToDtoArray(equipments: any[]): EquipmentDto[] {
    return equipments.map(equipment => this.transformToDto(equipment));
  }

  formatDateToYYYYMMDD(date: Date|undefined): string {
    if(!date){
      return '';
    }
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  
  }

  formatToISOString(dateString: string): string {
    const date = new Date(dateString);
    if (isNaN(date.getTime())) {
      throw new Error('Invalid date string');
    }
    return date.toISOString();
}


}
  



