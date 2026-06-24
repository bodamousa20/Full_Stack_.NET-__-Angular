
import { Component, signal, inject } from '@angular/core';
import { Product } from '../../Model/Product'; // Your data model
import { productServices } from '../../core/productServices';
import { RouterLink } from "@angular/router";
import { FormsModule } from '@angular/forms';
import { CategoryServices } from '../../core/CategoryServices';
import { Category } from '../category/category';
import { FormField } from "@angular/forms/signals";
import { CategoryModel } from '../../Model/Category';
@Component({
  selector: 'app-product',
  imports: [RouterLink, FormsModule],
  templateUrl: './products.html',
  styleUrl: './products.css',
})
export class Products {


  services = inject (productServices);
  catServices = inject (CategoryServices);
  categories = signal<CategoryModel[]| null>(null);

  products = signal<Product[]| null>(null);
  pageNumber = signal<number>(1);
  
  filter:string|null = null
  selectedCategoryId:number|'All' ='All';
  minprice:number = 0
  maxprice:number = 0
  sort:boolean| null = null;
  name:string = ''
  pageno: number = 0;
  totalProducts: number = 0;
  pageSize: number = 0;
currentFilter = {
  name: '',
  categoryId: undefined as number | undefined,
  minPrice: undefined as number | undefined,
  maxPrice: undefined as number | undefined,
  sort: undefined as boolean | undefined
};
  ngOnInit(){
    this.loadProducts();
    this.loadCategory()
  }

  loadProducts(  ){
    this.services.getAllProducts(
      this.pageNumber(),
      this.currentFilter.name,
      this.currentFilter.categoryId,
      this.currentFilter.minPrice,
      this.currentFilter.maxPrice,
      this.currentFilter.sort
    ).subscribe({
      next:(res)=>{
        this.products.set(res.data)
        this.pageno = res.pagenumber,
        this.pageSize =res.pagesize
        this.totalProducts = res.total
      },
      error:(error)=>{
        console.log(error)
      }
    })
  }
  loadCategory(){
    this.catServices.getCategories().subscribe({
      next:(res)=>{
        this.categories.set(res.data)
      },error:(error)=>{
        console.log(error)
      }
    })
  }
clear() {
  this.filter = null
  this.selectedCategoryId ='All';
  this.minprice = 0
  this.maxprice = 0
  this.sort = null;
  this.name = ''
}
  ApplyFilter() {
   this.currentFilter = {
  name: this.name,
  categoryId: this.selectedCategoryId !== 'All'
      ? Number(this.selectedCategoryId)
      : undefined,

  minPrice: this.minprice > 0
      ? this.minprice
      : undefined,
  maxPrice: this.maxprice > 0
      ? this.maxprice
      : undefined,

  sort: this.sort ?? undefined

  };
this.pageNumber.set(1);
this.loadProducts();
}
  prevPage() {
  if(this.pageNumber() == 1)
    return 
  else{
    this.pageNumber.update(current =>current-1)
    this.loadProducts()
  }
  }
  nextPage() {
  console.log("next")
  this.pageNumber.update(current =>current+1)
  this.loadProducts()
}
  activePhotoSet(image?: string): string {
  
  // 1. If 'image' is provided (from the thumbnail loop), use it. 
  //    Otherwise, grab the current active photo from the product array (for the main image).
  const selectedImage = image 


  const url = selectedImage;

  // 3. Your logic: Check if it's an external URL
  if (url?.includes("http://") || url?.includes("https://")) {
    return url;
  } else {
    // 4. Local URL: Strip the leading slash just in case to prevent double slashes (//)
    const cleanUrl = url?.startsWith('/') ? url.substring(1) : url;
    return `https://localhost:7245/${cleanUrl}`;
  }
  }
getCatName(categoryId: number) {
return this.categories()?.find(p=>p.id == categoryId)?.name
}


  
}
