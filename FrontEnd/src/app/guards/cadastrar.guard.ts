import { ColaboradorService } from 'src/app/services/Colaborador/colaborador-service.service';
import { CanActivateFn, Router } from '@angular/router';
import { map } from 'rxjs';

export class CadastrarGuard {

  constructor(private colaboradorService: ColaboradorService, private router: Router) {}


  VerificaPermissaoCadastrar(){
    return this.colaboradorService.VerificaPermissao(2).pipe(
      map(hasPermission => {
        if (hasPermission) {
          return true;
        } else {
          this.router.navigate(['/login']);
          return false;
        }
      })
    );
  }

  canActivate: CanActivateFn = (route, state) => {
    return this.VerificaPermissaoCadastrar();
  }
}
