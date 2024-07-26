import { ProjectUser } from "./projectuser.model";
import { Table } from "./table.model";

export interface Project {
  id: number;
  name: string;
  ownerId: number;
  users: ProjectUser[];
  tables?: Table[];
}
