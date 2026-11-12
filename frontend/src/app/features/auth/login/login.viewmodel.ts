import { Injectable, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { AuthApiService } from '../../../core/services/auth-api.service';
import { SesionService } from '../../../core/services/sesion.service';

/**
 * ViewModel de MVVM: contiene TODO el estado y la logica de presentacion de
 * la pantalla de login (loading, error, que pasa al enviar el formulario).
 * La View (login.view.ts) solo lee estos signals y llama a estos metodos;
 * no tiene logica propia, igual que un Controller del backend.
 */
@Injectable()
export class LoginViewModel {
  private readonly authApi = inject(AuthApiService);
  private readonly sesionService = inject(SesionService);
  private readonly router = inject(Router);

  readonly cargando = signal(false);
  readonly mensajeError = signal<string | null>(null);

  iniciarSesion(correo: string, password: string): void {
    this.mensajeError.set(null);
    this.cargando.set(true);

    this.authApi.login({ correo, password }).subscribe({
      next: (sesion) => {
        this.cargando.set(false);
        this.sesionService.iniciarSesion(sesion);
        this.router.navigate([sesion.rol === 'Gerente' ? '/clientes' : '/mis-unidades']);
      },
      error: (error) => {
        this.cargando.set(false);
        this.mensajeError.set(error?.error?.mensaje ?? 'Correo o contrasena incorrectos.');
      }
    });
  }
}
