import { ProjectUser } from "./projectuser.model";
import { UserRole } from "./UserRole";

export interface User {
  id: number;
  login: string;
  password: string;
  name: string;
  email: string;
  projects?: ProjectUser[];
  roles: UserRole[]
}
