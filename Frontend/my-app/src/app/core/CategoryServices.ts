import { HttpClient, HttpHeaders } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { uri } from "../Model/auth";
import { ApiResponse } from "../Model/Product";
import { CategoryModel } from "../Model/Category";

@Injectable({
    providedIn:`root`
})
export class CategoryServices{

    httpClient = inject(HttpClient)

    getCategories(){
     return this.httpClient.get<ApiResponse<CategoryModel[]>>(`${uri}/api/Category`)
    }
    getCategoryById(catId:number)
    {
        return this.httpClient.get<ApiResponse<CategoryModel>>(`${uri}/api/Category/${catId}`)
    }

}