import { Component, inject, signal } from '@angular/core';
import { AuthServices } from '../../core/authServices';
import { UserProfile } from '../../Model/auth';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-profile',
  imports: [RouterLink],
  templateUrl: './profile.html',
  styleUrl: './profile.css',
})
export class Profile {

  ngOnInit(){
    this.getProfile();
  }



  profileData = signal<UserProfile | null>(null);
  authServices = inject(AuthServices)
  getProfile(){
    this.authServices.getUserProfile().subscribe({
      next:(ss)=>{
        this.profileData.set(  ss.data  )
      }
    })
    }

    sigout(){
      this.authServices.logout();
    }


}
