import { Component, inject, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { AuthServices } from '../../core/authServices';
import { CartServices } from '../../core/CartServices';
import { productServices } from '../../core/productServices';

@Component({
  selector: 'app-header',
  templateUrl: './header.html',
  styleUrl: './header.css',
  imports: [RouterLink],
})
export class Header {

  cookie = inject(CookieService);
  router = inject(Router);
  service = inject(AuthServices)
  cartService = inject(CartServices);
  productServices = inject(productServices)
  loggedIn = this.service.isLoggedIn;


  ngOnInit() {
  if (!this.loggedIn()) return;

    this.cartService.getUserCart().subscribe({
      next: (res) => {
        if(res.data != null)
        this.cartService.cartCounter.set(res.data.cartItemDtos.length); 
      }
    });

    this.productServices.getFavouriteProduct().subscribe({
      next:(res)=>{
        if(res.data.products != null)
        this.productServices.favCount.set(res.data.products.length);
      }
    })

    
    
  }
 
  getUserName(): string | null {
    const data = localStorage.getItem('userInfo');
    if (!data) return null;

    return JSON.parse(data).username;
  }

  logout() {
   this.service.logout();
   this.service.isLoggedIn.set(false);
  }
}