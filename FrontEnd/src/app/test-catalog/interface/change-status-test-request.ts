import { Attachment } from "../../shared/interfaces/test-interfaces/attachment.interface";
import { TestRequestEnum } from "../../test-requests/Enums/test-request.enum";

export interface ChangeStatusTestRequest {
    status: TestRequestEnum;
    message: string;
    attachment?: Attachment;
    idUser?: number;
    
 }
