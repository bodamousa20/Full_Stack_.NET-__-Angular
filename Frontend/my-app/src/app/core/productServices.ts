import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { inject, Injectable, signal } from "@angular/core";
import { ApiResponse, Product, productDetails, productFavourite } from "../Model/Product";
import { uri } from "../Model/auth";
import { CookieService } from "ngx-cookie-service";
import { retry } from "rxjs";

@Injectable({
    providedIn:`root`
})
export class productServices{
    httpClient = inject(HttpClient)
    cookie = inject(CookieService)
    favCount = signal<number>(0);
    getAllProducts(
  pageNumber: number,
  nameQuery?:string,
  CategoryId?: number,
  MinPrice?: number,
  MaxPrice?: number,
  Sort?: boolean
) {
  let param = new HttpParams();
  
  if(nameQuery !== undefined)
    param = param.set("query",nameQuery);

  if (CategoryId !== undefined)
    param = param.set("CategoryId", CategoryId);

  if (MinPrice !== undefined)
    param = param.set("MinPrice", MinPrice);

  if (MaxPrice !== undefined)
    param = param.set("MaxPrice", MaxPrice);

  if (Sort !== undefined)
    param = param.set("Sort", Sort);

  param = param.set("pageNumber", pageNumber);

  return this.httpClient.get<ApiResponse<Product[]>>(
    `${uri}/api/product`,
    { params: param }
  );
}


    GetProductById(id:number){
        return this.httpClient.get<ApiResponse<productDetails>>(`${uri}/api/product/${id}`)
    }

    deleteProduct(id:number){
        return this.httpClient.delete(`${uri}/api/product/${id}`)
    }
    addedProductToFavourite(productId:number){
      return this.httpClient.post(`${uri}/api/user/favourites/${productId}`,{})
    }
    getFavouriteProduct(){
      return this.httpClient.get<ApiResponse<productFavourite>>(`${uri}/api/user/favourites`)
    }
    removeFavouriteProduct(productId:number){
      return this.httpClient.post(`${uri}/api/user/remove-favourite/${productId}`,{})
    }
  

}