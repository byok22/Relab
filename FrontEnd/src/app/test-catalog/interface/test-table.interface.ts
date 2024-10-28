import { TestStatusEnum } from "./test-status.enum";

export interface TestTable { 

    id: number;
    name?: string;
    description?: string;
    start?: string;
    end?: string;
    specifications?: number;
    equipments?: number;
    samples?: number;
    specialInstructions?: string;
    enginner?: string;
    technicians?: string;   
    lastUpdatedBy?: string;
    lastUpdatedMessage?: string;
    status?: TestStatusEnum;
    attachments?: number;
    attachmentsCount?: number;
    techniciansCount?: number;
    specificationsCount?: number;
    equipmentsCount?: number;
   

}
