import { Injectable, inject, signal } from '@angular/core';
import { ClientesApiService, RegistrarClienteRequest } from '../../../core/services/clientes-api.service';
import { ClienteDto } from '../../../core/models/usuario.model';

/**
 * ViewModel del modulo Clientes (espejo de gerente/clientes-cuentas.html).
 * Mantiene la lista, el texto de busqueda y el estado de carga como signals;
 * la View solo se suscribe a ellos.
 */
@Injectable()
export class ClientesListViewModel {
  private readonly clientesApi = inject(ClientesApiService);

  readonly clientes = signal<ClienteDto[]>([]);
  readonly cargando = signal(false);
  readonly mensajeError = signal<string | null>(null);
  readonly textoBusqueda = signal('');

  cargar(): void {
    this.cargando.set(true);
    this.mensajeError.set(null);

    this.clientesApi.obtenerTodos(this.textoBusqueda() || undefined).subscribe({
      next: (clientes) => {
        this.clientes.set(clientes);
        this.cargando.set(false);
      },
      error: () => {
        this.mensajeError.set('No se pudo cargar la lista de clientes.');
        this.cargando.set(false);
      }
    });
  }

  buscar(texto: string): void {
    this.textoBusqueda.set(texto);
    this.cargar();
  }

  registrarCliente(datos: RegistrarClienteRequest): void {
    this.clientesApi.registrar(datos).subscribe({
      next: () => this.cargar(),
      error: () => this.mensajeError.set('No se pudo registrar el cliente.')
    });
  }
}
