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
import { Toaster } from 'react-hot-toast';
import ScheduleCard from './components/ScheduleCard';
import { getSchedules } from './services/schedules';
import { useAuth } from './contexts/AuthContext';
import { isDateInCurrentWeek } from './utils/dateUtils';
import type { Schedule } from './types/schedule';
import InstallButton from './components/InstallButton';

function App() {
  const [schedules, setSchedules] = useState<Schedule[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const containerRef = useRef<HTMLDivElement>(null);
  const currentWeekRef = useRef<HTMLDivElement>(null);

  const { user, signOut } = useAuth();

  useEffect(() => {
    const loadSchedules = async () => {
      try {
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
      currentWeekRef.current.scrollIntoView({
        behavior: 'smooth',
        block: 'center',
      });
    }
  }, [loading]);

  const handleMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  return (
    <>
      {/* 🔥 BOTÃO SEMPRE EXISTE */}
      <InstallButton />

      {loading ? (
        <Box display="flex" justifyContent="center" alignItems="center" minHeight="100vh">
          <CircularProgress />
        </Box>
      ) : (
        <Box sx={{ flexGrow: 1 }}>
          <Toaster position="bottom-center" />

          <AppBar position="sticky">
            <Toolbar>
              <CleaningServices sx={{ mr: 2 }} />
              <Typography sx={{ flexGrow: 1 }}>
                Escalas de Limpeza
              </Typography>

              {user && (
                <>
                  <IconButton onClick={handleMenu} color="inherit">
                    <Avatar>{user.name.charAt(0).toUpperCase()}</Avatar>
                  </IconButton>
                  <Menu anchorEl={anchorEl} open={Boolean(anchorEl)} onClose={handleClose}>
                    <MenuItem disabled>{user.name}</MenuItem>
                    <MenuItem onClick={signOut}>Sair</MenuItem>
                  </Menu>
                </>
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
            {error && <Alert severity="error">{error}</Alert>}

            {schedules.map((schedule) => {
              const currentWeek = isDateInCurrentWeek(
                schedule.beginDate,
                schedule.endDate
              );

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
      )}
    </>
  );
}

export default App;