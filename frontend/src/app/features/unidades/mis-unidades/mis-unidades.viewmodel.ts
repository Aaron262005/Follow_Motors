import { Injectable, inject, signal } from '@angular/core';
import { UnidadesApiService } from '../../../core/services/unidades-api.service';
import { UnidadDto } from '../../../core/models/unidad.model';
import { SesionService } from '../../../core/services/sesion.service';

/** Espejo de cliente/cliente.html: lista las unidades del cliente en sesion. */
@Injectable()
export class MisUnidadesViewModel {
  private readonly unidadesApi = inject(UnidadesApiService);
  private readonly sesionService = inject(SesionService);

  readonly unidades = signal<UnidadDto[]>([]);
  readonly cargando = signal(false);
  readonly mensajeError = signal<string | null>(null);

  cargar(): void {
    const sesion = this.sesionService.sesion();
    if (!sesion) return;

    this.cargando.set(true);
    this.unidadesApi.obtenerPorCliente(sesion.usuarioId).subscribe({
      next: (unidades) => {
        this.unidades.set(unidades);
        this.cargando.set(false);
      },
      error: () => {
        this.mensajeError.set('No se pudieron cargar tus unidades.');
        this.cargando.set(false);
      }
    });
  }
}
