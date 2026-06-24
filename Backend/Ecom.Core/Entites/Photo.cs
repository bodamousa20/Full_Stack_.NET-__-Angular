using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecom.Core.Entites
{
    public class Photo:BaseEnitity<int>
    {
        public string Name { set; get; }

        public int ProductId { set; get; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
    }
}
