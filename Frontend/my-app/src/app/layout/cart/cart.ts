import { Component, inject, signal } from '@angular/core';
import { CartServices } from '../../core/CartServices';
import { CartResponse } from '../../Model/Cart';
import { Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { OrderServices } from '../../core/OrderServices';
@Component({
  selector: 'app-cart',
  imports: [RouterLink,FormsModule],
  templateUrl: './cart.html',
  styleUrl: './cart.css',
})
export class Cart {

    router = inject(Router)
    cartServices = inject(CartServices);
    orderServices = inject(OrderServices)
    cart = signal<CartResponse | null>(null);
    skeletonItems = Array(6).fill(0);
    address!: string;
    postalCode!: string;
    ngOnInit(){
      this.getCartItems();
    }

    getCartItems(){
      this.cartServices.getUserCart().subscribe({
        next:(ApiResponse)=>{
          this.cart.set(ApiResponse);
        }
      })
    }
    removeItem(productId:Number) {
      console.log(productId)
      this.cartServices.deleteCartItem(productId).subscribe({
        next:(response)=>{
          console.log(response);
          this.cart.set(response);
          this.cartServices.cartCounter.update(p=>p-1);

        }
      })
  }

  clearCart() {
     this.cartServices.ClearCart().subscribe({
      next:(data)=>{
         this.getCartItems();
         this.cartServices.cartCounter.set(0);
      }
     })
  }
  increase(productId:number) {
    this.cartServices.IncreaseQuantity(productId).subscribe({
      next:(response)=>{
        this.cart.set(response);
        console.log(response)
      }
    })
  }

  decrease(productId :number) {
    this.cartServices.decreaseQuantity(productId).subscribe({
      next:(response)=>{
        this.cart.set(response);
        console.log(response)
      }
    })


  }

  

  Checkout() {
    if(this.address == null || this.postalCode == null)
      return ;

    this.orderServices.CheckOut(this.address,this.postalCode).subscribe({
      next:(response)=>{
        this.router.navigate(["/user/checkout"])        
      }
    })
  }



}
