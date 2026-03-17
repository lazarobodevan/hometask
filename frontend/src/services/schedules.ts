import type { Schedule } from '../types/schedule';
import api from './api';


export const getSchedules = async (): Promise<Schedule[]> => {
  const response = await api.get('/houseTasks/schedules');
  return response.data;
};