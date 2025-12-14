export interface Task {
    id: string;
    title: string;
    description?: string | null;
    projectId: string,
    status: string,
    tasks: Task[];
    startDate: Date,
    endDate?: Date, 
    createdAt: Date;
}
