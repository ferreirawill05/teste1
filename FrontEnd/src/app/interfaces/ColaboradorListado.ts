export interface numero{
  numero: number
}

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
  numero: any;
  colaboradores : Colaboradores[];
  quantidade : number
}
