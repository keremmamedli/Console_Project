using Console_Project.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Project.Common.Model
{
    public class SaleItem : BaseEntity
        
    {
        public static int Count_ = 1;
        public Product product;
        private int Code1 { get; set; }
        public SaleItem()
        {
            Code = Code1;
            Code1++; // This part define Code of Every SaleItem 
        }
        public decimal SaleItemPrice { get; set; }
        public int SaleItemCount { get; set; }
        public Product Product { get; set; }
    }
}
