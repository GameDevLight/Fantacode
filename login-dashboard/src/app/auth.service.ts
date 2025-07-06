import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private apiUrl = 'http://localhost:5270/api/auth';

  constructor(private http: HttpClient) { }

  login(credentials: any) {
    return this.http.post<any>(`${this.apiUrl}/login`, credentials);
  }

  isAuthenticated() {
    return !!localStorage.getItem('token');
  }
}
