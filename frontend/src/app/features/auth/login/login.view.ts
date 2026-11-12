import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { LoginViewModel } from './login.viewmodel';

/**
 * View de MVVM: solo renderiza y traduce eventos del usuario a llamadas del
 * ViewModel. `providers: [LoginViewModel]` le da su propia instancia (no es
 * singleton global, igual que un Handler se crea por cada Command).
 */
@Component({
  selector: 'fm-login',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  providers: [LoginViewModel],
  template: `
    <section class="contenedor-auth">
      <h1>Follow Motors</h1>
      <form [formGroup]="formulario" (ngSubmit)="enviar()">
        <label>
          Correo
          <input type="email" formControlName="correo" placeholder="cliente@correo.com" />
        </label>

        <label>
          Contrasena
          <input type="password" formControlName="password" />
        </label>

        @if (viewModel.mensajeError()) {
          <p class="error">{{ viewModel.mensajeError() }}</p>
        }

        <button type="submit" [disabled]="formulario.invalid || viewModel.cargando()">
          {{ viewModel.cargando() ? 'Entrando...' : 'Entrar' }}
        </button>
      </form>
      <p>¿No tienes cuenta? <a routerLink="/registro">Registrate</a></p>
    </section>
  `,
  styleUrl: './login.view.css'
})
export class LoginView {
  protected readonly viewModel = inject(LoginViewModel);
  private readonly formBuilder = inject(FormBuilder);

  protected readonly formulario = this.formBuilder.nonNullable.group({
    correo: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required]
  });

  protected enviar(): void {
    if (this.formulario.invalid) return;
    const { correo, password } = this.formulario.getRawValue();
    this.viewModel.iniciarSesion(correo, password);
  }
}
