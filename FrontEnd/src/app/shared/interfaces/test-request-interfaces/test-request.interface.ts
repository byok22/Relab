import { ChangeStatusTestRequest } from "../../../test-catalog/interface/change-status-test-request";
import { TestRequestEnum } from "../../../test-requests/Enums/test-request.enum";
import { User } from "../UsersInterfaces/user.interface";
import { GenericUpdate } from "../generic-update.interface";
import { Test } from "../test-interfaces/test.interface";

// test-request.interface.ts
export interface TestRequest {
    id: number;
    status: TestRequestEnum;
    description: string;
    start: string;   
    end: string;
    tests: Test[];
    testsCount: number;
    active: boolean;    
    createdBy: User;
    createdAt: string;       
    updates: GenericUpdate[];
    changes: ChangeStatusTestRequest[];
  }
  