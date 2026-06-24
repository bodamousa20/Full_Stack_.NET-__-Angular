using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Entites
{
    public class BaseEnitity<T>
    {
        public T id { set; get; }
    }
}
