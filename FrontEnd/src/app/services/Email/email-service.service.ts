import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/app/environment/API';
import { EmailCadastro } from 'src/app/interfaces/EmailCadastro';
import { ListaId } from 'src/app/interfaces/ListaId';

@Injectable({
  providedIn: 'root'
})
export class EmailServiceService {

  private readonly API = environment.apiUrl

  constructor(private http : HttpClient) { }

  CadastrarEmail(email : EmailCadastro[], idColaborador : number){
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem("token")}`);
    return this.http.post(this.API + "emails/" + idColaborador, email, {headers})
  }

  EditarEmails(email : EmailCadastro[], idColaborador : number) : Observable<string[]>{
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem("token")}`);
    return this.http.put<string[]>(this.API + "emails/" + idColaborador, email, {headers})
  }

  DeletarEmail(id : ListaId[]){
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem("token")}`);
    return this.http.post(this.API + "emails/excluir", id, {headers})
  }
}
 