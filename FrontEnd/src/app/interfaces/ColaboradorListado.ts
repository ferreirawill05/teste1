
export interface Colaboradores {
idColaborador:     number;
nome:              string;
cpf:               string;
telefonePrincipal: string;
emailPrincipal:    string;
dtCriacao:         string;
nsCriacao:         string | null;
colaboradores : Colaboradores[];
}

export interface ColaboradorListado {
  colaboradores : Colaboradores[];
  quantidade : number
}
