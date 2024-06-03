export interface idColaborador{
    idColaborador: number;
}

export interface ColaboradorCadastro{
    nome : string;
    cpf : string;
    usuario: string;
    senha: string;
    confirmarSenha: string;
    idTipoUsuario: number;
    emails: Email[];
    telefones: Telefone[];
}

export interface Email{
    email: string;
    flPrincial: boolean;
}

export interface Telefone{
    apelido: string;
    numero: string;
    flPricipal: boolean;
}