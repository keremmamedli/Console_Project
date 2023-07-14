using Console_Project.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console_Project.Common.Model;

namespace Console_Project.Common.Model
{
    public class Sale : BaseEntity
    {
        private int Code1 { get; set; }
        public Sale()
        {
            Code = Code1;
            Code1++; // This part define Code of Every products 
        }
        public DateTime Date { get; set; }
        public decimal PriceofSale { get; set; } // Price of Sale found in SaleItem.cs and it {get; set; } here
        public SaleItem saleItems { get; set; }
    }
}
