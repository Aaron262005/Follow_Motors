import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { RegistroViewModel } from './registro.viewmodel';

@Component({
  selector: 'fm-registro',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  providers: [RegistroViewModel],
  template: `
    <section class="contenedor-auth">
      <h1>Crear cuenta</h1>
      <form [formGroup]="formulario" (ngSubmit)="enviar()">
        <label>Nombre <input formControlName="nombre" /></label>
        <label>Apellidos <input formControlName="apellidos" /></label>
        <label>Correo electronico <input type="email" formControlName="correo" /></label>
        <label>Contrasena <input type="password" formControlName="password" /></label>
        <label>Confirmar contrasena <input type="password" formControlName="confirmarPassword" /></label>

        @if (viewModel.mensajeError()) {
          <p class="error">{{ viewModel.mensajeError() }}</p>
        }

        <button type="submit" [disabled]="formulario.invalid || viewModel.cargando()">
          {{ viewModel.cargando() ? 'Creando cuenta...' : 'Crear cuenta' }}
        </button>
      </form>
      <p>¿Ya tienes cuenta? <a routerLink="/login">Inicia sesion</a></p>
    </section>
  `,
  styleUrl: '../login/login.view.css'
})
export class RegistroView {
  protected readonly viewModel = inject(RegistroViewModel);
  private readonly formBuilder = inject(FormBuilder);

  protected readonly formulario = this.formBuilder.nonNullable.group({
    nombre: ['', Validators.required],
    apellidos: ['', Validators.required],
    correo: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    confirmarPassword: ['', Validators.required]
  });

  protected enviar(): void {
    if (this.formulario.invalid) return;
    this.viewModel.registrar(this.formulario.getRawValue());
  }
}
