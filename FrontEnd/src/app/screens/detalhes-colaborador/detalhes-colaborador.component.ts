import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { takeUntil } from 'rxjs';
import { Telefone } from 'src/app/interfaces/ColaboradorCadastro';
import { ColaboradorDetalhes } from 'src/app/interfaces/ColaboradorDetalhes';
import { EmailCadastro } from 'src/app/interfaces/EmailCadastro';
import { TelefoneCadastro } from 'src/app/interfaces/TelefoneCadastro';
import { ColaboradorService } from 'src/app/services/Colaborador/colaborador-service.service';

@Component({
  selector: 'app-detalhes-colaborador',
  templateUrl: './detalhes-colaborador.component.html',
  styleUrls: ['./detalhes-colaborador.component.css']
})
export class DetalhesColaboradorComponent{

  displayedColumnsTelefone: string[] = ['Apelido', 'Telefone', 'TipoContato'];
  displayedColumnsEmail: string[] = ['Email', 'TipoContato'];
  dataSourceTelefone! : MatTableDataSource<TelefoneCadastro>
  dataSourceEmail! : MatTableDataSource<EmailCadastro>
  formulario! : FormGroup
  colaborador! : ColaboradorDetalhes
  permissaoEditar : boolean = false
  permissaoRemover : boolean = false
  idColaborador! : number
}

  // constructor(
  //   private formBuilder : FormBuilder,
  //   private serviceColaborador : ColaboradorService,
  //   private route : Router,
  //   private getRoute: ActivatedRoute,
  //   private dialog: MatDialog
  //   ) { }

  // ngOnInit() : void{
  //   this.getRoute.paramMap.subscribe(params => {
  //     this.idColaborador = Number(params.get('id'));
  //     this.ConsultaColaborador(this.idColaborador);
  //     // this.GetMe()
  //   });

  //   this.formulario = this.formBuilder.group({
  //     nome : [],
  //     cpf : [],
  //     usuario : [],
  //     permissaoEditar : [],
  //     permissaoExcluir : [],
  //     permissaoCadastrar : [],
  //     perfil : [],
  //   })
  // }

  // GetMe(){
  //   this.serviceColaborador.GetMeColaborador().pipe(takeUntil(this.unSubscribe$)).subscribe({
  //     next: (res) => {
  //       this.permissaoEditar =  res.permissoes.findIndex(element => element.idPermissao == 1) != -1
  //       this.permissaoRemover =  res.permissoes.findIndex(element => element.idPermissao == 3) != -1
  //     },
  //     error: () => {
  //       localStorage.clear()
  //       this.route.navigate(['/login'])
  //     }
  //   })
  // }

  // ConsultaColaborador(id : number){
  //   this.serviceColaborador.ConsultaPorId(id).subscribe({
  //     next: (colaborador) => {
  //       this.colaborador = colaborador
  //       this.AtualizandoFormulario(colaborador)

  //       this.dataSourceTelefone = new MatTableDataSource<TelefoneCadastro>();
  //       this.dataSourceEmail = new MatTableDataSource<EmailCadastro>();
  //     },
  //     error: () => {
  //       this.route.navigate(['/cadastro'])
  //     }
  //   })
  // }

  // AtualizandoFormulario(colaborador : ColaboradorDetalhes){
  //   colaborador.permissoes.forEach(permissao => {
  //     if (permissao.idPermissao == 1) {
  //       this.formulario.controls['permissaoEditar'].setValue(true)
  //     }
  //     if (permissao.idPermissao == 3) {
  //       this.formulario.controls['permissaoExcluir'].setValue(true)
  //     }
  //     if (permissao.idPermissao == 2) {
  //       this.formulario.controls['permissaoCadastrar'].setValue(true)
  //     }
  //   })

    // this.formulario.patchValue({
    //   nome: colaborador.nome,
    //   cpf: colaborador.cpf,
    //   usuario: colaborador.usuario,
    //   perfil: colaborador.idTipoUsuario == 1 ? "Master" : "Padrão"
    // });
  //}

  // pushMessage($event : Event){
  //   $event.preventDefault()
  //   this.dialog.open(DialogRemoverComponent, {
  //     data: {
  //       id: this.idColaborador
  //     },
  //     panelClass: 'dialog-container-alterar-senha',
  //   })
  // }

