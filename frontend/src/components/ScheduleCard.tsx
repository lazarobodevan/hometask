import React, { useState } from 'react';
import {
  Card,
  CardContent,
  Typography,
  Box,
  Chip,
  List,
  Paper,
} from '@mui/material';
import {
  CalendarToday,
} from '@mui/icons-material';
import { formatDateRange, formatDateWithWeekday } from '../utils/dateUtils';
import TaskItem from './TaskItem';
import type { Schedule } from '../types/schedule';

interface ScheduleCardProps {
  schedule: Schedule;
  isCurrentWeek: boolean;
}

const ScheduleCard: React.FC<ScheduleCardProps> = ({ schedule, isCurrentWeek }) => {
  const [tasks, setTasks] = useState(schedule.items);

  // Agrupar tarefas por data
  const tasksByDate = tasks.reduce((acc, task) => {
    if (!acc[task.date]) {
      acc[task.date] = [];
    }
    acc[task.date].push(task);
    return acc;
  }, {} as Record<string, typeof tasks>);

  const handleTaskUpdate = (taskId: string, completed: boolean) => {
    setTasks(prevTasks =>
      prevTasks.map(task =>
        task.taskId === taskId ? { ...task, completed } : task
      )
    );
  };

  // Calcular progresso da semana
  const totalTasks = tasks.length;
  const completedTasks = tasks.filter(t => t.completed).length;
  const progress = totalTasks > 0 ? Math.round((completedTasks / totalTasks) * 100) : 0;

  return (
    <Card 
      sx={{ 
        mb: 2,
        border: isCurrentWeek ? 2 : 0,
        borderColor: 'primary.main',
      }}
    >
      <CardContent>
        <Box display="flex" justifyContent="space-between" alignItems="center" mb={2}>
          <Box display="flex" alignItems="center" gap={1}>
            <CalendarToday color="primary" />
            <Typography variant="h6">
              {formatDateRange(schedule.beginDate, schedule.endDate)}
            </Typography>
          </Box>
          
          <Box display="flex" alignItems="center" gap={1}>
            {isCurrentWeek && (
              <Chip 
                label="Semana Atual" 
                color="primary" 
                size="small"
                icon={<CalendarToday />}
              />
            )}
            <Chip 
              label={`${completedTasks}/${totalTasks}`}
              color={progress === 100 ? "success" : "default"}
              size="small"
              variant="outlined"
            />
          </Box>
        </Box>

        {/* Barra de progresso visual */}
        {totalTasks > 0 && (
          <Box 
            sx={{ 
              width: '100%', 
              height: 4, 
              bgcolor: 'background.paper',
              borderRadius: 2,
              mb: 2,
              overflow: 'hidden'
            }}
          >
            <Box 
              sx={{ 
                width: `${progress}%`, 
                height: '100%', 
                bgcolor: progress === 100 ? 'success.main' : 'primary.main',
                transition: 'width 0.3s ease',
              }} 
            />
          </Box>
        )}

        {Object.entries(tasksByDate).map(([date, dateTasks]) => (
          <Box key={date} sx={{ mt: 2 }}>
            <Typography 
              variant="subtitle2" 
              color="primary" 
              gutterBottom
              sx={{ textTransform: 'capitalize' }}
            >
              {formatDateWithWeekday(date)}
            </Typography>
            <Paper variant="outlined" sx={{ bgcolor: 'background.paper' }}>
              <List dense disablePadding>
                {dateTasks.map((task) => (
                  <TaskItem
                    key={task.taskId}
                    task={task}
                    weekStart={schedule.beginDate}
                    onTaskUpdate={handleTaskUpdate}
                  />
                ))}
              </List>
            </Paper>
          </Box>
        ))}
      </CardContent>
    </Card>
  );
};

export default ScheduleCard;