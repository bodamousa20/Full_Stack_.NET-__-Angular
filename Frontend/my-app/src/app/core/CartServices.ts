import { HttpClient } from "@angular/common/http";
import { inject, Injectable, signal } from "@angular/core";
import { uri } from "../Model/auth";
import { CartResponse } from "../Model/Cart";

@Injectable({
    providedIn:"root"
})
export class CartServices{

    httpClient = inject(HttpClient)
    cartCounter = signal<number>(0);


    addProductToCart(productID:number,quanity:number = 1){
        console.log(quanity);
      return this.httpClient.post<CartResponse>(`${uri}/api/cart`, {productID,quanity})
    }

    getUserCart(){
        return this.httpClient.get<CartResponse>(`${uri}/api/cart`)
    }


    deleteCartItem(productId:Number){
        return this.httpClient.delete<CartResponse>(`${uri}/api/cart/cart-item/${productId}`)
    }
    ClearCart(){
        return this.httpClient.delete(`${uri}/api/cart`)
    }
    IncreaseQuantity(productId:number){
        return this.httpClient.post<CartResponse>(`${uri}/api/cart/increase-qnty/${productId}`,{})
    }
    decreaseQuantity(productId:number){
        return this.httpClient.post<CartResponse>(`${uri}/api/cart/decrease-qnty/${productId}`,{})
    }




}