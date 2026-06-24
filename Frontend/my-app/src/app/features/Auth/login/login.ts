import { Component, inject, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthServices } from '../../../core/authServices';
import { LoginFromData, LoginResponse } from '../../../Model/auth';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { JsonPipe } from '@angular/common';
import { audit } from 'rxjs';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {

  form = new FormGroup({
    email: new FormControl("",[Validators.required,Validators.email]),
    password: new FormControl("",[Validators.required,Validators.minLength(6)])
  })
  private services = inject(AuthServices)
  private cookie = inject (CookieService)
  private router = inject (Router)
  isLoading = signal(false);
  message = signal("");

  loginSubmit()
{
  // call api for checking 
  console.log(this.form);
  if(this.form.valid){
    this.isLoading.set(true);
    console.log("form is valid");
    //call API for login
    const body = new LoginFromData();
    body.email = this.form.controls.email.value ?? ''
    body.password = this.form.controls.password.value ?? ''

    this.services.LoginUser(body).subscribe({
      next:(res)=>{
        console.log(res);
        this.message.set(res.message)
        this.cookie.set("token",res.token,2,'/') // save token at cookie with name token and valid for 2 days 

        localStorage.setItem("userInfo",JSON.stringify(res.user)) // save the user logged in localstoarge
        
        this.isLoading.set(false)
        this.services.isLoggedIn.set(true)
        this.router.navigate(['user/home']);
      },
      error:()=>{
        this.message.set("Invalid Email Or Password") 
        this.isLoading.set(false)
      }
    })
  }
  else{
    this.message.set("Please Enter an Email and Password")
  }
 
}

get CheckEmail() {
  return this.form.controls.email.dirty 
  && 
  this.form.controls.email.invalid
   &&  
  this.form.controls.email.touched 
}
get CheckPassword() {
  return this.form.controls.password.dirty 
  && 
  this.form.controls.password.invalid
   &&  
  this.form.controls.password.touched
}
}
