import { Component, signal, ViewChild, ElementRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { productServices } from '../../core/productServices';
import { Image, productDetails } from '../../Model/Product';
import { FormsModule } from '@angular/forms';
import { CartServices } from '../../core/CartServices';

@Component({
  selector: 'app-product-details',
  imports: [FormsModule],
  templateUrl: './product-details.html',
  styleUrl: './product-details.css',
})
export class ProductDetails {

  product = signal<productDetails | null>(null);
  activeIdx = 0;
  qty = 1;
  image!:Image
  @ViewChild('thumbsTrack') thumbsTrack!: ElementRef<HTMLDivElement>;

  get activePhoto(): string {
    return this.product()?.photos?.[this.activeIdx]?.imageUrl ?? '';
  }

  constructor(private productService: productServices,private router:Router , private route: ActivatedRoute , private cartServices :CartServices) {}

  ngOnInit() {
    
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.productService.GetProductById(id).subscribe({
      next: (res) => this.product.set(res.data),
      error: (err) => console.log(err),
    });
  }
 // Notice the '?' after image. This makes it optional so activePhotoSet() works in HTML!
activePhotoSet(image?: any): string {
  
  // 1. If 'image' is provided (from the thumbnail loop), use it. 
  //    Otherwise, grab the current active photo from the product array (for the main image).
  const selectedImage = image || this.product()?.photos?.[this.activeIdx];

  // 2. Fallback if the product hasn't loaded yet or has no photos
  if (!selectedImage || !selectedImage.imageUrl) {
    return ''; // Or put a default placeholder path like 'assets/no-image.png'
  }

  const url = selectedImage.imageUrl;

  // 3. Your logic: Check if it's an external URL
  if (url.includes("http://") || url.includes("https://")) {
    return url;
  } else {
    // 4. Local URL: Strip the leading slash just in case to prevent double slashes (//)
    const cleanUrl = url.startsWith('/') ? url.substring(1) : url;
    return `https://localhost:7245/${cleanUrl}`;
  }
}

  setPhoto(idx: number) {
    this.activeIdx = idx;
    const thumb = this.thumbsTrack?.nativeElement?.children[idx] as HTMLElement;
    thumb?.scrollIntoView({ behavior: 'smooth', block: 'nearest', inline: 'center' });
  }

  scrollPhoto(dir: number) {
    const max = (this.product()?.photos?.length ?? 1) - 1;
    this.setPhoto(Math.max(0, Math.min(max, this.activeIdx + dir)));
  }

  changeQty(delta: number) { this.qty = Math.max(1, this.qty + delta); }
  addToCart(productId:number,quanity:number) { 
    this.cartServices.addProductToCart(productId,quanity).subscribe({
      next:()=>{
        this.cartServices.cartCounter.update(number =>number +1 );
        this.router.navigate(["/user/cart"])
      }
    })
   }
   addToFavourite(prdId:number) {
    this.productService.addedProductToFavourite(prdId).subscribe({
      next:()=>{
        this.productService.favCount.update(number=>number+1)
        this.router.navigate(["/user/favourite"])
      }
    })
  }
}