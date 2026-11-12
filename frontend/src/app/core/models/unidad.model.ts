export type CategoriaUnidad = 'Moto' | 'Atv' | 'Ssv' | 'Utv';
export type NumeroServicio = 'Primero' | 'Segundo' | 'Tercero';

export interface UnidadDto {
  id: string;
  clienteId: string;
  numeroSerie: string;
  categoria: CategoriaUnidad;
  modelo: string;
  fechaCompra: string;
  servicioActual: NumeroServicio;
  fechaUltimoServicio: string | null;
  kilometrajeUltimoServicio: number | null;
  imagenUrl: string | null;
}

export interface RegistrarUnidadRequest {
  clienteId: string;
  numeroSerie: string;
  categoria: CategoriaUnidad;
  modelo: string;
  fechaCompra: string;
  fechaUltimoServicio?: string | null;
  kilometrajeUltimoServicio?: number | null;
  imagenUrl?: string | null;
}
