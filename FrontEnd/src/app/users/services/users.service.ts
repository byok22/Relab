import { Injectable } from '@angular/core';
import { Apollo, gql } from 'apollo-angular';
import { map, Observable, of } from 'rxjs';
import { SelectOption } from '../../shared/interfaces/select-option.interface';
import { User } from '../../shared/interfaces/UsersInterfaces/user.interface';
import { LdapUserInterface } from '../interfaces/ldap-user.interface';
import { EmployeeType } from '../../shared/enums/employee-type.enum';
import { query } from 'express';
import { Employee } from '../../shared/interfaces/employees/employee.interface';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  http: any;
  getUserByID(id: number):Observable<User> {
    const user: User ={
      employeeAccount:'3524661',
      id: id,
      userName:'UserName',
      email:'UserEmail'
    }
    return of(user);
  }  

  updateUser(user: User): Observable<User> {
    const addUserMutation = gql`
      mutation UpdateUser($user: UserInput){
        userMutation{
          updateUser(user:$user){
            id
            userName
            employeeAccount
          }
        }
      }
    `;
    const inputUser = {     
      id: user.id,
      userName: user.userName,
      email: user.email ?? '',
      employeeAccount: user.employeeAccount // corregido desde employeeAccoint
    };  

    return this.apollo.use('userService').mutate({
      mutation: addUserMutation,
      variables: {
        user: inputUser
      }
    }).pipe(
      map((result: any) => result.data.userMutation.updateUser)
    );
  };
 

  createUser(user: User): Observable<User> {
    const addUserMutation = gql`
      mutation UpdateUser($user: UserInput){
        userMutation{
          addUser(user:$user){
            id
            userName
            employeeAccount
          }
        }
      }
    `;

    const inputUser = {     
      userName: user.userName,
      email: user.email??'',
      employeeAccount : user.employeeAccount
    };

    return this.apollo.use('userService').mutate({
      mutation: addUserMutation,
      variables: {
        user: inputUser
      }
    }).pipe(
      map((result: any) =>{
        console.log(result);
        return result.data.userMutation.addUser
        
      } )
    );
  };
 

  constructor(private apollo: Apollo) { }

  getProfiles():Observable<SelectOption[]> {

    return this.apollo.use('userService').query({
      query: gql`
        query Profiles{
          profileQuery{
            profiles{
              id
              name    
            }
          }
        }
      `
    }).pipe(
      map((result: any) => {      
        return this.transformToDtoArrayProfile(result.data.profileQuery.profiles);
      })
    );
   
    
  }

  getUsersByType(userType: EmployeeType): Observable<SelectOption[]> {
    return this.apollo.use('userService').query({
      query: gql`
        query UserQuery {
            userQuery {
                usersByType(userType: "${userType}") {
                    employeeAccount
                    userName
                }
            }
        }
      `
    }).pipe(
      map((result: any) => {
        console.log(result);
        return this.transformToDtoArray(result.data.userQuery.usersByType);
      })
    );
  }

  getUsersCompleteByType(userType: EmployeeType): Observable<User[]> {
    return this.apollo.use('userService').query({
      query: gql`
        query UserQuery {
            userQuery {
                usersByType(userType: "Eng") {
                    id
                    employeeAccount
                    userName
                    email
                }
            }
        }
      `
    }).pipe(
      map((result: any) => {
        if(!result.data.userQuery.usersByType||result.data.userQuery.usersByType.length == 0){
          return []
        }
        return this.transformToUserDtoArray(result.data.userQuery.usersByType);
      })
    );
  }

  getEmployeesByType(userType: EmployeeType): Observable<Employee[]> {

     let query : string = `
        query UserQuery {
            userQuery {
                usersByType(userType: "${userType.toString()}") {
                    id
                    employeeAccount
                    userName
                    email

                }
            }
        }
      `
     // console.log(query);
    return this.apollo.use('userService').query({
      query: gql`
         query {
         employeeQuery{
          employees{
            id
            employeeNumber
            name
            employeeType
          }
        }
        }
      `
    }).pipe(
      map((result: any) => {
        if(!result.data.employeeQuery.employees||result.data.employeeQuery.employees.length == 0){
          return []
        }
        return this.transformToEmployeeDtoArray(result.data.employeeQuery.employees);
      })
    );
  }

  getLdapUser( ntUser:string ):Observable<User>{

    let usrError: LdapUserInterface;

    if ( !ntUser ) throw Error ('user ntuser is required')
    const body ={
        ntUser
    };
    const user: User={
      employeeAccount: "",
      id: 1,
      userName: ''
    }
    return of(user);
    /*return this.http.post<LdapUserInterface>(`${ this.baseURL }/ldap/ldapuserbyntuser`,body)
        .pipe(
            catchError( err => of(usrError)),
            map( resp => resp)
        ) ;*/
}

private transformToDto(user: any): SelectOption {
  return {
    id: user.employeeAccount,
    text: user.userName,    
  };
}

private transformToDtoArrayProfile(profiles: any[]): SelectOption[] {
  console.log("profiles",profiles)
  return profiles.map(profile => this.transformToDtoProfiles(profile));
}


  private transformToDtoProfiles(user: any): SelectOption {
    return {
      id: user.id,
      text: user.name,    
    };
  }

  private transformToDtoArray(user: any[]): SelectOption[] {
    return user.map(equipment => this.transformToDto(equipment));
  }

  private transformToUserDto(user: any): User {
    return {
      id: user.id,
      userName: user.userName,    
      employeeAccount: user.employeeAccount,
       email: user.email
    };
  }

  private transformToUserDtoArray(users: any[]): User[] {
    return users.map(user => this.transformToUserDto(user));
  }

  private transformToEmployeeDto(user: Employee): Employee {
    return {
      id: user.id,
      employeeNumber: user.employeeNumber,
      name: user.name,
      employeeType: user.employeeType
    };
  }

  private transformToEmployeeDtoArray(employee: any[]): Employee[] {
    return employee.map(employee => this.transformToEmployeeDto(employee));
  }

}
