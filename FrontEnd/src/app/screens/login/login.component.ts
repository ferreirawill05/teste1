import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { LoginService } from 'src/app/services/Login/login.service';
import { MsgErrorComponent } from '../comp/msg-error/msg-error.component';


  @Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {

    formulario! : FormGroup
    usuario : string = localStorage?.getItem('usuario')?? ""
    loading : boolean = false;
    msgError : boolean = false;

    constructor(
      private formBuilder : FormBuilder,
      private service : LoginService,
      private router: Router,
      private dialog: MatDialog
    ) {}

    ngOnInit(): void {
      this.formulario = this.formBuilder.group({
        usuario : [this.usuario, [Validators.required]],
        senha : ['', [Validators.required, Validators.minLength(8)]],
        lembrar : [[]]
      })

    }

    onSubmit(){
      const usuario = this.formulario.controls['usuario']?.value
      const senha = this.formulario.controls['senha']?.value
      const lembrar = this.formulario.controls['lembrar']?.value
      this.loading = true
      this.msgError = false;

      lembrar ? localStorage.setItem('usuario', usuario) : localStorage.clear()

      this.service.Logar(usuario, senha).subscribe({
          next: (res) => {

            localStorage.setItem("token", res.token)
            this.router.navigate(['/home'])
            this.loading = false
          },
          error: () => {
            this.loading = false
            this.msgError = true
            this.pushMessage()
          }
        }
      )
    }

    pushMessage(){
      const dialogRef = this.dialog.open(MsgErrorComponent, {
        panelClass: 'dialog-container',
        backdropClass: 'custom-dialog-backdrop'
      })
    }
  }

