import { User } from "./user.model";

export interface TableElement {
    id: number;
    name: string;
    tableId: number;
    assignedUserId?: number | null;
    assignedUser?: User;
  }
  