
export interface Order{
    orderId:string,
    address:string,
    postalCode:string,
    date:string,
    orderItems:OrderProductItem[]
}     
export interface OrderProductItem{
    id:number,
    productId:number
    quantity:number,
    price:number,
    productName:string,
    thumbnailImage:string
}   
export interface AllOrder{
    orders:Order[]
}