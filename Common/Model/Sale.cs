using Console_Project.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console_Project.Common.Model;

namespace Console_Project.Common.Model
{
    public class Sale : BaseEntityForSale
    {
        public static int _count = 1;
        
        public Sale()
        {
            IDOfSale = _count;
            _count++; // Define ID of Sale
        }
        public DateTime Date { get; set; }
        public decimal PriceofSale { get; set; } // Price of Sale found in SaleItem.cs and it {get; set; } here
        public List<SaleItem> saleItems { get; set; }
    }
}
