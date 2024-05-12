import { Project } from "./project.model";
import { User } from "./user.model";

export interface ProjectUser {
  usertId: number;
  user: User;
  projectId: number;
  project: Project;
}
