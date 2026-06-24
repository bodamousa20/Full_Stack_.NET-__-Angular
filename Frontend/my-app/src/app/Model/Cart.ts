export interface CartProduct {
  id: number;
  name: string;
  description: string;
  price: number;
  categoryId: number;
  thunmailImage: string;
}

export interface CartItem {
  id: number;
  productId: number;
  quanity: number;
  price: number;
  product: CartProduct;
}

export interface Cart {
  id: number;
  userID: string;
  total: number;
  createdAt: string;
  cartItemDtos: CartItem[];
}

export interface CartResponse {
  message: string;
  data: Cart;
}