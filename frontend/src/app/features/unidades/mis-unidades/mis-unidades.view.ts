import { Component, OnInit, inject } from '@angular/core';
import { MisUnidadesViewModel } from './mis-unidades.viewmodel';

@Component({
  selector: 'fm-mis-unidades',
  standalone: true,
  providers: [MisUnidadesViewModel],
  template: `
    <section class="contenedor">
      <h1>Mis unidades</h1>

      @if (viewModel.cargando()) {
        <p>Cargando...</p>
      } @else if (viewModel.mensajeError()) {
        <p class="error">{{ viewModel.mensajeError() }}</p>
      } @else {
        <div class="tarjetas">
          @for (unidad of viewModel.unidades(); track unidad.id) {
            <article class="tarjeta">
              <h2>{{ unidad.modelo }}</h2>
              <p>VIN: {{ unidad.numeroSerie }}</p>
              <p>Categoria: {{ unidad.categoria }}</p>
              <p>Servicio actual: {{ unidad.servicioActual }}</p>
            </article>
          } @empty {
            <p>Todavia no tienes unidades registradas.</p>
          }
        </div>
      }
    </section>
  `,
  styleUrl: './mis-unidades.view.css'
})
export class MisUnidadesView implements OnInit {
  protected readonly viewModel = inject(MisUnidadesViewModel);

  ngOnInit(): void {
    this.viewModel.cargar();
  }
}
