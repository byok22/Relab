import { User } from "../UsersInterfaces/user.interface";

// equipment.interface.ts
export interface Equipment {
    id: number;
    name: string;
    calibrationDate: string;
    description: string;
    user?: User
  }