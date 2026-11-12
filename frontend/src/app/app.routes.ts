import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { rolGuard } from './core/guards/rol.guard';

/**
 * Lazy loading obligatorio: cada feature se carga solo cuando se visita
 * su ruta (loadComponent), nunca de entrada en el bundle principal.
 */
export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'login' },
  {
    path: 'login',
    loadComponent: () => import('./features/auth/login/login.view').then(m => m.LoginView)
  },
  {
    path: 'registro',
    loadComponent: () => import('./features/auth/registro/registro.view').then(m => m.RegistroView)
  },
  {
    path: 'clientes',
    canActivate: [authGuard, rolGuard('Gerente')],
    loadComponent: () =>
      import('./features/clientes/clientes-list/clientes-list.view').then(m => m.ClientesListView)
  },
  {
    path: 'mis-unidades',
    canActivate: [authGuard, rolGuard('Cliente')],
    loadComponent: () =>
      import('./features/unidades/mis-unidades/mis-unidades.view').then(m => m.MisUnidadesView)
  },
  { path: '**', redirectTo: 'login' }
];
