import { ChangeStatusTestRequest } from "../../../test-catalog/interface/change-status-test-request";
import { TestRequestEnum } from "../../../test-requests/Enums/test-request.enum";
import { GenericUpdate } from "../generic-update.interface";
import { Test } from "../test-interfaces/test.interface";

// test-request.interface.ts
export interface TestRequestDto {
    id: number;
    status: TestRequestEnum;
    createdBy?: string;
    user:string;
    description:string;
    active: boolean;   
    start: string;
    tentativeEnd?: string;
    end?: string;
    testsCount?: number;  
    tests?: Test[];
    lastupdated?: string;
    lastUpdatedBy?: string;
    lastUpdatedMessage?: string;
    createdAt: string;
    updates?: GenericUpdate[];
    changes?: ChangeStatusTestRequest[];
   
  }
  