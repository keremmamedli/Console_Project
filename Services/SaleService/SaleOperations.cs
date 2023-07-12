using Console_Project.Common.Enum;
using Console_Project.Common.Model;
using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Console_Project.Services.SaleService
{
    public class SaleOperations
    {
        public List<Product> product;
        public List<Sale> sale;
        public List<SaleItem> saleitem;

        public SaleOperations()
        {
            product = new List<Product>();
            sale = new List<Sale>();
            saleitem = new List<SaleItem>();
        }

        public int AddSale(int ID, int number)
        {
            var find = product.FirstOrDefault(x => x.Code == ID);
            if (find == null)
            {
                throw new Exception("This Product cannot be found");
            }
            var NewSaleItem = new SaleItem
            {
                Product = (Product)find,
                SaleItemCount = number
            };
            saleitem.Add(NewSaleItem);
            var newSale = new Sale
            {
                PriceofSale = number * NewSaleItem.Product.ProductPrice,
                Date = DateTime.Now.AddMinutes(1),
                Code = ID
            };
            sale.Add(newSale);
            return newSale.Code;
        }
        public List<Sale> ShowAllSales()
        {
            return sale;
        }
        public void DeleteSaleByID(int ID)
        {
            var RemoveSale = sale.FirstOrDefault(x => x.Code == ID);

            if (RemoveSale == null)
                throw new Exception($"{ID} Sale not found! ");

            sale = sale.Where(x => x.Code != ID).ToList();

            var table = new ConsoleTable("Code", "Price", "Date",
                   "Name", "Count");

            foreach (var row in sale)
            {
                table.AddRow(row.Code, row.PriceofSale, row.Date);
            }
            table.Write();
        }
        public void ShowSalesbyTimeRange(DateTime firstdate, DateTime lastdate)
        {
            var b = from i in sale
                    where (i.Date > firstdate && i.Date < lastdate)
                    select i;
            var table = new ConsoleTable("Code", "Price", "Date",
                   "Name", "Count");

            foreach (var row in b)
            {
                table.AddRow(row.Code, row.PriceofSale, row.Date);
            }
            table.Write();
        }
        public void ShowSalesbyAmountRange(decimal firstamount, decimal lastamount)
        {
            var b = from i in sale
                    where (i.PriceofSale > firstamount && i.PriceofSale < lastamount)
                    select i;
           
            var table = new ConsoleTable("Code", "Price", "Date",
                   "Name", "Count");

            foreach (var row in b)
            {
                table.AddRow(row.Code, row.PriceofSale, row.Date);
            }
            table.Write();
        }
        public void ShowSaleGivenDate(DateTime date)
        {
            var b = from i in sale
                    where i.Date == date
                    select i;
            
            var table = new ConsoleTable("Code", "Price", "Date",
                   "Name", "Count");

            foreach (var row in b)
            {
                table.AddRow(row.Code, row.PriceofSale, row.Date);
            }
            table.Write();
        }
    }
}
