import { Vector3 } from "three";
import { Task } from "./Task";


export interface Project {
    id: string;
    name: string;
    description?: string | null;
    children: Project[];
    tasks: Task[];
    position?: Vector3;
    createdAt: Date;
}
