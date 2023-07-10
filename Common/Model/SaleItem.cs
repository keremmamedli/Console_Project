using Console_Project.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Project.Common.Model
{
    public class SaleItem : BaseEntityForSaleItem
        
    {
        public static decimal PriceofSale = 0;
        public static int Count_ = 1;
        public SaleItem()
        {
            NumberOfSaleItem = Count_;
            Count_++;
            PriceofSale += Product.ProductPrice * SaleItemCount;
        }
        public int SaleItemCount { get; set; }
        public Product Product { get; set; }

    }
}
