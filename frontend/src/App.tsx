import React, { useEffect, useState, useRef } from 'react';
import {
  Container,
  Box,
  Typography,
  CircularProgress,
  Alert,
  AppBar,
  Toolbar,
  IconButton,
  Menu,
  MenuItem,
  Avatar,
} from '@mui/material';
import { CleaningServices } from '@mui/icons-material';
import ScheduleCard from './components/ScheduleCard';
import { getSchedules } from './services/schedules';
import { useAuth } from './contexts/AuthContext';
import type { Schedule } from './types/schedule';

function App() {
  const [schedules, setSchedules] = useState<Schedule[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const containerRef = useRef<HTMLDivElement>(null);
  const currentWeekRef = useRef<HTMLDivElement>(null);
  
  const { user, signOut } = useAuth();

  const isCurrentWeek = (beginDate: string, endDate: string) => {
    const today = new Date();
    const start = new Date(beginDate);
    const end = new Date(endDate);
    return today >= start && today <= end;
  };

  useEffect(() => {
    const loadSchedules = async () => {
      try {
        setLoading(true);
        const data = await getSchedules();
        setSchedules(data);
      } catch (err) {
        setError('Erro ao carregar as escalas');
      } finally {
        setLoading(false);
      }
    };

    loadSchedules();
  }, []);

  useEffect(() => {
    if (!loading && currentWeekRef.current && containerRef.current) {
      currentWeekRef.current.scrollIntoView({ behavior: 'smooth', block: 'center' });
    }
  }, [loading]);

  const handleMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  if (loading) {
    return (
      <Box display="flex" justifyContent="center" alignItems="center" minHeight="100vh">
        <CircularProgress />
      </Box>
    );
  }

  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar position="sticky">
        <Toolbar>
          <CleaningServices sx={{ mr: 2 }} />
          <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
            Escalas de Limpeza
          </Typography>
          
          {user && (
            <div>
              <IconButton onClick={handleMenu} color="inherit">
                <Avatar>{user.name.charAt(0).toUpperCase()}</Avatar>
              </IconButton>
              <Menu anchorEl={anchorEl} open={Boolean(anchorEl)} onClose={handleClose}>
                <MenuItem disabled>{user.name}</MenuItem>
                <MenuItem onClick={signOut}>Sair</MenuItem>
              </Menu>
            </div>
          )}
        </Toolbar>
      </AppBar>

      <Container 
        maxWidth="md" 
        sx={{ 
          mt: 2, 
          mb: 2, 
          maxHeight: 'calc(100vh - 64px)',
          overflow: 'auto',
        }}
        ref={containerRef}
      >
        {error && (
          <Alert severity="error" sx={{ mb: 2 }}>
            {error}
          </Alert>
        )}

        {schedules.map((schedule) => {
          const currentWeek = isCurrentWeek(schedule.beginDate, schedule.endDate);
          return (
            <div
              key={schedule.beginDate}
              ref={currentWeek ? currentWeekRef : null}
            >
              <ScheduleCard 
                schedule={schedule} 
                isCurrentWeek={currentWeek}
              />
            </div>
          );
        })}
      </Container>
    </Box>
  );
}

export default App;