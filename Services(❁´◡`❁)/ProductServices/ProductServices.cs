using Console_Project.Common.Enum;
using Console_Project.Common.Model;
using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Console_Project.Services.ProductService
{
    #region ProductOperations
    public class ProductOpeations
    {
        public List<Product> product;
        public List<Sale> sale;
        public List<SaleItem> saleitem;
        public ProductOpeations()
        {
            product = new();
            sale = new();
            saleitem = new();
        }
        public int AddProduct(string productName, int productCount, decimal productPrice, string category)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new FormatException("Error... Name is empty!");

            if (productCount <= 0)
                throw new FormatException("Count can`t be lower than zero or equal zero!");

            if (productPrice < 0)
                throw new FormatException("Price is lower than 0!");

            if (string.IsNullOrWhiteSpace(category))
                throw new FormatException("Category is empty!");

            bool isSuccessful
                = Enum.TryParse(typeof(Categories), category, true, out object parsedCategories);

            if (!isSuccessful)
            {
                throw new InvalidDataException("Category cannot be found");
            }
            var NewProduct = new Product
            {
                ProdcutName = productName,
                ProductCount = productCount,
                ProductPrice = productPrice,
                Categories = (Categories)parsedCategories
            };
            product.Add(NewProduct);
            return NewProduct.Code;
        }
        public void UpdateProduct(int newCode, string newName, decimal newPrice, int newCount, string newCategory)
        {
            var selectedProduct = product.FirstOrDefault(x => x.Code == newCode);

            if (selectedProduct == null)
                throw new Exception($"Product code {newCode} not found!");

            bool isSuccessful
                = Enum.TryParse(typeof(Categories), newCategory, true, out object newparsedCategories);
            if (!isSuccessful)
            {
                throw new Exception("This category not found");
            }
            selectedProduct.ProductPrice = newPrice;
            selectedProduct.ProdcutName = newName;
            selectedProduct.ProductCount = newCount;
            selectedProduct.Categories = (Categories)newparsedCategories;
        }
        public void RemoveProduct(int CodeOfProduct)
        {
            var RemoveProduct = product.FirstOrDefault(x => x.Code == CodeOfProduct);

            if (RemoveProduct == null)
                throw new Exception($"{CodeOfProduct} Product not found! ");

            product = product.Where(x => x.Code != CodeOfProduct).ToList();
        }
        public List<Product> ShowAllProducts()
        {
            return product;
        }
        public void ShowProductsofCategories(string category_1)
        {
            var c_list = new List<Product>();
            foreach (var i in Enum.GetValues(typeof(Categories)))
            {
                var e = product.Where(p => p.Categories.ToString().ToLower().Equals(category_1.ToLower())).ToList();
                c_list.AddRange(e);
            }
            if (c_list.Count == 0)
            {
                throw new Exception("Not Found");
            }
            var bar = c_list.GroupBy(x => x.ProdcutName).Select(x => x.First()).ToList();// remove dublicates in list

            var table = new ConsoleTable("Product Name", "Product Price", "Product Categories", "Product Count", "Product code");
            foreach (var en in bar)
            {
                table.AddRow(en.ProdcutName, en.ProductPrice, en.Categories, en.ProductCount, en.Code);
            }
            table.Write();
        }
        public void ShowProductsPriceRange(decimal startprice, decimal lastprice)
        {
            var list_c = new List<Product>();
            if (startprice < 0 || lastprice < 0)
            {
                throw new Exception("Price cannot lower than zero");
            }

            if (startprice > lastprice)
            {
                throw new Exception("Last price cannot be lower than start price");
            }
            var prod = product.Where(x => x.ProductPrice > startprice && x.ProductPrice < lastprice).ToList();
            list_c.AddRange(prod);
            var table = new ConsoleTable("Product Name", "Product Price",
                "Product Categories", "Product Count", "Product code");
            foreach (var pr in list_c)
            {
                table.AddRow(pr.ProdcutName, pr.ProductPrice, pr.Categories, pr.ProductCount, pr.Code);
            }
            table.Write();
        }
        public void SearchWithName(string name_)
        {
            var list_ = new List<Product>();
            if (name_ == null)
            {
                throw new Exception("String is Null");
            }
            var pre = from i in product
                      where i.ProdcutName == name_
                      select i;

            var table = new ConsoleTable("Product Name", "Product Price",
                "Product Categories", "Product Count", "Product code");
            foreach (var pr in pre)
            {
                table.AddRow(pr.ProdcutName, pr.ProductPrice, pr.Categories, pr.ProductCount, pr.ProductCount);
            }
            table.Write();
        }
        #endregion

        public void AddSale(int number)
        {
            var table = new ConsoleTable("Name", "Count", "Price of items" , "History");
            var list_ = new List<SaleItem>();
            var saletlist = new List<SaleItem>();
            decimal totalprice = 0;
            DateTime d = DateTime.Now;
            string dateString = d.ToString("yyyy/MM/dd HH:mm:ss");
            if (number <= 0)
            {
                throw new Exception("Sale Item cannot be lower than 0 or equal to 0");
            }
            for (int i = 0; i < number; i++)
            {
                Console.WriteLine("Enter ID");
                int ID = int.Parse(Console.ReadLine().Trim());
                if (ID < 0)
                {
                    throw new Exception("ID cannot be lower than 0");
                }
                var item = product.FirstOrDefault(p => p.Code == ID);
                if (item != null)
                {
                    Console.WriteLine($"Enter {ID} Sale Item count");
                    int count = int.Parse(Console.ReadLine().Trim());
                    if (count <= 0)
                    {
                        throw new Exception("Count cannot be Lower than 0 or equal 0");
                    }
                    if (count > item.ProductCount)
                    {
                        throw new Exception("There is not enough product in stock");
                    }
                    totalprice = (int)(totalprice + item.ProductPrice * count);
                    item.ProductCount = item.ProductCount - count;
                    table.AddRow(item.ProdcutName, count, item.ProductPrice * count ,dateString);
                }
                else
                {
                    throw new Exception("This Product was not found");
                }
            }
            table.Write();
            Console.WriteLine($"---------------------------------Total Price = {totalprice}");
            Console.WriteLine("Added this sale in Sale");
            
            {

            }
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
