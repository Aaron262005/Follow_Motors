import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { SesionService } from '../services/sesion.service';

/** Uso: { path: 'gerente', canActivate: [rolGuard('Gerente')], ... } */
export function rolGuard(rolRequerido: 'Cliente' | 'Gerente'): CanActivateFn {
  return () => {
    const sesionService = inject(SesionService);
    const router = inject(Router);

    if (sesionService.rol() === rolRequerido) return true;

    router.navigate(['/login']);
    return false;
  };
}
