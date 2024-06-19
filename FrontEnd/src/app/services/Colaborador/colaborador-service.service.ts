import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/app/environment/API';
import { ColaboradorListado } from 'src/app/interfaces/ColaboradorListado';
import { Observable, catchError, map, of } from 'rxjs';
import { ColaboradorCadastro, idColaborador } from 'src/app/interfaces/ColaboradorCadastro';
import { ColaboradorDetalhes } from 'src/app/interfaces/ColaboradorDetalhes';
import { ColaboradorEditar } from 'src/app/interfaces/ColaboradorEditar';
import { ListaFiltros } from 'src/app/interfaces/ListaFiltros';
import { AlterarSenha } from 'src/app/interfaces/AlterarSenha';


@Injectable({
  providedIn: 'root'
})
export class ColaboradorService {

  private readonly API = environment.apiUrl

  constructor(private http : HttpClient) { }

  //tipar interface
  ListarColaboradores(pagina : ListaFiltros) : Observable<ColaboradorListado>{

    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem("token")}`);
    return this.http.post<ColaboradorListado>(this.API + "filtroGeral", pagina, {headers})
  }

  AlterarSenha(senhas : AlterarSenha){
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem("token")}`);
    return this.http.patch(this.API + "colaboradores/alterarsenha", senhas, {headers})
  }

  AlterarSenhaOutroColaborador(senhas : AlterarSenha, idColaborador : number){
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem("token")}`);
    return this.http.patch(this.API + "colaboradores/alterarsenha/" + idColaborador, senhas, {headers})
  }

  CadastrarColaborador(colaborador : ColaboradorCadastro) : Observable<idColaborador>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem("token")}`);
    return this.http.post<idColaborador>(this.API + "colaboradores", colaborador, {headers})
  }

  GetMeColaborador() : Observable<any>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem("token")}`);
    return this.http.get<any>(this.API + "colaboradores/me", {headers})
  }

  ConsultaPorId(id : number) : Observable<ColaboradorDetalhes>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem("token")}`);
    return this.http.get<ColaboradorDetalhes>(this.API + "api/colaboradores/" + id, {headers})
  }

  EditarColaborador(colaborador : ColaboradorEditar, id : number){
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem("token")}`);
    return this.http.patch(this.API + "colaboradores/editar/" + id, colaborador, {headers})
  }

 DeletarColaborador(id : number) : Observable<ColaboradorDetalhes>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem("token")}`);
    return this.http.delete<ColaboradorDetalhes>(this.API + "colaboradores/" + id, {headers})
  }

  VerificaPermissao(idPermissao: number): Observable<boolean> {
    return this.GetMeColaborador().pipe(
      map(resposta => resposta.permissoes.some()),
      catchError(() => of(false))
    );
  }

}
