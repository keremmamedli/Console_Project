using Console_Project.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Project.Common.Model
{
    public class SaleItem : BaseEntity // Added all saleitem count,code,product and everything here
        
    {
        public Product product;
        private int Code { get; set; } 
        public decimal SaleItemPrice { get; set; }
        public int SaleItemCount { get; set; }
        public Product Product { get; set; }
    }
}
