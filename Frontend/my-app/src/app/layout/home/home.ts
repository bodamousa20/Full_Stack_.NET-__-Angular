import { Component, inject, OnInit, signal } from '@angular/core';
import { productServices } from '../../core/productServices';
import { Product } from '../../Model/Product';
import { Router, RouterLink } from '@angular/router';
import { CategoryServices } from '../../core/CategoryServices';
import { CategoryModel } from '../../Model/Category';
import { CartServices } from '../../core/CartServices';

@Component({
  selector: 'app-home',
  imports: [RouterLink],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit {

  product_Services = inject(productServices);
  categoryServices = inject(CategoryServices);
  cart_Services = inject(CartServices);
  products = signal<Product[]| null>(null);
  categories = signal<CategoryModel[] | null>(null);
  skeletonItems = Array(6).fill(0);

  router = inject(Router)
  
  ngOnInit() {
    this.getProducts();
    this.getCategories();
  
  }

  getCategories(){
    this.categoryServices.getCategories().subscribe({
      next:(res)=>{
        this.categories.set(res.data)
      }
    })
  }

  AddProductTocart(productID: number) {
      this.cart_Services.addProductToCart(productID).subscribe({
        next:(res)=>{
          console.log("at cart home eeee")
          console.log(res)
          this.cart_Services.cartCounter.update(p=>p+1)
          this.router.navigate(['/user/cart'])
        }
        
      })
  }
  getProductById(id:number){
    this.router.navigate(['/user/product-details',id])
  }
  getProducts(){
    this.product_Services.getAllProducts(2).subscribe({
      next:(data)=>{
        console.log(data);
        this.products.set(data.data);
      }
    })
  }


 
}
