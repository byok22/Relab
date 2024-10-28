import { Injectable } from '@angular/core';
import { Apollo, gql } from 'apollo-angular';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { SelectOption } from '../../shared/interfaces/select-option.interface';
import { GenericResponse } from '../../shared/interfaces/response/generic-response';
import { Employee } from '../../shared/interfaces/employees/employee.interface';
import { EmployeeType } from '../../shared/enums/employee-type.enum';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {

  constructor(private apollo: Apollo) { }

  createEmployee(employee: Employee): Observable<GenericResponse> {
    const addEmployeeMutation = gql`
      mutation($employee: EmployeeInput){
          employeeMutation{
            addEmployee(employee: $employee){
              message
              isSuccessful
            }
          }
        }
    `;

    const inputEmployee = {
      name: employee.name,
      employeeNumber: employee.employeeNumber,
      id: 0,
      employeeType: employee.employeeType.toUpperCase()
    };

    return this.apollo.use('apiGateWay').mutate({
      mutation: addEmployeeMutation,
      variables: {
        employee: inputEmployee
      }
    }).pipe(
      map((result: any) => {
        
        const results = result.data.employeeMutation.addEmployee;
        return results;
      }
      )
    );
  }

  updateEmployee(employee: Employee): Observable<GenericResponse> {
    const updateEmployeeMutation = gql`
     mutation($employee: EmployeeInput){
        employeeMutation{
          updateEmployee(employee: $employee){
            message
            isSuccessful
          }
        }
      }
    `;

    const inputEmployee = {
      name: employee.name,
      employeeNumber: employee.employeeNumber,
      id: employee.id,
      employeeType: employee.employeeType.toUpperCase()
    };

    const inputJson = JSON.stringify(inputEmployee);

    return this.apollo.use('apiGateWay').mutate({
      mutation: updateEmployeeMutation,
      variables: {
        employee: inputEmployee
      }
    }).pipe(

      /*
      {
  "data": {
    "employeeMutation": {
      "updateEmployee": {
        "message": "Update Employee",
        "isSuccessful": true
      }
    }
  }
}
      */
      map((result: any) => {
        const results = result.data.employeeMutation.updateEmployee;
        return results;
      })
    );
  }

  getypesEmployees(): Observable<SelectOption[]> {
    return this.apollo.use('apiGateWay').query({
      query: gql`
         query {
            employeeQuery {
              employeeDropDown {
                id
                text
              }
            }
          }
      `
    }).pipe(
      map((result: any) => result.data.employeeQuery.employeeDropDown)
    );
  }

  getEmployeeByNumber(employeeNumber: string): Observable<Employee> {
    return this.apollo.use('apiGateWay').query({
      query: gql`
       query($employeeNumber: String){
          employeeQuery{
            employee(employeeNumber: $employeeNumber){
              id
              employeeNumber
              name
              employeeType
            }
            
          }
        }
      `,
      variables: {
        employeeNumber: employeeNumber
      }
    }).pipe(
      map((result: any) => this.transformToDto(result.data.employeeQuery.employee))
    );
  }

  getEmployeesByStatus(employeeType: string): Observable<Employee[]> {
    return this.apollo.use('apiGateWay').query({
      query: gql`
       query($employeeType:EmployeeEnumType){
        employeeQuery{
          employeesByType(employeeType: $employeeType){
            id
            employeeNumber
            name
            employeeType
          }
        }
}
      `,
      variables: {
        employeeType: employeeType.toUpperCase()
      }
      ,
      fetchPolicy: 'network-only'
    }).pipe(
      map((result: any) => {
        const results = this.transformToDtoArray(result.data.employeeQuery.employeesByType?? []);
        return results;
      }
    
    )
    );
  }

  private transformToDto(employee: any): Employee {
    return {
      id: employee.id,
      name: employee.name,
      employeeNumber: employee.employeeNumber,
      employeeType: employee.employeeType
    };
  }

  private transformToDtoArray(employees: any[]): Employee[] {
    return employees.map(employee => this.transformToDto(employee));
  }
  getEmployeesByType(employeeType: EmployeeType): Observable<Employee[]> {
    return this.apollo.use('apiGateWay').query({
      query: gql`
          query GetEmployeesByType($type: EmployeeEnumType) {
          employeeQuery {
            employeesByType(employeeType: $type) {
              id
              employeeNumber
              name
              employeeType
            }
          }
        }
      `,
      variables: {
        type: employeeType.toUpperCase()
      },
      fetchPolicy: 'network-only'
    }).pipe(
      map((result: any) => this.transformToDtoArray(result?.data?.employeeQuery?.employeesByType || []))
    );
  }
}