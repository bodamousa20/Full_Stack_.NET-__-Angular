import { HttpClient } from "@angular/common/http";
import { inject, Injectable, signal } from "@angular/core";
import { AllOrder, Order } from "../Model/Order";
import { uri } from "../Model/auth";

@Injectable({
    providedIn:'root'
})
export class OrderServices{


    httpClient = inject(HttpClient)
    Orders = signal<AllOrder | null >(null)
    CheckOut(address:string ,postalCode:string){
        return this.httpClient.post<Order>(`${uri}/api/cart/checkout`,{address,postalCode})
    }
    getAllOrders(){
    return this.httpClient.get<AllOrder>(`${uri}/api/cart/Orders`)
    }



}