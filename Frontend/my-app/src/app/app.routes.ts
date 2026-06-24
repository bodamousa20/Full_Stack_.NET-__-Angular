import { Routes } from '@angular/router';
import { Login } from './features/Auth/login/login';
import { Register } from './features/Auth/register/register';
import { Notfound } from './features/notfound/notfound';
import { ProductDetails } from './layout/product-details/product-details';
import { Home } from './layout/home/home';
import { authGuardGuard } from './auth-guard-guard';
import {Products } from './layout/product/products';
import { Favourite } from './layout/user-favourite/favourite'; 
import { Cart } from './layout/cart/cart';
import { Profile } from './layout/profile/profile';
import { Checkout } from './layout/checkout/checkout';

export const routes: Routes = [
    {
        path:"login",
        component:Login
        
    },
    {
        path:"register",
        component:Register
    },
    {
        canActivate:[authGuardGuard],
        path:"user/home",
        component:Home
    },
    {
        canActivate:[authGuardGuard],
        path:"user/all-products",
        component:Products
    },
    {   
        canActivate:[authGuardGuard],
        path:"user/checkout",
        component:Checkout
    },
    {
        canActivate :[authGuardGuard],
        path:"user/favourite",
        component:Favourite
    },
    {
        canActivate:[authGuardGuard],
        path:"user/profile",
        component:Profile
    },
    {
        canActivate :[authGuardGuard],
        path:"user/cart",
        component:Cart
    },
    {
        canActivate:[authGuardGuard],
        path:"user/product-details/:id",
        component:ProductDetails
    },
    {
       path:"",
       redirectTo:'/login',
       pathMatch:'full' 
    },
    {
        path:"**",
        component:Notfound
    }

];
