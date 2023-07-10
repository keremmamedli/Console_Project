using Console_Project.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console_Project.Common.Enum;

namespace Console_Project.Common.Model
{
    public class Product : BaseEntityForProduct
    {
        private static int Code1 = 0;
        public Product()
        {
            Code = Code1;
            Code1++;
        }
        public string ProdcutName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductCount { get; set; }
        public Categories categories { get; set; }
    }
}
