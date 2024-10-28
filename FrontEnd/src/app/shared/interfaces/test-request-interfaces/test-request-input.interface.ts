import { ChangeStatusTestRequest } from "../../../test-catalog/interface/change-status-test-request";
import { GenericUpdate } from "../generic-update.interface";
import { TestInputType } from "../test-interfaces/test-input-type";
import { TestDto } from "../test-interfaces/testdto.interface";

export interface TestRequestInput {

    id:number;
    status:string,
    description:string,
    start: string,
    end: string,
    tests?: TestInputType[],
    updates?: GenericUpdate[],
    changes?: ChangeStatusTestRequest[]
 }
