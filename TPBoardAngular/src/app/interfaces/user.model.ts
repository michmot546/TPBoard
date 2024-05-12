import { ProjectUser } from "./projectuser.model";

export interface User {
  id: number;
  login: string;
  password: string;
  name: string;
  email: string;
  projects?: ProjectUser[];
}
