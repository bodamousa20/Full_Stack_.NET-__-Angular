export const uri = "https://localhost:7245";
export class RegisterFormData {
  username: string = '';
  age: number = 0;
  phone: string = '';
  email: string = '';
  password: string = '';
}
export class LoginFromData
{
  email: string = '' 
  password: string=''
}
export interface LoginResponse {
  message:string,
  token:string,
  user:UserResponse
}

export interface UserProfile{
  id:string,
  age:number,
  username:string,
  email:string,
  phone:string
}
export interface UserResponse{
  id:string,
  age:number,
  username:string,
  email:string
  phone:string
}

