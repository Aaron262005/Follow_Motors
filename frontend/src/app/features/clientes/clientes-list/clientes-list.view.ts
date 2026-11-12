import { Component, OnInit, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ClientesListViewModel } from './clientes-list.viewmodel';

@Component({
  selector: 'fm-clientes-list',
  standalone: true,
  imports: [FormsModule],
  providers: [ClientesListViewModel],
  template: `
    <section class="contenedor">
      <h1>Clientes</h1>

      <input
        type="search"
        placeholder="Buscar nombre, correo o numero de cliente..."
        [ngModel]="viewModel.textoBusqueda()"
        (ngModelChange)="viewModel.buscar($event)"
      />

      @if (viewModel.cargando()) {
        <p>Cargando...</p>
      } @else if (viewModel.mensajeError()) {
        <p class="error">{{ viewModel.mensajeError() }}</p>
      } @else {
        <table>
          <thead>
            <tr>
              <th>No. cliente</th><th>Nombre</th><th>Correo</th><th>Telefono</th><th>Unidades</th>
            </tr>
          </thead>
          <tbody>
            @for (cliente of viewModel.clientes(); track cliente.id) {
              <tr>
                <td>{{ cliente.numeroCliente }}</td>
                <td>{{ cliente.nombre }} {{ cliente.apellidos }}</td>
                <td>{{ cliente.correo }}</td>
                <td>{{ cliente.telefono }}</td>
                <td>{{ cliente.cantidadUnidades }}</td>
              </tr>
            } @empty {
              <tr><td colspan="5">Sin clientes registrados todavia.</td></tr>
            }
          </tbody>
        </table>
      }
    </section>
  `,
  styleUrl: './clientes-list.view.css'
})
export class ClientesListView implements OnInit {
  protected readonly viewModel = inject(ClientesListViewModel);

  ngOnInit(): void {
    this.viewModel.cargar();
  }
}
