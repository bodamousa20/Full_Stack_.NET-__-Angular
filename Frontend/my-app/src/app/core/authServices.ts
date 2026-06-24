import { HttpClient, HttpParams } from "@angular/common/http";
import { inject, Injectable, signal } from "@angular/core";
import { ApiResponse, Product } from "../Model/Product";
import { parseTemplate } from "@angular/compiler";
import { LoginFromData, LoginResponse, RegisterFormData, uri, UserProfile } from "../Model/auth";
import { CookieService } from "ngx-cookie-service";
import { Router } from "@angular/router";
import { Profile } from "../layout/profile/profile";

@Injectable({
    providedIn:'root'
})
export class AuthServices
{
    
    private readonly httpClient = inject(HttpClient);
    private readonly cookie = inject(CookieService)
    private readonly router = inject(Router)
  isLoggedIn = signal<boolean>(
    !!localStorage.getItem('userInfo')
  );   

    constructor() {
       this.isLoggedIn.set(
        localStorage.getItem("userInfo") !=null
       )
        
    }
    RegsiterUser(body:RegisterFormData){
        return this.httpClient.post(`${uri}/api/Auth/register`,body)
    }

    LoginUser(body:LoginFromData){
      return this.httpClient.post<LoginResponse>(`${uri}/api/Auth/Login`,body)
    }

    logout() {
        localStorage.removeItem("userInfo")
        //remove the cokkie 
        this.cookie.delete("token", "/");
        this.router.navigate(['login'])
    }
    
    getUserProfile(){
        return this.httpClient.get<ApiResponse<UserProfile>>(`${uri}/api/user/profile`)
    }
    

}