import type { Schedule, UpdateTaskDoneDto } from '../types/schedule';
import api from './api';


export const getSchedules = async (): Promise<Schedule[]> => {
  const response = await api.get('/houseTasks/schedules');
  return response.data;
};

export const markAsDone = async (data: UpdateTaskDoneDto): Promise<void> => {
  try {
    const response = await api.patch('/houseTasks/done', data);
    return response.data;
  } catch (error: any) {
    handleApiError(error, "Erro ao marcar tarefa");
  }
};

export const markAsUndone = async (data: UpdateTaskDoneDto): Promise<void> => {

  try {
    const response = await api.patch('/houseTasks/undone', data);
    return response.data;
  } catch (error: any) {
    handleApiError(error, "Erro ao desmarcar tarefa");
  }
};

const handleApiError = (error: any, defaultMessage: string): never => {
  console.error('Erro na requisição:', {
    status: error.response?.status,
    data: error.response?.data,
    config: {
      url: error.config?.url,
      method: error.config?.method,
      data: error.config?.data
    }
  });

  // Verifica se tem resposta da API
  if (error.response?.data) {
    const responseData = error.response.data;

    // Caso 1: Objeto com propriedade 'error' (seu caso)
    if (responseData.error && typeof responseData.error === 'string') {
      throw new Error(responseData.error);
    }

    // Caso 2: Objeto com propriedade 'message'
    if (responseData.message && typeof responseData.message === 'string') {
      throw new Error(responseData.message);
    }

    // Caso 3: É uma string direta
    if (typeof responseData === 'string') {
      throw new Error(responseData);
    }

    // Caso 4: É um objeto mas não sabemos a estrutura
    try {
      throw new Error(JSON.stringify(responseData));
    } catch {
      throw new Error(defaultMessage);
    }
  }

  // Erro de rede ou outro tipo
  if (error.request) {
    throw new Error('Não foi possível conectar ao servidor');
  }

  throw new Error(defaultMessage);
};