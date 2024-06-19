import { CanActivateFn, Router } from '@angular/router';
import { ColaboradorService } from '../services/Colaborador/colaborador-service.service';
import { map } from 'rxjs';

export class DeletarGuard {

  constructor(private colaboradorService: ColaboradorService, private router: Router) {}


  VerificaPermissaoDeletar(){
    return this.colaboradorService.VerificaPermissao(3).pipe(
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
    return this.VerificaPermissaoDeletar();
  }
}
