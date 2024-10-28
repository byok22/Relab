import { Attachment } from "../../shared/interfaces/test-interfaces/attachment.interface";
import { TestStatusEnum } from "./test-status.enum";

export interface ChangeStatus {
    status: TestStatusEnum;
    message: string;
    attachment?: Attachment;
    idUser?: number;
    idTest?: number;
    
 }
