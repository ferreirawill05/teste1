import { Email, Telefone } from "./ColaboradorCadastro";

export interface ColaboradorDetalhes{
    nome: string,
    cpf: string,
    usuario: string,
    senha: null,
    idTipoUsuario: number,
    emails: Email[],
    telefone: Telefone[],
    // permissoes: Permissoes[],
}