import { Injectable } from '@angular/core';
import { forkJoin, map, Observable, of } from 'rxjs';
import { SelectOption } from '../../shared/interfaces/select-option.interface';
import { TestTable } from '../interface/test-table.interface';
import { Test } from '../../shared/interfaces/test-interfaces/test.interface';
import { TestStatusEnum } from '../interface/test-status.enum';
import { ChangeStatus } from '../interface/change-status.';
import { Apollo, gql } from 'apollo-angular';
import { TestDto } from '../../shared/interfaces/test-interfaces/testdto.interface';
import { GenericResponse } from '../../shared/interfaces/response/generic-response';
import { EmployeeType } from '../../shared/enums/employee-type.enum';
import { HttpService } from '../../shared/interfaces/http-service';
import { Attachment } from '../../shared/interfaces/test-interfaces/attachment.interface';

@Injectable({
  providedIn: 'root'
})
export class TestsService {
  
  
  

  

  constructor(private apollo: Apollo,
    private httpService: HttpService
  ) { }


  //#region Form
  deleteEquipment(id: number, idEquipment: number): Observable<GenericResponse> {
    return this.apollo.use('apiGateWay').mutate({
      mutation: gql`
        mutation($idTest: Int, $idEquipment: Int) {
          testMutation {
            deleteEquipmentFromTest(idTest: $idTest, idEquipment: $idEquipment) {
              message
              isSuccessful
            }
          }
        }
      `,
      variables: {
        idTest: Number(id),
        idEquipment: Number(idEquipment)
      }
    }).pipe(
      map((result: any) => {
        return result.data.testMutation.deleteEquipmentFromTest;
      })
    );
  }

  deleteSpecification(id: number, idSpecification: number): Observable<GenericResponse> {
    return this.apollo.use('apiGateWay').mutate({
      mutation: gql`
        mutation($idTest: Int, $idSpecification: Int) {
          testMutation {
            deleteSpecificationFromTest(idTest: $idTest, idSpecification: $idSpecification) {
              message
              isSuccessful
            }
          }
        }
      `,
      variables: {
        idTest: Number(id),
        idSpecification: Number(idSpecification)
      }
    }).pipe(
      map((result: any) => {
        return result.data.testMutation.deleteSpecificationFromTest;
      })
    );
  }

  deleteAttachment(id: number, IdAttachment: number): Observable<GenericResponse> {
    return this.apollo.use('apiGateWay').mutate({
      mutation: gql`
        mutation($idTest: Int, $idAttachment: Int) {
          testMutation {
            deleteAttachmentFromTest(idTest: $idTest, idAttachment: $idAttachment) {
              message
              isSuccessful
            }
          }
        }
      `,
      variables: {
        idTest: Number(id),
        idAttachment: Number(IdAttachment)
      }
    }).pipe(
      map((result: any) => {
        return result.data.testMutation.deleteAttachmentFromTest;
      })
    );
  }
  async addAttachment(id: number, attachment: Blob): Promise<Observable<GenericResponse>> {
    const uploadedAttachment = await this.httpService.uploadFile('Attachment/uploadfile', attachment);

    const attachmentInput = {
      name: uploadedAttachment.name,
      url: uploadedAttachment.url,
      location: uploadedAttachment.location
    };

    return this.apollo.use('apiGateWay').mutate({
      mutation: gql`
        mutation($idTest: Int, $attachment: AttachmentInput) {
          testMutation {
            createAttachmentFromTest(idTest: $idTest, attachment: $attachment) {
              message 
              isSuccessful
            }
          }
        }
      `,
      variables: {
        idTest: id,
        attachment: attachmentInput
      }
    }).pipe(
      map((result: any) => {
        return result.data.testMutation.createAttachmentFromTest;
      })
    );
  }

  
  deleteProfile(idTest: number): Observable<GenericResponse> {
    return this.apollo.use('apiGateWay').mutate({
      mutation: gql`
        mutation($idTest: Int) {
          testMutation {
            deleteProfileFromTest(idTest: $idTest) {
              message
              isSuccessful
            }
          }
        }
      `,
      variables: {
        idTest: Number(idTest)
      }
    }).pipe(
      map((result: any) => {
        return result.data.testMutation.deleteProfileFromTest;
      })
    );
  }

  deleteTechnician(id: number, employeeId: number): Observable<GenericResponse> {
    return this.apollo.use('apiGateWay').mutate({
      mutation: gql`
        mutation($idTest: Int, $employeeId: Int) {
          testMutation {
            deleteTechnicianFromTest(idTest: $idTest,  idEmployee: $employeeId) {
              message
              isSuccessful
            }
          }
        }
      `,
      variables: {
        idTest: Number(id),
        employeeId: Number(employeeId)
      }
    }).pipe(
      map((result: any) => {
        return result.data.testMutation.deleteTechnicianFromTest;
      })
    );
  }


  //#endregion

  
// Mutación de actualización del test (con conversión a base64)
async update(test: Test): Promise<Observable<GenericResponse>> {

  if(test.profile && (test.profile.id == undefined || test.profile.id <= 0) && test.profile.file){
    const uploadedProfileFile = await this.httpService.uploadFile('Attachment/uploadfile', test.profile.file??new Blob());
    test.profile = uploadedProfileFile;
  }



  // Formatear la fecha de calibración a ISO string si existe
  const equipments = (test.equipments ?? []).map(equipment => {
    const parseDate = (dateString: string) => {
      const [datePart, timePart] = dateString.split(' ');
      const [day, month, year] = datePart.split('-').map(Number);
      const [hours, minutes, seconds] = timePart.split(':').map(Number);
      return new Date(year, month - 1, day, hours, minutes, seconds);
    };

    return {
      id: equipment.id,
      name: equipment.name,
      calibrationDate: equipment.calibrationDate ? parseDate(equipment.calibrationDate).toISOString() : null,
      description: equipment.description
    };
  });

  // Crear el objeto `test` con los archivos ya convertidos a base64
  const testS = {
    id: test.id,
    name: test.name,
    description: test.description,
    start: test.start,
    end: test.end,
    profile: {
      id: test.profile?.id,
      name: test.profile?.name,
      url: test.profile?.url, // Enviando solo la URL del archivo
      location: test.profile?.location
    },
    samples: {
      quantity: Number(test.samples?.quantity), // Asegurarse de que sea un número
      weight: Number(test.samples?.weight),     // Asegurarse de que sea un número
      size: Number(test.samples?.size)          // Asegurarse de que sea un número
    },
    specialInstructions: test.specialInstructions,
    enginner: {
      id: test.enginner?.id ?? 0,
      employeeNumber: test.enginner?.employeeNumber ?? '0',
      employeeType: test.enginner?.employeeType.toUpperCase() ?? EmployeeType.Engineer.toUpperCase(),
      name: test.enginner?.name ?? ''
    },
    technicians: test.technicians?.map(technician => ({
      id: technician.id,
      employeeNumber: technician.employeeNumber,
      employeeType: technician.employeeType.toUpperCase(),
      name: technician.name
    })),
    specifications: test.specifications?.map(specification => ({
      specificationName: specification.specificationName ?? '',
      details: specification.details
    })) ?? [],
    equipments: equipments,
    status: test.status.toUpperCase(),
    changeStatusTest: test.changeStatusTest
  };

  // Transformar a JSON
  var testJson = JSON.stringify(testS);
  return this.apollo.use('apiGateWay').mutate({
    mutation: gql`
      mutation($test: TestInput) {
        testMutation {
          updateTest(test: $test) {
            message
            isSuccessful
          }
        }
      }
    `,
    variables: {
      test: testS
    },fetchPolicy: 'no-cache'
  }).pipe(
    map((result: any) => {
      const results = result.data.testMutation.updateTest; 
      return  results;
    })
  );
}



   async create(test: Test): Promise<Observable<GenericResponse>> {
    

    //Update Files from profile and attachments and complete the test.profile and test.attachments wait before continue

     if(test.profile?.file){
      const uploadedProfileFile = await this.httpService.uploadFile('Attachment/uploadfile', test.profile.file);
      test.profile = uploadedProfileFile;
     }

     //await for all attachments
      if(test.attachments && test.attachments.length > 0){
        const attachments = await Promise.all(
          test.attachments.map(async attachment => {
            const uploadedAttachment = await this.httpService.uploadFile('Attachment/uploadfile', attachment.file?? new Blob());
            return uploadedAttachment;
          })
        );
        test.attachments = attachments;
      }
  

      //format calibration date to isostringdate from equipment if exist
      //format calibration date to isostringdate from equipment if exist
const equipments = (test.equipments ?? []).map(equipment => {
  const parseDate = (dateString: string) => {
    const [datePart, timePart] = dateString.split(' ');
    const [day, month, year] = datePart.split('-').map(Number);
    const [hours, minutes, seconds] = timePart.split(':').map(Number);
    return new Date(year, month - 1, day, hours, minutes, seconds);
  };

  return {
    id: equipment.id,
    name: equipment.name,
    calibrationDate: equipment.calibrationDate ? parseDate(equipment.calibrationDate).toISOString() : null,
    description: equipment.description
  };
});

      const testS = {
        id: test.id,
        name: test.name,
        description: test.description,
        start: test.start,
        end: test.end,
        samples: {
          quantity: Number(test.samples?.quantity), // Asegurarse de que sea un número
          weight: Number(test.samples?.weight),     // Asegurarse de que sea un número
          size: Number(test.samples?.size)          // Asegurarse de que sea un número
        },
        specialInstructions: test.specialInstructions,
        profile: {
          name: test.profile?.name,
          url: test.profile?.url, // Enviando solo la URL del archivo
          location: test.profile?.location
        },
        attachments: test.attachments?.map(attachment => ({
          name: attachment.name,
          url: attachment.url, // Enviando solo la URL del archivo
          location: attachment?.location
        })) || [],
        enginner: test.enginner,
        //maps test.technicians to technicians one by one prop
        technicians: test.technicians?.map(technician => ({
          id: technician.id ?? 0,
          employeeNumber: technician.employeeNumber ?? '0',
          employeeType: technician.employeeType.toUpperCase() ?? EmployeeType.Technician.toUpperCase(),
          name: technician.name ?? ''
        })) ?? [],
        specifications: test.specifications,
        equipments: equipments,
        status: test.status.toUpperCase(),
        changeStatusTest: test.changeStatusTest
      };
    

    // transfort to json
    var testJson = JSON.stringify(testS);
    return this.apollo.use('apiGateWay').mutate({
      mutation: gql`
        mutation($test: TestInput) {
          testMutation {
            addTest(test: $test) {
              message 
              isSuccessful
            }
          }
        }
      `,
      variables: {
        test: testS
      }
    }).pipe(
      map((result: any) => {
        const createdTest = result.data.testMutation.addTest;
        return createdTest;
      })
    );
  }

  getTestByID(id: number): Observable<Test>
  {

    return this.apollo.use('apiGateWay').query({
      query: gql`
       query($id:  Int){
        testQuery{
          getTestById(id: $id){
            id
            name
            description
            start
            end
            samples{
              quantity
              weight
              size
            }
            
            specialInstructions
            profile { 
              id             
              name
              url
            }
            attachments { 
              id             
              name
              url
            }
            enginner{
              id
              employeeNumber       
              employeeType
              name
            }
            technicians {
               id
              employeeNumber       
              employeeType
              name
            }
            specifications {
              specificationName
            }
            equipments {
              id
              name
            }
            status
            changeStatusTest {
              status
            }
            
          }
        }
      }
      `,
      variables: 
        {
          id : id
        },
        fetchPolicy: 'no-cache'
    }).pipe(
      map((result: any) => {
        console.log(result);
        const resultado = result.data.testQuery.getTestById;
        return this.transFormToDtoTest(resultado);

      })
    );
  }
  getTestsByStatus(status: string): Observable<TestTable[]> {
    if( status == ''){
      status = 'NEW';

    }
    return this.apollo.use('apiGateWay').query({
      query: gql`
        query($status:  TestStatusEnum){
        testQuery{
          getAllTestByStatus(status: $status){
            id
            name
            description
            start
            end
            samples{
              quantity
              weight
              size
            }
            
           
            enginner{
              employeeNumber       
              employeeType
              name
            }
              attachmentsCount
              techniciansCount
              specificationsCount
              equipmentsCount
           
            status
            changeStatusTest {
              status
            }
            
          }
        }
      }
      `,
      variables: 
        {
          status : status.toUpperCase()
        },
        fetchPolicy: 'no-cache'
    }).pipe(
      map((result: any) => {
        const resultado = result;
        return this.transformToDtoArray(resultado.data.testQuery.getAllTestByStatus)

      })
    );
  }
  
  getStatus(): Observable<SelectOption[]> {

      const selecs: SelectOption[] =
      [
        {
          id: TestStatusEnum.New,
          text: 'New'
        },
        {
          id: TestStatusEnum.Pending,
          text: 'Pending'
        },
        {
          id: TestStatusEnum.Approved,
          text: 'Approved'
        },
        {
          id: TestStatusEnum.Rejected,
          text: 'Rejected'
        },
        {
          id: TestStatusEnum.Cancelled,
          text: 'Cancelled'
        },
        {
          id: TestStatusEnum.Completed,
          text: 'Completed'
        },
        {
          id: TestStatusEnum.Ended,
          text: 'Ended'
        },
        {
          id: TestStatusEnum.InProgress,
          text: 'In Progress'
        }
      ]; 
      return of(selecs);
  }

  addStatusChangeToTest(status: ChangeStatus): Observable<GenericResponse>{


    /*
    export interface ChangeStatus {
    status: TestStatusEnum;
    message: string;
    attachment?: Attachment;
    idUser?: number;
    idTest?: number;
    
 }
    */

    const testStatus = {
      
      status: status.status.toUpperCase(),
      message: status.message,
      attachment: status.attachment?? null,
      idUser: status.idUser,
      idTest: status.idTest

    };

    //convert to json
    var statusJson = JSON.stringify(testStatus);
      

    return this.apollo.use('apiGateWay').mutate({
      mutation: gql`
        mutation($statusChange: ChangeStatusTestInput)
        {
          testMutation{
            changeStatusTest(statusChange: $statusChange){
              message
              isSuccessful
            }
          }
        }
      `,
      variables: {
        statusChange: testStatus
      }
    }).pipe(
      map((result: any) => {
        return result.data.testMutation.changeStatusTest;
      })
    );
  }


  private transFormToDtoTest(test: TestDto): Test {


    let testT: Test = {
        id: test.id ?? 0,
        name: test.name ?? '',
        description: test.description ?? '',
        start: test.start ? new Date(test.start) : undefined,
        end: test.end ? new Date(test.end) : undefined,
        specifications: test.specifications ?? [], // Inicializar como array vacío
        equipments: test.equipments ?? [],         // Inicializar como array vacío
        samples: test.samples,
        specialInstructions: test.specialInstructions ?? '',
        enginner: test.enginner,        
        idRequest: test.idRequest ?? 0,
        profile: test.profile,
        attachments: test.attachments ?? [],       // Inicializar como array vacío        
        lastUpdatedMessage: test.lastUpdatedMessage ?? '',
        createdAt: test.createdAt ?? '',
        createdBy: test.createdBy ?? 0,
        status: test.status ?? TestStatusEnum.New,
        changeStatusTest: test.changeStatusTest ?? []    // Inicializar como array vacío
    };
    
    testT.equipments =[];
    testT.technicians =[];
    testT.attachments =[];
    testT.specifications =[];
    testT.changeStatusTest =[];
    testT.updates =[];

    if(test.changeStatusTest&&test.changeStatusTest?.length>0)
    {
      test.changeStatusTest.forEach((tech)=>{
        testT.changeStatusTest?.push(tech);
      });
    }

    if(test.updates&&test.updates?.length>0)
    {
      test.updates.forEach((tech)=>{
        testT.updates?.push(tech);
      });
    }

    if(test.attachments&&test.attachments?.length>0)
    {
      test.attachments.forEach((tech)=>{
        testT.attachments?.push(tech);
      });
    }
    if(test.technicians&&test.technicians?.length>0)
    {
      test.technicians.forEach((tech)=>{
        testT.technicians?.push(tech);
      });
    }
    if(test.specifications&&test.specifications?.length>0)
    {
      test.specifications.forEach((tech)=>{
        testT.specifications?.push(tech);
      });
    }
    if(test.equipments&&test.equipments?.length>0)
    {
      test.equipments.forEach((tech)=>{
        testT.equipments?.push(tech);
      });
    }
    

    

    return testT;
}

  
  private transformToDto(test: TestDto): TestTable {
    const testTable: TestTable={
      id:1
    };

    testTable.id = test.id ?? 0;
    testTable.name = test.name;
    testTable.description = test.description??'N/A';
    testTable.start = test.start;
    testTable.end = test.end;
    testTable.specifications = test.specificationsCount??0;
    testTable.enginner = test.enginner?.name??'N/A';
    testTable.equipments = test.equipmentsCount??0;
    testTable.specialInstructions = test.specialInstructions;
    testTable.technicians = test.techniciansCount?.toString()??'0';
    testTable.attachments = test.attachmentsCount??0;
    testTable.lastUpdatedBy = test.lastUpdatedBy?.toString()??'N/A';
    testTable.status = test.status;
    testTable.lastUpdatedMessage = test.lastUpdatedMessage??'N/A';


    return testTable;
  }

  private transformToDtoArray(test: any[]): TestTable[] {
    return test.map(test => this.transformToDto(test));
  }

  // Método para convertir el archivo a base64, usando un flujo pseudo-sincrónico
toBase64(file: Blob): string {
  let result = "";
  const reader = new FileReader();
  reader.readAsDataURL(file);
  
  // Asignar el valor directamente al final de la lectura
  reader.onloadend = () => {
    result = reader.result as string;
  };
  
  return result; // Esto devolverá una cadena vacía inicialmente (sigue siendo asíncrono).
}


 

}
