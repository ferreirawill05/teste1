import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/app/environment/API';
import { PermissaoCadastro } from 'src/app/interfaces/PermissaoCadastro';

@Injectable({
  providedIn: 'root'
})
export class PermissoesServiceService {

  private readonly API = environment.apiUrl

  constructor(private http : HttpClient) { }

  CadastrarPermissao(permissoes : PermissaoCadastro[]){
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem("token")}`);
    return this.http.post(this.API + "permissoes", permissoes, {headers})
  }

  EditarPermissao(permissoes : PermissaoCadastro[],idColaborador : number){
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem("token")}`);
    return this.http.put(this.API + "permissoes/" + idColaborador, permissoes, {headers})
  }

}
