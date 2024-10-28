import { Injectable } from '@angular/core';
import { Observable, map, of } from 'rxjs';
import { TestRequest } from '../../shared/interfaces/test-request-interfaces/test-request.interface';
import { TestRequestDto } from '../../shared/interfaces/test-request-interfaces/test-request-dto.interface';
import { Test } from '../../shared/interfaces/test-interfaces/test.interface';
import { ChangeStatusTestRequest } from '../../test-catalog/interface/change-status-test-request';
import { Apollo, gql } from 'apollo-angular';
import { TestRequestEnum } from '../Enums/test-request.enum';
import { GenericInserUpdateResponse } from '../../shared/interfaces/response/generic-inser-update-response.';
import { TestRequestInput } from '../../shared/interfaces/test-request-interfaces/test-request-input.interface';
import { TransformInterfacesService } from '../../shared/services/transform-interfaces.service';

@Injectable({
  providedIn: 'root'
})
export class TestRequestService {


  
  addStatusChangeToTestRequest(idTemp: number, $event: ChangeStatusTestRequest): Observable<GenericInserUpdateResponse>  {
    const response: GenericInserUpdateResponse = {
      isSuccessful :true,
      message: 'Status Actualizado'
    }
    return of(response);
  }


  constructor( private apollo: Apollo, private transformService: TransformInterfacesService) { }

    create(testRequestOrigin: TestRequestDto): Observable<GenericInserUpdateResponse> {
      const createTestRequestMutation = gql`
        mutation($testRequest: TestRequestInput!) {
          testRequestMutation {
            addTestRequest(testRequest: $testRequest) {
              message
              isSuccessful
            }
          }
        }
      `;

      const formatToISOString = (dateString: string): string => {
        const date = new Date(dateString);
        return date.toISOString();
      };
    
      // Convertir a JSON con las fechas en formato ISO
      const ConttestRequest: TestRequestInput = {
        id: testRequestOrigin.id<=0?0:testRequestOrigin.id,
        status: testRequestOrigin.status.toUpperCase(),
        description: testRequestOrigin.description,
        start: formatToISOString(testRequestOrigin.start),  // Convertir la fecha
        end: formatToISOString(testRequestOrigin.end ?? ''),  // Convertir la fecha o dejar vacío  
        tests: testRequestOrigin.tests?.map((test) => this.transformService.transformTesttoTestDto(test)) ?? [], // Usar map en lugar de forEach             
      };
    
      // Convertir el objeto a una cadena JSON
      const jsonString = JSON.stringify(ConttestRequest);
    
      return this.apollo.use('apiGateWay').mutate({
        mutation: createTestRequestMutation,
        variables: {
          testRequest: ConttestRequest  // Enviar el JSON como objeto
        }
      }).pipe(
        map((result: any) => {
          const results = result.data.testRequestMutation.addTestRequest;
          return results;
        })
      );
    }
  
  
  update(testRequestOrigin: TestRequestDto):Observable<GenericInserUpdateResponse> {

    const createTestRequestMutation = gql`
        mutation($testRequest: TestRequestInput){
        testRequestMutation{
          updateTestRequest(testRequest: $testRequest){
            message
            isSuccessful
          }
        }
}
      `;

      const formatToISOString = (dateString: string): string => {
        const date = new Date(dateString);
        return date.toISOString();
      };
    
      // Convertir a JSON con las fechas en formato ISO
      const ConttestRequest: TestRequestInput = {
        id: testRequestOrigin.id<=0?0:testRequestOrigin.id,
        status: testRequestOrigin.status.toUpperCase(),
        description: testRequestOrigin.description,
        start: formatToISOString(testRequestOrigin.start),  // Convertir la fecha
        end: formatToISOString(testRequestOrigin.end ?? ''),  // Convertir la fecha o dejar vacío  
        tests: testRequestOrigin.tests?.map((test) => this.transformService.transformTesttoTestDto(test)) ?? [], // Usar map en lugar de forEach             
      };
    
      // Convertir el objeto a una cadena JSON
      const jsonString = JSON.stringify(ConttestRequest);
    
      return this.apollo.use('apiGateWay').mutate({
        mutation: createTestRequestMutation,
        variables: {
          testRequest: ConttestRequest  // Enviar el JSON como objeto
        }
      }).pipe(
        map((result: any) => {
          const results = result.data.testRequestMutation.addTestRequest;
          return results;
        })
      );

  }

  /*getTestsFromRequest(idRequest:number):Observable<Test[]>{
    const body={
      idRequest
      
    };
    return this.http
    .post<Test[]>(`${this.baseURL}/api/TestRequest/bystatusandprocess`, body);
   
  }

  getAllTestRequest(): Observable<TestRequestDto[]> {
    return this.http
      .get<TestRequest[]>(`${this.baseURL}/api/TestRequest/all`)
      .pipe(
        map(testRequests => testRequests.map(testRequest => this.transformToDto(testRequest)))
      );
  }*/

  getTestRequest(status:string): Observable<TestRequestDto[]> {
   

   if( status == ''){
      status = 'NEW';

    }
    return this.apollo.use('apiGateWay').query({
      query: gql`
         query($status:TestRequestsStatusEnum ){
          testRequestQuery{
            testRequestsByStatus(status: $status){
              id
              status
              description
              start
              end
            
              createdBy{
                id
                userName
              }
             testsCount                       
            }
          }
        }
      `,
      variables: 
        {
          $status : status
        }
    }).pipe(
      map((result: any) => {
        const resultado = result;
        return this.transformToDtoArray(resultado.data.testRequestQuery.testRequestsByStatus)

      })
    );

   
  }

  getTestRequestById(id: number): Observable<TestRequestDto> {
    //completar
    return this.apollo.use('apiGateWay').query({
      query: gql`
     query($id: Int){
      testRequestQuery{
        getTestRequestsById(id:$id)
        {
          id
          status
          description
          start
          end
          testsCount
          active
          createdBy{
            id
            userName
            employeeAccount
            email
          }
          tests{
            id
            name
            description
            start
            end
            samples{
              id
              quantity
              weight
              size
            }
            specialInstructions
            profile{          
              name
              url          
            }
            attachments{
              name
              url
            }
            enginner
            {
              id
              employeeNumber
              name
              employeeType
            }
            technicians{
              id
              employeeNumber
              name
              employeeType
              
            }
            specifications{
              specificationName
              details
            }
            equipments{
              name
              id
              description
              calibrationDate
            }
            status
            lastUpdatedMessage
            idRequest
          }
        
          createdAt
        }
      }
        
      
    }
      `,
      variables: {
        id: id
      }
    }).pipe(
      map((result: any) => {
        const resultado = result;
        return this.transformToDto(resultado.data.testRequestQuery.getTestRequestsById)
      })
    );
    
  }


   
  private transformToDtoArray(test: TestRequest[]): TestRequestDto[] {
    return test.map(test => this.transformToDto(test));
  }

  private transformToDto(testRequest: TestRequest): TestRequestDto {

    const lastUpdate = testRequest?.updates && testRequest.updates.length > 0 ? testRequest.updates.reduce((latest, update) => {
      return update.updatedAt > latest.updatedAt ? update : latest;
    }, testRequest.updates[0]) : undefined;

    const testRequestDto: TestRequestDto = {
      id: 0,
      status: TestRequestEnum.New,
      user: '',
      description: '',
      active: false,
      start: '',
      lastUpdatedBy: '',
      lastUpdatedMessage: '',
      createdAt: '',
      testsCount: 0,

    };
    if (!testRequest) {
      throw new Error('testRequest is undefined');
    }

    testRequestDto.id = testRequest.id ?? 0;
    testRequestDto.status = testRequest.status ?? TestRequestEnum.New;
    testRequestDto.active = testRequest.active ?? false;
    testRequestDto.createdAt = testRequest.createdAt ? testRequest.createdAt.toString() : Date.now().toString();
    testRequestDto.createdBy = testRequest.createdBy?.employeeAccount ?? 'N/A';
    testRequestDto.start = testRequest.start ? testRequest.start.toString() : '';
    testRequestDto.end = testRequest.end ? testRequest.end.toString() : '';
    testRequestDto.testsCount = testRequest.tests?.length ?? 0;
    testRequestDto.lastupdated = (lastUpdate?.updatedAt ?? new Date()).toString();
    testRequestDto.lastUpdatedBy = lastUpdate?.user?.employeeAccount ?? '';
    testRequestDto.lastUpdatedMessage = lastUpdate?.message ?? '';
    testRequestDto.user = testRequest.createdBy?.userName ?? '';
    testRequestDto.description = testRequest.description ?? '';
    testRequestDto.tests = testRequest.tests ?? [];
    testRequestDto.testsCount = testRequest.testsCount ?? 0;
    testRequestDto.updates = testRequest.updates ?? [];

    return testRequestDto;
  }
  parseDate(dateString: string): Date|undefined {
    if(dateString==''|| dateString == undefined || dateString == null){
      return  undefined;
    }
    const [datePart, timePart] = dateString.split(' ');
    const [day, month, year] = datePart.split('-').map(Number);
    const [hours, minutes, seconds] = timePart.split(':').map(Number);
    return new Date(year, month - 1, day, hours, minutes, seconds);
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
}
