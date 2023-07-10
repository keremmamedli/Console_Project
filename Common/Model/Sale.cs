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
            NumberOfSale = _count;
            _count++;
        }
        public DateTime Date { get; set; }
        public decimal PriceofSale { get; set; }
        public List<SaleItem> saleItems { get; set; }
          
    }
}
