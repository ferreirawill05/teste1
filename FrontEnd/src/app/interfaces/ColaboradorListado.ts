
export interface Colaboradores {
idColaborador:     number;
nome:              string;
cpf:               string;
telefonePrincipal: string;
emailPrincipal:    string;
dtCriacao:         string;
nsCriacao:         string | null;
}

export interface ColaboradorListado {
colaboradores : Colaboradores[];
numero : number
}
  