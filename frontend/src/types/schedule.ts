export interface Task {
  date: string;
  taskId: string;
  taskName: string;
  areaId: string;
  areaName: string;
  responsibleId: string;
  responsibleName: string;
  completed: boolean;
  completedBy: string | null;
  completedAt: string | null;
}

export interface Schedule {
  beginDate: string;
  endDate: string;
  items: Task[];
}

export interface UpdateTaskDoneDto {
  taskId: string;
  weekStart: string;
}