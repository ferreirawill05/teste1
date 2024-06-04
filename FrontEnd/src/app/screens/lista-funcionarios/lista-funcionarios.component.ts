import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { takeUntil } from 'rxjs';
import { ColaboradorListado, Colaboradores } from 'src/app/interfaces/ColaboradorListado';
import { ListaFiltros } from 'src/app/interfaces/ListaFiltros';
import { ColaboradorService } from 'src/app/services/Colaborador/colaborador-service.service';



@Component({
  selector: 'app-lista-funcionarios',
  templateUrl: './lista-funcionarios.component.html',
  styleUrls: ['./lista-funcionarios.component.css'],

})
export class ListaFuncionariosComponent implements OnInit{

displayedColumns: string[] = ['nomeFuncionario', 'cpf', 'telefonePrincipal', 'emailPrincipal', 'dataCriacao', 'cadastradoPor'];
colaboradores! : Colaboradores[]
dataSource! : MatTableDataSource<Colaboradores>
page: number = 0;
pageSize: number = 5;
quantidadeColaboradores : number = 0
dataInicial : Date = new Date(1800,0o1,0o1)
dataFinal : Date = new Date(1800,0o1,0o1)
pesquisa : string = ''
registroTemporal : boolean = true
permissaoCadastrar : boolean = false


  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private service : ColaboradorService,
    private router : Router
    ){}

  ngOnInit(): void {
    this.ListandoColaboradores()
  }

  ListandoColaboradores(){
    // tipar ListaFiltros
    const pagina : ListaFiltros  = {
      page: this.page * this.pageSize,
      pageSize: this.pageSize,
      quantidadeColaboradores: this.quantidadeColaboradores,
      dataInicial: this.dataInicial,
      dataFinal: this.dataFinal,
      pesquisa: this.pesquisa,
      registroTemporal: this.registroTemporal
    }
    console.log(pagina);

    this.service.ListarColaboradores(pagina).subscribe({
      next: (resposta : ColaboradorListado) => {
        this.colaboradores = resposta.colaboradores
        console.log(this.colaboradores);
        this.colaboradores.forEach(element => {
          let date = element.dtCriacao.split(' ')[0]
          let dateArray = date.split('/')
          element.dtCriacao = dateArray[1] + '/' + dateArray[0] + '/' + dateArray[2]
        })

        this.quantidadeColaboradores = resposta.numero
        this.dataSource = new MatTableDataSource<Colaboradores>(this.colaboradores);
      },
      error: (e) => {console.log(e)}
    })
  }

  onPaginateChange(event : PageEvent) {

    this.page = event.pageIndex;
    this.pageSize = event.pageSize;

    this.ListandoColaboradores()
  }

  FiltroAntigos(){
    if (this.registroTemporal) {
      this.registroTemporal = false
      this.ListandoColaboradores()
    }
  }

  FiltroRecentes(){
    if (!this.registroTemporal) {
      this.registroTemporal = true
      this.ListandoColaboradores()
    }
  }

  RedirectDetalhes(colaborador : any){
    let rota = 'detalhes/' + colaborador.idColaborador
    this.router.navigate([rota])
  }

}
