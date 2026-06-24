
export interface Product {
  id: number;
  name: string;
  description: string;
  price: number;
  categoryId: number;
  thunmailImage: string; // or any[] if backend sends complex objects
}
export interface productDetails {
  id: number;
  name: string;
  description: string;
  price: number;
  categoryId: number;
  photos:Image[]
}

export interface Image {
  id:number,
  imageUrl:string
}
export interface productFavourite{
  id:number,
  userId:string,
  products:Product[]
}
export interface ApiResponse<T> 
{
  data: T;
  message: string;
  pagenumber: number,
  pagesize: number,
  total: number
}
