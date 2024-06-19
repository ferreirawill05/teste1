export interface Permissoes {
  idColaboradorPermissao: number;
  idColaborador:          number;
  idPermissao:            number;
}

export interface GetMeColaborador {
  nome:       string;
  idTipoUsuario: number;
  permissoes: Permissoes[];
}
