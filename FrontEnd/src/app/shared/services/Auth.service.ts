import { Injectable } from '@angular/core';
import { User } from '../interfaces/UsersInterfaces/user.interface';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  getLoggedUser(): User {
    const user: User ={
      employeeAccount: '',
      id: 0,
      userName: ''
    }
    return user;
  }

  constructor() { }

}
