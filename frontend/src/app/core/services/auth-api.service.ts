import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { SesionUsuario } from '../models/usuario.model';

export interface LoginRequest { correo: string; password: string; }
export interface RegistroRequest {
  nombre: string; apellidos: string; correo: string;
  password: string; confirmarPassword: string;
}

/**
 * Capa de acceso a datos (el "Model" de MVVM): solo hace llamadas HTTP y
 * devuelve datos tipados. No guarda estado ni decide nada de UI; eso vive
 * en el ViewModel (login.viewmodel.ts).
 */
@Injectable({ providedIn: 'root' })
export class AuthApiService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiUrl}/auth`;

  login(datos: LoginRequest): Observable<SesionUsuario> {
    return this.http.post<SesionUsuario>(`${this.baseUrl}/login`, datos);
  }

  registrar(datos: RegistroRequest): Observable<string> {
    return this.http.post<string>(`${this.baseUrl}/registro`, datos);
  }
}
