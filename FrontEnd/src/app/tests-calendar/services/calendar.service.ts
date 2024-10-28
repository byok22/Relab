import { Injectable } from '@angular/core';
import { HttpService } from '../../shared/interfaces/http-service';
import { HttpClient } from '@angular/common/http';
import { Observable, map, of } from 'rxjs';
import { Test } from '../../shared/interfaces/test-interfaces/test.interface';
import { GenericResponse } from '../../shared/interfaces/response/generic-response';
import { Apollo, gql } from 'apollo-angular';

@Injectable({
  providedIn: 'root'
})
export class CalendarService  {

  constructor(private apollo: Apollo) {
   
  }

  getAllTests(): Observable<Test[]> {
   
    return this.apollo.use('apiGateWay').query({
      query: gql`
       query{
        testQuery{
          getAllTest{
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
              file
            }
            attachments {
              file
            }
            enginner{
              employeeNumber       
              employeeType
              name
            }
            technicians {
              employeeType
            }
            specifications {
              specificationName
            }
            equipments {
              id
            }
            status
            changeStatusTest {
              status
            }
            
          }
        }
      }
      `
      
    }).pipe(
      map((result: any) => {
        const resultado = result;
       // console.log(resultado);
        return result.data.testQuery.getAllTest;
       

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
        }
    }).pipe(
      map((result: any) => {
        console.log(result);
        const resultado = result.data.testQuery.getTestById;
        return resultado;

      })
    );
  }
  getTestsByStatus(status: string): Observable<Test[]> {
    if( status == ''){
      status = 'NEW';

    }

    //dont use cache
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
        fetchPolicy: 'no-cache',
    }).pipe(
      map((result: any) => {
        const resultado = result.data.testQuery.getAllTestByStatus;
        return resultado;

      })
    );
  }

  updateTestDates(
    idTest: number,
    startDate: string,
    endDate: string
  ): Observable<GenericResponse> {
    return this.apollo.use('apiGateWay').mutate({
      mutation: gql`
        mutation($idTest: Int, $startDate: String, $endDate: String) {
          testMutation {
            updateDatesTest(idTest: $idTest, startDate: $startDate, endDate: $endDate) {
              message
              isSuccessful
            }
          }
        }
      `,
      variables: {
        idTest: idTest,
        startDate: startDate,
        endDate: endDate
      }
    }).pipe(
      map((result: any) => {
        const response = result.data.testMutation.updateDatesTest;
        return response;
      })
    );
  }

}
