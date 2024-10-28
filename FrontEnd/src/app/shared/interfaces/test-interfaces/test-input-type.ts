import { ChangeStatus } from "../../../test-catalog/interface/change-status.";
import { TestStatusEnum } from "../../../test-catalog/interface/test-status.enum";
import { Employee } from "../employees/employee.interface";
import { Equipment } from "../equipments/equipment.interface";
import { GenericUpdate } from "../generic-update.interface";
import { User } from "../UsersInterfaces/user.interface";
import { Attachment } from "./attachment.interface";
import { Samples } from "./samples.interface";
import { Specification } from "./specification.interface";

export interface TestInputType{
    id?: number;
    name?: string;
    description?: string;
    start?: string;
    end?: string;
    specifications?: Specification[];
    equipments?: Equipment[];
    samples?: Samples;
    specialInstructions?: string;
    enginner?: {
        employeeNumber: string;
        employeeType: string;
        name: string;
        id: number;
    }
    technicians?: {
        employeeNumber: string;
        employeeType: string;
        name: string;
        id: number;
    }[];
    idRequest?: number;
    profile?: Attachment
    attachments?: Attachment[];
   // lastupdated?: string;
    //lastUpdatedBy?: number;
    lastUpdatedMessage?: string;
   // createdAt?: string;
    createdBy?: number;
    status?: string;
    changeStatusTest?: ChangeStatus[];
    updates?: GenericUpdate[];

}