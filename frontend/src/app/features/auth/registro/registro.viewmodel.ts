import { Injectable, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { AuthApiService, RegistroRequest } from '../../../core/services/auth-api.service';

@Injectable()
export class RegistroViewModel {
  private readonly authApi = inject(AuthApiService);
  private readonly router = inject(Router);

  readonly cargando = signal(false);
  readonly mensajeError = signal<string | null>(null);

  registrar(datos: RegistroRequest): void {
    this.mensajeError.set(null);
    this.cargando.set(true);

    this.authApi.registrar(datos).subscribe({
      next: () => {
        this.cargando.set(false);
        this.router.navigate(['/login']);
      },
      error: (error) => {
        this.cargando.set(false);
        this.mensajeError.set(error?.error?.mensaje ?? 'No se pudo completar el registro.');
      }
    });
  }
}
