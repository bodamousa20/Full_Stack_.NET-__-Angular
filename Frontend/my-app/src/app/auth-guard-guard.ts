import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

export const authGuardGuard: CanActivateFn = (route, state) => {
  const cookie = inject (CookieService)
    const router = inject(Router);

  if(cookie.get("token")){
    return true
  }
  
  return router.createUrlTree(['/login']);
};
