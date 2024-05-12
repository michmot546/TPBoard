import { Project } from "./project.model";
import { TableElement } from "./tableelement.model";

export interface Table {
  id: number;
  name: string;
  projectId?: number;
  project?: Project;
  elements?: TableElement[];
}
