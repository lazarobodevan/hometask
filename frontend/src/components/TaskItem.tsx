import React, { useState } from 'react';
import {
  ListItem,
  ListItemIcon,
  ListItemText,
  Checkbox,
  Box,
  Typography,
  CircularProgress,
  Tooltip,
  Fade,
} from '@mui/material';
import {
  Person,
  Room,
  CheckCircle,
  RadioButtonUnchecked,
} from '@mui/icons-material';
import type { Task, UpdateTaskDoneDto } from '../types/schedule';
import { markAsDone, markAsUndone } from '../services/schedules';
import toast from 'react-hot-toast';

interface TaskItemProps {
  task: Task;
  weekStart: string;
  onTaskUpdate: (taskId: string, completed: boolean) => void;
}

const TaskItem: React.FC<TaskItemProps> = ({ task, weekStart, onTaskUpdate }) => {
  const [loading, setLoading] = useState(false);
  const [completed, setCompleted] = useState(task.completed);
  const [showFeedback, setShowFeedback] = useState(false);

  const handleToggle = async () => {
    if (loading) return;

    const newCompletedState = !completed;
    const dto = {
      taskId: task.taskId,
      weekStart: weekStart,
    } as UpdateTaskDoneDto;

    setLoading(true);
    const loadingToast = toast.loading(newCompletedState ? 'Concluindo...' : 'Reabrindo...');
    try {
      if (newCompletedState) {
        await markAsDone(dto);
      } else {
        await markAsUndone(dto);
        task.completedBy = null;
        task.completedAt = null;
      }
      
      setCompleted(newCompletedState);
      onTaskUpdate(task.taskId, newCompletedState);
      
      // Mostrar feedback visual
      setShowFeedback(true);
      setTimeout(() => setShowFeedback(false), 1500);
      
    } catch (error:any) {
      console.log('Erro ao atualizar tarefa:', JSON.stringify(error));
      toast.error(error.message || 'Erro ao atualizar tarefa', {
        id: loadingToast,
        duration: 5000,
      });
    } finally {
      setLoading(false);
    }
  };

  return (
    <ListItem 
      divider
      sx={{ 
        py: 1.5,
        '&:last-child': { 
          borderBottom: 'none' 
        },
        opacity: completed ? 0.8 : 1,
        transition: 'opacity 0.2s',
      }}
      secondaryAction={
        showFeedback && (
          <Fade in={showFeedback} timeout={500}>
            <Typography variant="caption" color="success.main" sx={{ mr: 1 }}>
              {completed ? '✓ Concluída' : '↩ Reaberta'}
            </Typography>
          </Fade>
        )
      }
    >
      <ListItemIcon sx={{ minWidth: 40 }}>
        <Tooltip title={completed ? 'Marcar como não concluída' : 'Marcar como concluída'}>
          <Checkbox 
            edge="start" 
            checked={completed}
            onChange={handleToggle}
            disabled={loading}
            size="small"
            icon={<RadioButtonUnchecked fontSize="small" />}
            checkedIcon={<CheckCircle fontSize="small" color="success" />}
            sx={{
              color: completed ? 'success.main' : 'inherit',
            }}
          />
        </Tooltip>
      </ListItemIcon>
      
      <ListItemText
        primary={
          <Box display="flex" alignItems="center" gap={1}>
            <Typography 
              variant="body2" 
              fontWeight="medium"
              sx={{
                textDecoration: completed ? 'line-through' : 'none',
                color: completed ? 'text.secondary' : 'text.primary',
              }}
            >
              {task.taskName}
            </Typography>
            {loading && <CircularProgress size={16} sx={{ ml: 1 }} />}
          </Box>
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
            {task.completedBy && task.completedAt && (
              <Typography variant="caption" color="text.secondary" component="span">
                Concluído por {task.completedBy}
              </Typography>
            )}
          </Box>
        }
      />
    </ListItem>
  );
};

export default TaskItem;