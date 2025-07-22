import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { ApiResponse  } from './users.model';

export interface User {
  id?: string;
  username?: string;
  password?: string;
  phone: string;
  email: string;
  status: number;
  role: number;
  name?: string;
}

const BASE_URL = 'https://localhost:7181/api/Users';

@Injectable({ providedIn: 'root' })
export class UsersService {
  private http = inject(HttpClient);

  // Cadastra um novo usuário
  createUser(user: User): Observable<any> {
    return this.http.post(`${BASE_URL}`, user);
  }

  // Cadastra usuários de teste
  createFakeUsers(): Observable<any> {
    return this.http.post(`${BASE_URL}/CreatetUserTest`, {});
  }

  // Lista todos os usuários
  getAllUsers(): Observable<User[]> {
    return this.http
      .get<ApiResponse<{ data: User[] }>>(`${BASE_URL}/ListAll`)
      .pipe(map(res => res.data.data));
  }    

  // Obtém um usuário pelo ID
  getUserById(id: string): Observable<User> {
    return this.http
      .get<{ success: boolean; data: User }>(`${BASE_URL}/${id}`)
      .pipe(map(res => res.data));
  }

  // Exclui um usuário pelo ID
  deleteUser(id: string): Observable<any> {
    return this.http.delete(`${BASE_URL}/${id}`);
  }
}
