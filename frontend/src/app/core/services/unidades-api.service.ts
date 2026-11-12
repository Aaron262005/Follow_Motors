import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { RegistrarUnidadRequest, UnidadDto } from '../models/unidad.model';

@Injectable({ providedIn: 'root' })
export class UnidadesApiService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiUrl}/unidades`;

  obtenerPorCliente(clienteId: string): Observable<UnidadDto[]> {
    return this.http.get<UnidadDto[]>(`${this.baseUrl}/cliente/${clienteId}`);
  }

  registrar(datos: RegistrarUnidadRequest): Observable<UnidadDto> {
    return this.http.post<UnidadDto>(this.baseUrl, datos);
  }
}
