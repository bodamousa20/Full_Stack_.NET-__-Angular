using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Entites
{
    public class Category:BaseEnitity<int>
    {
        
        public string name { set; get; }
        public string description { set; get; }

        public ICollection<Product> Products = new List<Product>();

    }
}
