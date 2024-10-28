import { Injectable } from '@angular/core';
import { HttpService } from '../../shared/interfaces/http-service';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { DropDown } from '../interfaces/drop-down';
import { SelectOption } from '../../shared/interfaces/select-option.interface';
import { TestRequestEnum } from '../../test-requests/Enums/test-request.enum';
import { EmployeeType } from '../../shared/enums/employee-type.enum';

@Injectable({
  providedIn: 'root'
})
export class DropdownsService   {

  constructor(private http: HttpClient) {
    
  }

  getEmployeeTypes(): Observable<SelectOption[]> {
    const selecs: SelectOption[] =
      [
        {
          id: EmployeeType.Engineer,
          text: 'Engineer'
        },
        {
          id: EmployeeType.Technician,
          text: 'Technician'
        },
        {
          id: EmployeeType.All,
          text: 'All'
        }
      ];
    return of(selecs);
  }

  getStatus(): Observable<SelectOption[]> {
    const selecs: SelectOption[] =
      [
        {
          id: TestRequestEnum.All,
          text: 'All'
        },
        {
          id: TestRequestEnum.New,
          text: 'New'
        },
        {
          id: TestRequestEnum.Pending,
          text: 'Pending'
        },
        {
          id: TestRequestEnum.Approved,
          text: 'Approved'
        },
        {
          id: TestRequestEnum.Rejected,
          text: 'Rejected'
        },
        {
          id: TestRequestEnum.Cancelled,
          text: 'Cancelled'
        },
        {
          id: TestRequestEnum.Completed,
          text: 'Completed'
        },
        {
          id: TestRequestEnum.Ended,
          text: 'Ended'
        },
        {
          id: TestRequestEnum.InProgress,
          text: 'In Progress'
        }
      ]; 
      return of(selecs);
  }

}
