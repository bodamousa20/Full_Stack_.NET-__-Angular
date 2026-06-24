import { Component, inject, signal } from '@angular/core';
import { productServices } from '../../core/productServices';
import { ApiResponse, Product } from '../../Model/Product';
import { RouterLink } from '@angular/router';
@Component({
  selector: 'app-favourite',
  imports: [RouterLink],
  templateUrl: './favourite.html',
  styleUrl: './favourite.css',
})
export class Favourite{
  
  productServices = inject(productServices)
  favouriteProducts = signal<Product[]>([])

  ngOnInit(){
    this.getAllUserFav()
  }

  removeFavourite(prdId:number) {
    this.productServices.removeFavouriteProduct(prdId).subscribe({
      next:(res)=>{
        console.log(res)
        this.getAllUserFav();
      }
    })
  }

  getAllUserFav(){
    this.productServices.getFavouriteProduct().subscribe({
      next:(response)=>{
        this.favouriteProducts.set(response.data.products);
        this.productServices.favCount.set(response.data.products.length)
      }
    })
  }


}
