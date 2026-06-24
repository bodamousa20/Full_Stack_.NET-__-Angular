import { Component, inject, signal } from '@angular/core';
import { OrderServices } from '../../core/OrderServices';
import { Order } from '../../Model/Order';

@Component({
  selector: 'app-checkout',
  imports: [],
  templateUrl: './checkout.html',
  styleUrl: './checkout.css',
})
export class Checkout {


  services = inject(OrderServices);
  orders = signal<Order[] | null>(null);
  skeletonItems = [1, 2, 3];


  ngOnInit(){
    this.getAllOrders();
  }

  getAllOrders(){
      this.services.getAllOrders().subscribe({
        next:(response)=>{
          console.log(response)
          this.orders.set(response.orders);
        }
      })
    }

}
