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
        public SaleItem()
        {
            Code = Count_;
            Count_++; // Define ID of Sale Item
        }
        public int SaleItemCount { get; set; }
        public Product Product { get; set; }
        
    }
}
