import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/app/environment/API';
import { Telefone } from 'src/app/interfaces/ColaboradorCadastro';
import { ListaId } from 'src/app/interfaces/ListaId';
import { TelefoneCadastro } from 'src/app/interfaces/TelefoneCadastro';

@Injectable({
  providedIn: 'root'
})
export class TelefoneServiceService {

  private readonly API = environment.apiUrl

  constructor(private http : HttpClient) { }

  CadastrarTelefone(telefones : TelefoneCadastro[], idColaborador: number){
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem("token")}`);
    return this.http.post(this.API + "telefones/" + idColaborador, telefones, {headers})
  }

  EditarTelefones(telefones : TelefoneCadastro[], idColaborador: number) : Observable<string[]>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem("token")}`);
    return this.http.put<string[]>(this.API + "telefones/" + idColaborador, telefones, {headers})
  }

  DeletarTelefone(id : ListaId[]){
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem("token")}`);
    return this.http.post(this.API + "telefones/excluir", id, {headers})
  }

}
