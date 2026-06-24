using System;
using System.Collections.Generic;
using Ecom.Core.Entites; // Ensure this matches your namespace

namespace Ecom.Infrastructure.Data
{
    public static class EcomDataSeeder
    {
        public static List<Product> GenerateProducts()
        {
            var random = new Random();
            var products = new List<Product>();

            int productIdCounter = 1;
            int photoIdCounter = 1;

            // Dictionary mapping the existing Category IDs (1-5) to realistic product names and constraints
            var categoryData = new Dictionary<int, (string[] Names, string ImageKeyword, int MinPrice, int MaxPrice)>
            {
                { 1, (new[] {
                    "iPhone 14 Pro", "Samsung Galaxy S23", "MacBook Pro 16", "Dell XPS 13", "Sony WH-1000XM5",
                    "AirPods Pro", "iPad Air", "Kindle Paperwhite", "Nintendo Switch", "PlayStation 5",
                    "Xbox Series X", "LG C2 OLED TV", "Apple Watch Series 8", "Garmin Fenix 7", "GoPro Hero 11",
                    "DJI Mini 3 Pro", "Bose QuietComfort 45", "Sennheiser Momentum 4", "Asus ROG Zephyrus", "Razer Blade 15",
                    "Logitech MX Master 3S", "Keychron K2", "Samsung Odyssey G7", "Sonos Roam", "UE Megaboom 3",
                    "Anker PowerCore 10000", "SanDisk Extreme SSD", "Seagate Backup Plus", "Google Pixel 7", "OnePlus 11"
                }, "electronics,gadget", 50, 2500) },

                { 2, (new[] {
                    "Men's Classic T-Shirt", "Levi's 501 Original Jeans", "Nike Air Force 1", "Adidas Ultraboost", "North Face Puffer Jacket",
                    "Ray-Ban Aviator Sunglasses", "Casio Vintage Watch", "Women's Floral Summer Dress", "Zara Leather Jacket", "H&M Knit Sweater",
                    "Calvin Klein Underwear", "Tommy Hilfiger Polo", "Gucci GG Belt", "Converse Chuck Taylor", "Vans Old Skool",
                    "Puma Suede Classic", "Ralph Lauren Oxford Shirt", "Lululemon Align Leggings", "Patagonia Fleece Pullover", "Columbia Rain Jacket",
                    "Champion Reverse Hoodie", "Dr. Martens 1460 Boots", "Timberland 6-Inch Boots", "Balenciaga Triple S", "Off-White Sneaker",
                    "Supreme Box Logo Tee", "Prada Nylon Backpack", "Hermes Silk Scarf", "Chanel Classic Bag", "LV Neverfull"
                }, "fashion,clothing", 20, 800) },

                { 3, (new[] {
                    "Mid-Century Modern Sofa", "Solid Wood Dining Table", "Ergonomic Office Chair", "Memory Foam Mattress", "Persian Area Rug",
                    "Industrial Floor Lamp", "Ceramic Table Vase", "Velvet Throw Pillows", "Bamboo Cutting Board", "Cast Iron Skillet",
                    "Stainless Cookware Set", "Egyptian Cotton Sheets", "Blackout Curtains", "Geometric Wall Art", "Floating Wall Shelves",
                    "Faux Potted Monstera", "Woven Rattan Basket", "Tufted Ottoman", "Glass Coffee Table", "Minimalist Bed Frame",
                    "Orthopedic Dog Bed", "Smart WiFi Thermostat", "Robot Vacuum Cleaner", "HEPA Air Purifier", "Ceramic Mug Set",
                    "Marble Coasters", "Teak Wood Patio Set", "Hammock with Stand", "Tabletop Fire Pit", "Essential Oil Diffuser"
                }, "furniture,interior", 30, 1500) },

                { 4, (new[] {
                    "Spalding NBA Basketball", "Nike Premier Football", "Wilson Tennis Racket", "Callaway Golf Clubs", "Manduka PRO Yoga Mat",
                    "Bowflex Dumbbells", "Schwinn Cycling Bike", "Everlast Punching Bag", "Speedo Fastskin Goggles", "Burton Snowboard",
                    "Salomon Trail Shoes", "CamelBak Hydration Pack", "Fitbit Charge 5", "Garmin Edge 530", "Titleist Golf Balls",
                    "Rawlings Baseball Glove", "Under Armour Duffel", "Cork Yoga Block", "Resistance Band Set", "Jump Rope with Counter",
                    "High-Density Foam Roller", "TRX Suspension Trainer", "Table Tennis Table", "Yonex Badminton Racket", "Penny Skateboard",
                    "Rollerblade Inline Skates", "Mares Scuba Fins", "Coleman 4-Person Tent", "Mummy Sleeping Bag", "Trek Mountain Bike"
                }, "sports,fitness", 15, 1200) },

                { 5, (new[] {
                    "Mac Matte Lipstick", "Estee Lauder Foundation", "Nars Creamy Concealer", "Fenty Gloss Bomb", "Urban Decay Palette",
                    "Maybelline Mascara", "Anastasia Brow Wiz", "Charlotte Tilbury Cream", "The Ordinary Niacinamide", "Cerave Cleanser",
                    "La Roche-Posay Sunscreen", "Paula's Choice BHA", "Laneige Lip Mask", "Olaplex Hair Perfector", "Moroccanoil Treatment",
                    "Dyson Supersonic Dryer", "Revlon Volumizer", "GHD Platinum+ Styler", "Chanel Mademoiselle", "Dior Sauvage",
                    "Tom Ford Oud Wood", "Jo Malone Sea Salt", "Kiehl's Facial Cream", "SK-II Pitera Essence", "Tatcha Dewy Cream",
                    "Glossier Boy Brow", "Clinique Moisture Surge", "Mario Badescu Spray", "Neutrogena Makeup Wipes", "Real Techniques Brushes"
                }, "beauty,cosmetics", 10, 300) }
            };

            // Loop through the dictionary and build the Product and Photo objects
            foreach (var category in categoryData)
            {
                int existingCategoryId = category.Key;
                var data = category.Value;

                foreach (var productName in data.Names)
                {
                    // Generate a realistic price
                    decimal randomPrice = Math.Round((decimal)(random.NextDouble() * (data.MaxPrice - data.MinPrice) + data.MinPrice), 2);

                    var product = new Product
                    {
                        // Remove Id = productIdCounter if your DB auto-generates Identity IDs
                        Name = productName,
                        Description = $"High-quality {productName.ToLower()} perfect for your everyday needs.",
                        Price = randomPrice,
                        CategoryId = existingCategoryId, // Links to your existing DB categories
                        Photos = new List<Photo>()
                    };

                    var photo = new Photo
                    {
                        // Remove Id = photoIdCounter if your DB auto-generates Identity IDs
                        fileName = $"{productName.Replace(" ", "_").ToLower()}_{productIdCounter}.jpg",
                        fileExtention = ".jpg",
                        fileSize = random.Next(150000, 850000),
                        imageUrl = $"https://loremflickr.com/640/480/{data.ImageKeyword}?lock={productIdCounter}"
                    };

                    product.Photos.Add(photo);
                    products.Add(product);

                    productIdCounter++;
                    photoIdCounter++;
                }
            }

            return products;
        }
    }
}