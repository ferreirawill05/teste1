import { Email, Telefone } from "./ColaboradorCadastro";

export interface ColaboradorEditar{
    nome: string,
    idTipoUsuario: number,
    emails: Email[],
    telefones: Telefone[],
}