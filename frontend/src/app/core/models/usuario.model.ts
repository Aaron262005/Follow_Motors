export type RolUsuario = 'Cliente' | 'Gerente';

/** Espejo de FollowMotors.Application.Auth LoginResult (backend). */
export interface SesionUsuario {
  token: string;
  usuarioId: string;
  nombreCompleto: string;
  rol: RolUsuario;
  numeroCliente: string | null;
}

export interface ClienteDto {
  id: string;
  nombre: string;
  apellidos: string;
  correo: string;
  telefono: string;
  numeroCliente: string | null;
  activo: boolean;
  cantidadUnidades: number;
}
