import React from 'react';
import {
  Card,
  CardContent,
  Typography,
  Box,
  Chip,
  List,
  ListItem,
  ListItemText,
  ListItemIcon,
  Checkbox,
  Paper,
} from '@mui/material';
import {
  CalendarToday,
  Person,
  Room,
} from '@mui/icons-material';
import type { Schedule } from '../types/schedule';

interface ScheduleCardProps {
  schedule: Schedule;
  isCurrentWeek: boolean;
}

const ScheduleCard: React.FC<ScheduleCardProps> = ({ schedule, isCurrentWeek }) => {
  const formatDate = (dateString: string) => {
    const date = new Date(dateString);
    return date.toLocaleDateString('pt-BR', { 
      day: '2-digit', 
      month: '2-digit' 
    });
  };

  // Agrupar tarefas por data
  const tasksByDate = schedule.items.reduce((acc, task) => {
    if (!acc[task.date]) {
      acc[task.date] = [];
    }
    acc[task.date].push(task);
    return acc;
  }, {} as Record<string, typeof schedule.items>);

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
              {formatDate(schedule.beginDate)} - {formatDate(schedule.endDate)}
            </Typography>
          </Box>
          {isCurrentWeek && (
            <Chip 
              label="Semana Atual" 
              color="primary" 
              size="small"
              icon={<CalendarToday />}
            />
          )}
        </Box>

        {Object.entries(tasksByDate).map(([date, tasks]) => (
          <Box key={date} sx={{ mt: 2 }}>
            <Typography 
              variant="subtitle2" 
              color="primary" 
              gutterBottom
              sx={{ textTransform: 'capitalize' }}
            >
              {new Date(date).toLocaleDateString('pt-BR', { 
                weekday: 'long',
                day: '2-digit',
                month: '2-digit'
              })}
            </Typography>
            <Paper variant="outlined" sx={{ bgcolor: 'background.paper' }}>
              <List dense disablePadding>
                {tasks.map((task) => (
                  <ListItem 
                    key={task.taskId}
                    divider
                    sx={{ 
                      py: 1.5,
                      '&:last-child': { 
                        borderBottom: 'none' 
                      } 
                    }}
                  >
                    <ListItemIcon sx={{ minWidth: 40 }}>
                      <Checkbox 
                        edge="start" 
                        checked={task.completed}
                        disabled
                        size="small"
                      />
                    </ListItemIcon>
                    <ListItemText
                      primary={
                        <Typography variant="body2" fontWeight="medium">
                          {task.taskName}
                        </Typography>
                      }
                      secondary={
                        <Box 
                          component="span" 
                          sx={{ 
                            display: 'flex', 
                            alignItems: 'center', 
                            gap: 2,
                            mt: 0.5,
                            flexWrap: 'wrap'
                          }}
                        >
                          <Box component="span" sx={{ display: 'flex', alignItems: 'center', gap: 0.5 }}>
                            <Person sx={{ fontSize: 16, color: 'text.secondary' }} />
                            <Typography variant="caption" color="text.secondary" component="span">
                              {task.responsibleName}
                            </Typography>
                          </Box>
                          <Box component="span" sx={{ display: 'flex', alignItems: 'center', gap: 0.5 }}>
                            <Room sx={{ fontSize: 16, color: 'text.secondary' }} />
                            <Typography variant="caption" color="text.secondary" component="span">
                              {task.areaName}
                            </Typography>
                          </Box>
                        </Box>
                      }
                    />
                  </ListItem>
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