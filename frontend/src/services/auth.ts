import type { User } from '../types/user';
import api from './api';


export const login = async (username: string): Promise<User> => {
  try {
    const response = await api.post('/auth/login', { username });
    return response.data;
  } catch (error: any) {
    // Se o backend retornar uma resposta com erro
    if (error.response) {
      // O servidor respondeu com um status de erro
      console.log('Status do erro:', error.response.status);
      console.log('Dados do erro:', error.response.data);
      
      // Se a resposta for uma string (como "Usuário não cadastrado")
      if (typeof error.response.data === 'string') {
        throw new Error(error.response.data);
      }
      // Se a resposta for um objeto com mensagem
      if (error.response.data?.message) {
        throw new Error(error.response.data.message);
      }
      // Se for 401 sem mensagem específica
      if (error.response.status === 401) {
        throw new Error('Usuário não autorizado');
      }
    }
    
    // Erro de rede (não conseguiu conectar ao servidor)
    if (error.request) {
      throw new Error('Não foi possível conectar ao servidor');
    }
    
    // Outro tipo de erro
    throw new Error('Erro ao fazer login');
  }
};