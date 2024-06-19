import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { AlterarSenha } from 'src/app/interfaces/AlterarSenha';
import { ColaboradorService } from 'src/app/services/Colaborador/colaborador-service.service';
import { ContemNumero } from 'src/app/validation/CaractereNumerico';
import { passwordMatch } from 'src/app/validation/ConfirmarSenha';
import { ContemMaiuscula } from 'src/app/validation/Uppercase';
import { MsgErrorComponent } from '../comp/msg-error/msg-error.component';

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
        confirmarSenhaNova: new FormControl("", [Validators.required]),

      }, [passwordMatch("senhaNova", "confirmarSenhaNova")])
    }

    onSubmit(){
      const senhaAtual = this.formulario.controls['senhaAtualTroca']?.value
    const senhaNova = this.formulario.controls['senhaNovaTroca']?.value
    const confirmarSenha = this.formulario.controls['confirmarSenhaTroca']?.value
    const senhas : AlterarSenha = {senhaAtual, senhaNova, confirmarSenha }

    this.msgErro = false
    this.loading = true

    this.service.AlterarSenha(senhas).subscribe({
      next: () => {
        setTimeout(() => {
          this.router.navigate(["/home"])
          this.loading = false
        }, 1000);
      },
      error: () =>{
        this.loading = false
        this.msgErro = true
        this.pushMessage()
      }
    })
    }

    pushMessage(){
      const dialogRef = this.dialog.open(MsgErrorComponent, {
        panelClass: 'dialog-container',
        backdropClass: 'custom-dialog-backdrop'
      })
    }
}
