import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { ColaboradorCadastro, Telefone } from 'src/app/interfaces/ColaboradorCadastro';
import { TelefoneServiceService } from 'src/app/services/Telefone/telefone-service.service';

@Component({
  selector: 'app-cadastrar-colaborador',
  templateUrl: './cadastrar-colaborador.component.html',
  styleUrls: ['./cadastrar-colaborador.component.css']
})
export class CadastrarColaboradorComponent implements OnInit{

displayedColumns: string[] = ['apelido', 'telefone', 'tipoContato']
Colaboradores! : Telefone[]
dataSource! : MatTableDataSource<ColaboradorCadastro>

constructor(
  private service: TelefoneServiceService,
  private router: Router
){}

ngOnInit(): void {
    this.ListandoTelefones()
}

ListandoTelefones(){
   
}
}
