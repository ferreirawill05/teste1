// Generated by https://quicktype.io

export interface ColaboradorDetalhes {
  nome:          string;
  cpf:           string;
  usuario:       string;
  senha:         null;
  idTipoUsuario: number;
  emails:        Email[];
  telefone:      Telefone[];
  permissoes:    Permissoes[];
}

export interface Email {
  idEmail:     number;
  email:       string;
  flPrincipal: boolean;
}

export interface Permissoes {
  idColaboradorPermissao: number;
  idColaborador:          number;
  idPermissao:            number;
}

export interface Telefone {
  idTelefone:  number;
  apelido:     string;
  numero:      string;
  flPrincipal: boolean;
}
