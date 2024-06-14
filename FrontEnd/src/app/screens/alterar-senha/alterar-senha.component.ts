import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ColaboradorService } from 'src/app/services/Colaborador/colaborador-service.service';
import { ContemNumero } from 'src/app/validation/CaractereNumerico';
import { passwordMatch } from 'src/app/validation/ConfirmarSenha';
import { ContemMaiuscula } from 'src/app/validation/Uppercase';

@Component({
  selector: 'app-alterar-senha',
  templateUrl: './alterar-senha.component.html',
  styleUrls: ['./alterar-senha.component.css']
})
export class AlterarSenhaComponent implements OnInit {
  formulario!: FormGroup
  loading: boolean = false;
  msgErro: boolean = false

  constructor(
    private FormBuilder: FormBuilder,
    private service: ColaboradorService,
    private router: Router,
    private dialog: MatDialog
    ){  }

    ngOnInit(): void {
      this.formulario = new FormGroup({
        senhaAtual : new FormControl("", [Validators.required]),
        senhaNova : new FormControl("", [Validators.required, Validators.minLength(8), ContemMaiuscula(), ContemNumero()]),

      }

    )
    }
}
