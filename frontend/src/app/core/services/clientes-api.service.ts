import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ClienteDto } from '../models/usuario.model';

export interface RegistrarClienteRequest {
  nombre: string; apellidos: string; correo: string;
  telefono: string; password?: string | null;
}

@Injectable({ providedIn: 'root' })
export class ClientesApiService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiUrl}/clientes`;

  obtenerTodos(busqueda?: string): Observable<ClienteDto[]> {
    let params = new HttpParams();
    if (busqueda) params = params.set('busqueda', busqueda);
    return this.http.get<ClienteDto[]>(this.baseUrl, { params });
  }

  obtenerPorId(id: string): Observable<ClienteDto> {
    return this.http.get<ClienteDto>(`${this.baseUrl}/${id}`);
  }

  registrar(datos: RegistrarClienteRequest): Observable<ClienteDto> {
    return this.http.post<ClienteDto>(this.baseUrl, datos);
  }
}
