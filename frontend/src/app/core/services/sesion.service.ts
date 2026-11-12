import { Injectable, computed, signal } from '@angular/core';
import { SesionUsuario } from '../models/usuario.model';

const CLAVE_SESION = 'fm_sesion';

/**
 * Estado global de sesion, expuesto como signals. Cualquier ViewModel puede
 * leer sesion() / rol() reactivamente sin volver a pedirlo al backend.
 */
@Injectable({ providedIn: 'root' })
export class SesionService {
  private readonly _sesion = signal<SesionUsuario | null>(this.leerSesionGuardada());

  readonly sesion = this._sesion.asReadonly();
  readonly estaAutenticado = computed(() => this._sesion() !== null);
  readonly rol = computed(() => this._sesion()?.rol ?? null);

  iniciarSesion(sesion: SesionUsuario): void {
    this._sesion.set(sesion);
    localStorage.setItem(CLAVE_SESION, JSON.stringify(sesion));
  }

  cerrarSesion(): void {
    this._sesion.set(null);
    localStorage.removeItem(CLAVE_SESION);
  }

  obtenerToken(): string | null {
    return this._sesion()?.token ?? null;
  }

  private leerSesionGuardada(): SesionUsuario | null {
    try {
      const crudo = localStorage.getItem(CLAVE_SESION);
      return crudo ? (JSON.parse(crudo) as SesionUsuario) : null;
    } catch {
      return null;
    }
  }
}
