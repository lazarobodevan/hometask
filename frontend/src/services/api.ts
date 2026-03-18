import axios from 'axios';
import type { User } from '../types/user';

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

api.interceptors.request.use(
  (config) => {
    // Pega o usuário do localStorage
    const userStr = localStorage.getItem('@tasks:user');
    
    if (userStr) {
      try {
        const user: User = JSON.parse(userStr);
        config.headers['X-User-Id'] = user.id;
      
      } catch (error) {
        console.error('Erro ao parsear usuário do localStorage:', error);
      }
    } else {
      if (import.meta.env.DEV) {
        console.log('⚠️ Requisição sem usuário autenticado');
      }
    }
    
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export default api;