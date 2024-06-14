import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs';
import { ColaboradorService } from 'src/app/services/Colaborador/colaborador-service.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  nome : string = "william"

  constructor(
    private router: Router,
    private serviceColaborador : ColaboradorService
  ){}

  ngOnInit(): void {
    //this.GetMe()
  }

  // GetMe(){
  //   this.serviceColaborador.GetMeColaborador().subscribe({
  //     next: (res) => {
  //       this.nome = res.nome
  //     },
  //     error: () => {
  //       localStorage.clear()
  //       this.router.navigate(['/login'])
  //     }
  //   })
  // }

  Logout(){
    localStorage.removeItem('token')
    this.router.navigate(["/login"])
  }


  // VerificaPermissaoEditar(){
  //   return this.serviceColaborador.VerificaPermissao(1).pipe(
  //     map(hasPermission => {
  //       if (hasPermission) {
  //         console.log(true, hasPermission);
  //       } else {
  //         console.log(false, hasPermission);
  //       }
  //     })
  //   );
  }

