import { CanActivateFn, Router } from '@angular/router';

export const authenticatedGuard: CanActivateFn = (route, state) => {
  if (localStorage.getItem("token")){
    return true;
  }
  new Router().navigate(['/login'])
  return false
};
