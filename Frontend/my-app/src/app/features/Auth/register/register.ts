import { Component, inject, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

import { RegisterFormData } from '../../../Model/auth';
import { AuthServices } from '../../../core/authServices';
import { single } from 'rxjs';
import { Router } from '@angular/router';
import { validate } from '@angular/forms/signals';
@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule,],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  constructor(private authServies: AuthServices) {}
  
  private router = inject(Router)
  form = new FormGroup({
    username : new FormControl("",[Validators.maxLength(30),Validators.minLength(5),Validators.required]),
    age: new FormControl(0,[Validators.required,Validators.minLength(18)]),
    phone: new FormControl("",[Validators.required,Validators.minLength(11),Validators.maxLength(11),Validators.pattern(/^\d{11}$/),Validators.pattern(/^01[0125]\d{8}$/)]),
    email : new FormControl("",[Validators.email,Validators.required]),
    password : new FormControl("",[Validators.required,Validators.minLength(6)]),
    confirm : new FormControl("",[Validators.required])
  })
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
get checkusername() {
  return this.form.controls.username.dirty 
  && 
  this.form.controls.username.invalid
   &&  
  this.form.controls.username.touched 
}
get checkPhone() {
  return this.form.controls.phone.dirty 
  && 
  this.form.controls.phone.invalid
   &&  
  this.form.controls.phone.touched
  }
get checkAge() {
  return this.form.controls.age.dirty 
  && 
  this.form.controls.age.invalid
   &&  
  this.form.controls.age.touched
}
get checkMatched(){
  return this.form.controls.confirm.value != this.form.controls.password.value && this.form.controls.password.touched
}


Data = this.form.controls
message = signal("");
  errors = signal<string[]>([]);
isLoading = signal(false)
RegisterSubmited()
{
  //
  if(this.form.valid)
  {
    this.errors.set([]);
  this.isLoading.set(true);
   const body : RegisterFormData = {
  username: this.Data.username.value ?? '',
  email:    this.Data.email.value ?? '',
  password: this.Data.password.value ?? '',
  age:   Number(this.Data.age.value) ,    
  phone:    this.Data.phone.value ?? '', 
   };

   this.authServies.RegsiterUser(body).subscribe({
    next:(message)=>{
      this.message.set("Account Created Successfully")
      this.isLoading.set(false);

      setInterval(()=>{
        this.router.navigate(['login'])
      },2000)
    },
    error:(err)=>{
      const errorsList: string[] = err.error?.errorsList;
      if(errorsList && errorsList.length > 0){
        this.errors.set(errorsList);
        this.message.set("Error Occured");
      }
            this.isLoading.set(false);

    }
   })
 
  }
}

}
