import { PossuiEspecial } from 'src/app/validation/CaractereEspecial';
import { ElementRef } from '@angular/core';
import { ColaboradorEditar } from './../../interfaces/ColaboradorEditar';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { switchMap } from 'rxjs';
import { ColaboradorCadastro, Telefone, idColaborador } from 'src/app/interfaces/ColaboradorCadastro';
import { ColaboradorDetalhes } from 'src/app/interfaces/ColaboradorDetalhes';
import { DataDuplicada } from 'src/app/interfaces/DataDuplicada';
import { EmailCadastro } from 'src/app/interfaces/EmailCadastro';
import { ListaId } from 'src/app/interfaces/ListaId';
import { PermissaoCadastro } from 'src/app/interfaces/PermissaoCadastro';
import { TelefoneCadastro } from 'src/app/interfaces/TelefoneCadastro';
import { ColaboradorService } from 'src/app/services/Colaborador/colaborador-service.service';
import { TelefoneServiceService } from 'src/app/services/Telefone/telefone-service.service';
import { ContemNumero } from 'src/app/validation/CaractereNumerico';
import { passwordMatch } from 'src/app/validation/ConfirmarSenha';
import { ContemMaiuscula } from 'src/app/validation/Uppercase';
import { CpfValidator } from 'src/app/validation/validarCpf';
import { DoubleMsgComponent } from '../comp/double-msg/double-msg.component';
import { AlterarSenhaComponent } from '../alterar-senha/alterar-senha.component';
import { EmailServiceService } from 'src/app/services/Email/email-service.service';
import { PermissoesServiceService } from 'src/app/services/Permissao/permissoes-service.service';

@Component({
  selector: 'app-cadastro-colaborador',
  templateUrl: './cadastrar-colaborador.component.html',
  styleUrls: ['./cadastrar-colaborador.component.css']
})
export class CadastrarColaboradorComponent implements OnInit {

  displayedColumnsTelefone: string[] = ['Apelido', 'Telefone', 'TipoContato', 'Excluir'];
  displayedColumnsEmail: string[] = ['Email', 'TipoContato', 'Excluir'];
  dataSourceTelefone! : MatTableDataSource<TelefoneCadastro>
  dataSourceEmail! : MatTableDataSource<EmailCadastro>
  formulario! : FormGroup
  colaborador! : ColaboradorDetalhes
  isMaster : boolean = false
  editar : boolean = false
  emailInvalido : boolean = false
  idColaborador! : number 
  EmailCadastrados : EmailCadastro[] = []
  TelefoneCadastrados : TelefoneCadastro[] = []
  ListaInvalidaEmail : boolean = true
  ListaInvalidaTelefone : boolean = true
  ListaDeletarEmail : ListaId[] = []
  ListaDeletarTelefone : ListaId[] = []

  MockTelefone : TelefoneCadastro[] = [
    {
      idTelefone: 1,
      numero: '',
      apelido: '',
      flPrincipal: true
    },
  ]
  MockEmail : EmailCadastro[] = [
    {
      idEmail: 1,
      email: '',
      flPrincipal: true
    },
  ]

  constructor(
    private elemento : ElementRef,
    private serviceColaborador : ColaboradorService,
    private serviceEmail : EmailServiceService,
    private serviceTelefone : TelefoneServiceService,
    private servicePermissoes : PermissoesServiceService,
    private getRoute: ActivatedRoute,
    private dialog: MatDialog,
    private route : Router
    ) { }

  ngOnInit() : void{
    this.getRoute.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.idColaborador = Number(id)
        this.editar = true
        this.formulario = new FormGroup({
          nome : new FormControl("", [Validators.required]),
          cpf : new FormControl("", [Validators.required, Validators.minLength(11), CpfValidator()]),
          usuario : new FormControl("", [Validators.required]),
          permissaoEditar : new FormControl(""),
          permissaoExcluir: new FormControl(""),
          permissaoCadastrar: new FormControl(""),
          perfil: new FormControl("2"),
        })
        this.ConsultaColaborador(Number(id));
        this.ListaInvalidaEmail = false;
        this.ListaInvalidaTelefone = false;
      }else{
        this.formulario = new FormGroup({
          nome : new FormControl("", [Validators.required]),
          cpf : new FormControl("", [Validators.required, Validators.minLength(11), CpfValidator()]),
          usuario : new FormControl("", [Validators.required]),
          permissaoEditar : new FormControl(""),
          permissaoExcluir: new FormControl(""),
          permissaoCadastrar: new FormControl(""),
          perfil: new FormControl("2"),
          senha : new FormControl("", [Validators.required, Validators.minLength(8), PossuiEspecial(), ContemMaiuscula(), ContemNumero()]),
          confirmarSenha : new FormControl("", [Validators.required]),
        }, [passwordMatch("senha","confirmarSenha")])
      }
    });
    this.dataSourceTelefone = new MatTableDataSource<TelefoneCadastro>(this.MockTelefone);
    this.dataSourceEmail = new MatTableDataSource<EmailCadastro>(this.MockEmail);
  }

  debug(){
    console.log(this.formulario)
  }

  // OBSERVABLES E CHAMADAS NA SERVICE

  // GetMe(){
  //   this.serviceColaborador.GetMeColaborador().pipe(takeUntil(this.unSubscribe$)).subscribe({
  //     next: (res) => {
  //       res.idTipoUsuario == 1 ? this.isMaster = true : this.isMaster = false
  //     },
  //     error: () => {
  //       this.isMaster = false
  //     }
  //   })
  // }

  ExcluirEmail(idEmail : ListaId[]){
    this.serviceEmail.DeletarEmail(idEmail).subscribe({
      next: () => {
        console.log('Email deletado')
      },
      error: () => {
        console.log('Erro ao deletar emails')
      }
    })
  }

  ConsultaColaborador(id : number){
    this.serviceColaborador.ConsultaPorId(id).subscribe({
      next: (colaborador) => {
        this.colaborador = colaborador
        this.AtualizandoFormulario(colaborador)
        this.EmailCadastrados = colaborador.emails
        this.TelefoneCadastrados = colaborador.telefone
        this.MockEmail = colaborador.emails
        this.MockTelefone = colaborador.telefone
        this.dataSourceTelefone = new MatTableDataSource<TelefoneCadastro>(this.MockTelefone);
        this.dataSourceEmail = new MatTableDataSource<EmailCadastro>(this.MockEmail);
      }
    })
  }

  AtualizandoFormulario(colaborador : ColaboradorDetalhes){
    colaborador.permissoes.forEach(permissao => {
      if (permissao.idPermissao == 1) {
        this.formulario.controls['permissaoEditar'].setValue(true)
      }
      if (permissao.idPermissao == 3) {
        this.formulario.controls['permissaoExcluir'].setValue(true)
      }
      if (permissao.idPermissao == 2) {
        this.formulario.controls['permissaoCadastrar'].setValue(true)
      }
    })

    this.formulario.patchValue({
      nome: colaborador.nome,
      cpf: colaborador.cpf,
      usuario: colaborador.usuario,
      perfil: colaborador.idTipoUsuario
    });
  }

  CadastrarColaborador(event : Event){
    event.preventDefault()

    const colaborador : ColaboradorCadastro = {
      nome : this.formulario.value.nome,
      cpf : this.formulario.value.cpf,
      usuario : this.formulario.value.usuario,
      senha : this.formulario.value.senha,
      confirmarSenha : this.formulario.value.confirmarSenha,
      idTipoUsuario : this.formulario.value.perfil,
      emails: this.MockEmail,
      telefones: this.MockTelefone
    }

    this.serviceColaborador.CadastrarColaborador(colaborador).subscribe({
      next: (data : idColaborador) => {
        this.CadastrarEmail(data.idColaborador)
        this.CadastrarTelefone(data.idColaborador)
        this.CadastrarPermissoes(data.idColaborador)
      },
      error: (data) => {
        this.pushMessageError(data.error)
      }
    })
  }

  AlterarColaborador(event : Event){
    event.preventDefault()
    const colaborador : ColaboradorEditar = {
      nome : this.formulario.value.nome,
      idTipoUsuario : this.formulario.value.perfil,
      emails: this.MockEmail,
      telefones: this.MockTelefone
    }

    this.serviceColaborador.EditarColaborador(colaborador, this.idColaborador)
    .pipe()
    .pipe(switchMap(() => {
      return this.serviceEmail.DeletarEmail(this.ListaDeletarEmail)
    }))
    .pipe(switchMap(() => {
      return this.serviceTelefone.DeletarTelefone(this.ListaDeletarTelefone)
    }))
    .pipe(switchMap(() => {
      return this.serviceEmail.EditarEmails(this.MockEmail, this.idColaborador)
    }))
    .pipe(switchMap(() => {
      return this.serviceTelefone.EditarTelefones(this.MockTelefone, this.idColaborador)
    }))
    .subscribe({
      next: () => {
        this.AlterarPermissoes(this.idColaborador)
      },
      error: (data) => {
        // this.pushMessageErrorEditar()
      }
    })
  }

  AlterarEmails(event : Event){
    event.preventDefault()

     this.serviceEmail.EditarEmails(this.MockEmail, this.idColaborador).subscribe({
      next: (resposta) => {

      }
    })
  }

  AlterarTelefones(event : Event){
    event.preventDefault()

    this.serviceTelefone.EditarTelefones(this.MockTelefone, this.idColaborador)
  }

  AlterarPermissoes(idColaborador : number){
    const permissao : PermissaoCadastro[] = []
    if (this.formulario.value.permissaoEditar) {
      permissao.push({idColaborador, idPermissao: 1})
    }
    if (this.formulario.value.permissaoExcluir) {
      permissao.push({idColaborador, idPermissao: 3})
    }
    if (this.formulario.value.permissaoCadastrar) {
      permissao.push({idColaborador, idPermissao: 2})
    }
    if (permissao.length <= 0) {
      permissao.push({idColaborador, idPermissao: 0})
    }

    this.servicePermissoes.EditarPermissao(permissao, idColaborador).subscribe({
      next: () => {
        this.route.navigate(['/detalhes/' + this.idColaborador])
      },
      error: () => {
        console.log('Erro ao editar permissões')
        this.route.navigate(['/detalhes/' + this.idColaborador])
      }
    })
  }

  CadastrarEmail(idColaborador : number){
    this.serviceEmail.CadastrarEmail(this.MockEmail, idColaborador).subscribe({
      next: () => {
        this.route.navigate(['/cadastro'])
      },
    })
  }

  CadastrarTelefone(idColaborador : number){
    this.serviceTelefone.CadastrarTelefone(this.MockTelefone, idColaborador).subscribe({
      next: () => {
        this.route.navigate(['/cadastro'])
      },
    })
  }

  CadastrarPermissoes(idColaborador : number){
    const permissao : PermissaoCadastro[] = []
    if (this.formulario.value.permissaoEditar) {
      permissao.push({idColaborador, idPermissao: 1})
    }
    if (this.formulario.value.permissaoExcluir) {
      permissao.push({idColaborador, idPermissao: 3})
    }
    if (this.formulario.value.permissaoCadastrar) {
      permissao.push({idColaborador, idPermissao: 2})
    }

    this.servicePermissoes.CadastrarPermissao(permissao).subscribe({
      next: () => {
        console.log('Todos as permissões foram cadastradas')
      },
      error: () => {
        console.log('Erro ao cadastrar permissões')
      }
    })
  }

  //Telefone
  RemoverTelefone(idTelefone : number, event : Event){
    event.preventDefault()
    if (this.MockTelefone.length > 1) {
      const index = this.MockTelefone.findIndex(element => element.idTelefone == idTelefone);
      if (index !== -1) {
        if (this.MockTelefone[index].flPrincipal != false ) {
          this.MockTelefone.splice(index, 1);
          this.MockTelefone[0].flPrincipal = true
          this.dataSourceTelefone = new MatTableDataSource<TelefoneCadastro>(this.MockTelefone);
          this.ListaDeletarTelefone.push({id : idTelefone})
        } else{
          this.MockTelefone.splice(index, 1);
          this.dataSourceTelefone = new MatTableDataSource<TelefoneCadastro>(this.MockTelefone);
          this.ListaDeletarTelefone.push({id : idTelefone})
        }

      }
    }
  }

  AdicionarTelefone(event : Event){
    event.preventDefault()
    const telefone : TelefoneCadastro = {
      idTelefone: this.MockTelefone.length + 1,
      numero: '',
      apelido: '',
      flPrincipal: false
    }

    if (!this.MockTelefone.length) {
      telefone.flPrincipal = true
    }
    this.MockTelefone.push(telefone)
    this.dataSourceTelefone = new MatTableDataSource<TelefoneCadastro>(this.MockTelefone);
  }

  EditarTelefone(idTelefone : number){
    const index = this.MockTelefone.findIndex(element => element.idTelefone == idTelefone);
    this.MockTelefone[index].apelido = this.elemento.nativeElement.querySelector('#apelidoTelefone' + idTelefone).value;
    const inputNumero : HTMLInputElement = this.elemento.nativeElement.querySelector('#telefone' + idTelefone);
    const numeroFormatado: string = inputNumero.value.replace(/[^0-9]/g, '');
    const spanNumero : HTMLSpanElement = this.elemento.nativeElement.querySelector('#erroNumero' + idTelefone)

    if (numeroFormatado.length == 11) {
      this.ResetaErro(spanNumero, inputNumero)
      this.MockTelefone[index].numero = numeroFormatado;
    }
    else{
      this.CriaMensagemErro(spanNumero, inputNumero)
      this.MockTelefone[index].numero = numeroFormatado;
    }
    this.ValidaListaTelefone(inputNumero)
  }


  EditaTelefonePrincipal(idTelefone : number){
    console.log("Edita telefone principal");

    const index = this.MockTelefone.findIndex(element => element.idTelefone == idTelefone);
    if (this.elemento.nativeElement.querySelector('#telefonePrincipal' + idTelefone).value == 'false') {
      this.MockTelefone.forEach(Telefone => {
        Telefone.flPrincipal = false
      })
      this.MockTelefone[index].flPrincipal = true
    }
  }

  // EMAIL
  RemoverEmail(idEmail : number,event : Event){
    event.preventDefault()
    if (this.MockEmail.length > 1) {
      console.log(this.ListaDeletarEmail);
      const index = this.MockEmail.findIndex(element => element.idEmail == idEmail);
      const emailExiste =  this.EmailCadastrados.findIndex(element => element.idEmail == idEmail);
      if (index !== -1) {
        if (this.MockEmail[index].flPrincipal == true ) {
          index >= 1 ? this.MockEmail[index - 1].flPrincipal = true : this.MockEmail[index + 1].flPrincipal = true
        }
        if (emailExiste !== -1) {
          // Envia requição de delete para API
          this.ListaDeletarEmail.push({id : idEmail})
          //this.ExcluirEmail(idEmail)
          console.log(this.ListaDeletarEmail);


        }
        this.MockEmail.splice(index, 1);
        this.dataSourceEmail = new MatTableDataSource<EmailCadastro>(this.MockEmail);
      }
    }
  }

  AdicionarEmail(){
    const email : EmailCadastro = {
      idEmail: this.MockEmail.length + 1,
      email: '',
      flPrincipal: false
    }
    if (!this.MockEmail.length) {
      email.flPrincipal = true
    }

    this.MockEmail.push(email)
    this.dataSourceEmail = new MatTableDataSource<EmailCadastro>(this.MockEmail);
  }

  EditarEmail(idEmail : number){
    const index = this.MockEmail.findIndex(element => element.idEmail == idEmail);
    const email : HTMLInputElement = this.elemento.nativeElement.querySelector('#email' + idEmail);
    const spanEmail : HTMLSpanElement = this.elemento.nativeElement.querySelector('#erroEmail' + idEmail);
    if (this.ValidaEmail(email.value)) {
      this.ResetaErro(spanEmail, email)
      this.MockEmail[index].email = email.value;
    }
    else{
      this.CriaMensagemErro(spanEmail, email)
      this.MockEmail[index].email = "";
    }
    this.ValidaListaEmail()
  }

  ValidaEmail(email : string){
    const regex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/;
    return regex.test(String(email).toLowerCase());
  }

  EditaEmailPrincipal(idEmail : number){
    const index = this.MockEmail.findIndex(element => element.idEmail == idEmail);
    if (this.elemento.nativeElement.querySelector('#emailPrincipal' + idEmail).value == 'false') {
      this.MockEmail.forEach(email => {
        email.flPrincipal = false
      })
      this.MockEmail[index].flPrincipal = true
    }
  }

  ResetaErro(span: HTMLSpanElement, input: HTMLInputElement){
    input.classList.remove('error');
    span.classList.add('sumir');
      setTimeout(() => {
        span.classList.add('d-none')
        span.classList.remove('sumir');
      }, 1000);
  }

  CriaMensagemErro(span : HTMLSpanElement, email: HTMLInputElement){
    span.classList.remove('d-none')
    email.classList.add('error')
  }



  pushMessage($event : Event){
    $event.preventDefault()
    this.dialog.open(AlterarSenhaComponent, {
      data: {
        nome: this.colaborador.nome,
        id: this.idColaborador
      },
      panelClass: 'dialog-container-alterar-senha',
    })
  }

  pushMessageError(data : DataDuplicada){
    this.dialog.open(DoubleMsgComponent, {
      data: data,
      panelClass: 'dialog-container-alterar-senha',
    })
  }

  // pushMessageErrorEditar(){
  //   const dataDuplica : DataDuplicada = {
  //     erro: "Dados duplicados, ja cadastrados em outro colaborador"
  //   }
  //   this.dialog.open(DoubleMsgComponent, {
  //     data: dataDuplica,
  //     panelClass: 'dialog-container-alterar-senha',
  //   })
  // }

  ValidaListaEmail(){
    let erro = true
    this.MockEmail.forEach(email => {
      if (email.email != "" && this.ValidaEmail(email.email)) {
        erro = false
      }
    })
    this.ListaInvalidaEmail = erro
  }

  ValidaListaTelefone(inputTelefone : HTMLInputElement){
    let erro = false
    this.MockTelefone.forEach(telefone => {
      console.log(telefone.numero.length);

      if (telefone.numero.length != 11) {
        erro = true
      }
    })
    this.ListaInvalidaTelefone = erro
    console.log(this.ListaInvalidaTelefone);

  }

  FormataTexto(numero : string) : string{
    numero.split('');

    return `(${numero[0]}${numero[1]}) ${numero[2]}${numero[3]}${numero[4]}${numero[5]}${numero[6]}-${numero[7]}${numero[8]}${numero[9]}${numero[10]}`
  }

  AtribuirValorInput(idTelefone : number){

    let numero = this.MockTelefone.findIndex(element => element.idTelefone == idTelefone);
    if (numero != -1 && this.MockTelefone[numero].numero && this.MockTelefone[numero].numero.length == 11) {
      this.elemento.nativeElement.querySelector('#telefone' + idTelefone).value = this.FormataTexto(this.MockTelefone[numero].numero);
      this.elemento.nativeElement.querySelector('#telefone' + idTelefone).placeholder = 'Ex: (00) 00000-0000'
    }
  }

}

