using Console_Project.Common.Enum;
using Console_Project.Common.Model;
using ConsoleTables;
using System.Security.Cryptography.X509Certificates;

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
                throw new FormatException("Count can't be lower than zero or equal zero!");

            if (productPrice < 0)
                throw new FormatException("Price is lower than 0!");

            if (string.IsNullOrWhiteSpace(category))
                throw new FormatException("Category is empty!");

            bool isSuccessful = Enum.TryParse(typeof(Categories), category, true, out object parsedCategories);

            if (!isSuccessful)
            {
                throw new InvalidDataException("Category cannot be found");
            }

            var existingProduct = product.FirstOrDefault(p => p.ProdcutName == productName && p.ProductPrice == productPrice);
            if (existingProduct == null)
            {
                var newProduct = new Product
                {
                    ProdcutName = productName,
                    ProductCount = productCount,
                    ProductPrice = productPrice,
                    Categories = (Categories)parsedCategories
                };
                product.Add(newProduct);
                return newProduct.Code;
                Console.WriteLine("Added new Row in table: ");
            }
            else
            {
                existingProduct.ProductCount += productCount;
                return existingProduct.Code;
                Console.WriteLine($"Added new product on product with ID: {existingProduct.Code}");

            }
        }

        public void UpdateProduct(int newCode, string newName, decimal newPrice, int newCount, string newCategory)
        {
            var selectedProduct = product.FirstOrDefault(x => x.Code == newCode);

            if (selectedProduct == null)
            {
                throw new Exception($"Product code {newCode} not found!");
            }

            bool isSuccessful = Enum.TryParse(typeof(Categories), newCategory, true, out object newParsedCategory);
            if (!isSuccessful)
            {
                throw new Exception("This category was not found.");
            }

            selectedProduct.ProductPrice = newPrice;
            selectedProduct.ProdcutName = newName;
            selectedProduct.ProductCount = newCount;
            selectedProduct.Categories = (Categories)newParsedCategory;
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
        public decimal SaleItemPrice { get; private set; }
        int salecode = 0;
        public void AddSale(int number)
        {
            int itemcode0 = 0;
            decimal b = 0;
            saleitem = new();
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
                var find = product.FirstOrDefault(x => x.Code == ID);

                if (find == null)
                {
                    throw new Exception("This ID cannot be found");
                }
                Console.WriteLine($"Enter Count of Product with {ID} ID");
                int count = int.Parse(Console.ReadLine().Trim());

                if (count <= 0)
                {
                    throw new Exception("Count cannot be lower than zero or equal zero");
                }
                if (count > find.ProductCount)
                {
                    throw new Exception("There is not enough product in stock");
                }
                var saleItem = new SaleItem
                {
                    Code = itemcode0,
                    SaleItemCount = count,
                    Product = find,
                    SaleItemPrice = count * find.ProductPrice

                };
                itemcode0++;
                b += saleItem.SaleItemPrice;
                saleitem.Add(saleItem);

                find.ProductCount -= count;
            }

            var newsale = new Sale
            {
                Code = salecode,
                PriceofSale = b,
                Date = DateTime.Now.AddHours(1).AddMinutes(1).AddSeconds(1),
                saleItems = saleitem

            };
            sale.Add(newsale);

            var itemTable = new ConsoleTable("Sale Item ID", "Sale Item Name", "Sale Item Count", "Sale Item Price");
            foreach (var item in saleitem)
            {
                itemTable.AddRow(item.Code, item.Product.ProdcutName, item.SaleItemCount, item.SaleItemPrice);
            }
            var saleTable = new ConsoleTable("Sale ID", "Total Sale Price", "History");
            foreach (var item1 in sale)
            {
                saleTable.AddRow(item1.Code, item1.PriceofSale, DateTime.Now.AddHours(1).AddMinutes(1).AddSeconds(1));
            }
            Console.WriteLine("Daily all list of saleitem:");
            Console.WriteLine("-----------------------------------------------");
            itemTable.Write();

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Sale Table:");
            Console.WriteLine("-----------------------------------------------");
            saleTable.Write();
            salecode++;
        }

        public void ShowAllSales()
        {
            var saleItemsTable = new ConsoleTable("Sale ID", "Sale Item ID", "Sale Item Name", "Sale Count", "Sale Item Price");
            var saleTable = new ConsoleTable("Sale ID", "Total Sale Price", "History");

            if (sale == null)
            {
                throw new Exception("There are no sales today yet");
            }

            foreach (var saleRecord in sale)
            {
                foreach (var saleItem in saleRecord.saleItems)
                {
                    saleItemsTable.AddRow(saleRecord.Code, saleItem.Code, saleItem.Product.ProdcutName, saleItem.SaleItemCount, saleItem.SaleItemPrice);
                }
            }

            foreach (var saleRecord in sale)
            {
                saleTable.AddRow(saleRecord.Code, saleRecord.PriceofSale, DateTime.Now.AddHours(1).AddMinutes(1).AddSeconds(1));
            }
           
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("All Sales table:");
            saleTable.Write();
           
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("All Sale Items in Sales:");
            saleItemsTable.Write();
        }

        public void DeleteSaleItemInSale(int saleID, int saleItemID, int saleItemCount)
        {
            if (saleID < 0)
            {
                throw new Exception("saleID cannot be lower than 0");
            }

            if (saleItemID < 0)
            {
                throw new Exception("saleItemID cannot be lower than 0");
            }

            if (saleItemCount <= 0)
            {
                throw new Exception("The number of returned products cannot be lower than zero or equal to 0");
            }

            var saleRecord = sale.FirstOrDefault(x => x.Code == saleID);

            if (saleRecord == null)
            {
                throw new Exception($"Sale with ID {saleID} cannot be found");
            }

            var saleItem = saleRecord.saleItems.FirstOrDefault(item => item.Code == saleItemID);

            if (saleItem == null)
            {
                throw new Exception($"Sale Item with ID {saleItemID} could not be found");
            }

            if (saleItem.SaleItemCount == saleItemCount)
            {
                saleItem.Product.ProductCount += saleItemCount;
                saleRecord.PriceofSale -= saleItemCount * saleItem.Product.ProductPrice;
                saleRecord.saleItems.Remove(saleItem);
            }
            else if (saleItem.SaleItemCount > saleItemCount)
            {
                saleItem.Product.ProductCount += saleItemCount;
                saleRecord.PriceofSale -= saleItemCount * saleItem.Product.ProductPrice;
                saleItem.SaleItemCount -= saleItemCount;
            }
            else
            {
                throw new Exception("The number of returned products exceeds the available quantity in the sale item");
            }

            ShowAllSales();
        }




        //var table = new ConsoleTable("Sale Item Name", "Sale Item Name", "Sale Item Count", "Sale Item Price", "Update History");
        //foreach (var item in saleitem)
        //{
        //    table.AddRow(item.Code, item.Product.ProdcutName, item.SaleItemCount, item.SaleItemPrice,
        //        DateTime.Now.AddHours(1).AddMinutes(1).AddSeconds(1));
        //}
        //table.Write();
    }
}




//public void DeleteSaleByID(int ID)
//{
//    var RemoveSale = sale.FirstOrDefault(x => x.Code == ID);

//    if (RemoveSale == null)
//        throw new Exception($"{ID} Sale not found! ");

//    sale = sale.Where(x => x.Code != ID).ToList();

//    var table = new ConsoleTable("Code", "Price", "Date",
//           "Name", "Count");

//    foreach (var row in sale)
//    {
//        table.AddRow(row.Code, row.PriceofSale, row.Date);
//    }
//    table.Write();
//}
//    public void ShowSalesbyTimeRange(DateTime firstdate, DateTime lastdate)
//    {
//        var b = from i in sale
//                where (i.Date > firstdate && i.Date < lastdate)
//                select i;
//        var table = new ConsoleTable("Code", "Price", "Date",
//               "Name", "Count");

//        foreach (var row in b)
//        {
//            table.AddRow(row.Code, row.PriceofSale, row.Date);
//        }
//        table.Write();
//    }
//    public void ShowSalesbyAmountRange(decimal firstamount, decimal lastamount)
//    {
//        var b = from i in sale
//                where (i.PriceofSale > firstamount && i.PriceofSale < lastamount)
//                select i;

//        var table = new ConsoleTable("Code", "Price", "Date",
//               "Name", "Count");

//        foreach (var row in b)
//        {
//            table.AddRow(row.Code, row.PriceofSale, row.Date);
//        }
//        table.Write();
//    }
//    public void ShowSaleGivenDate(DateTime date)
//    {
//        var b = from i in sale
//                where i.Date == date
//                select i;

//        var table = new ConsoleTable("Code", "Price", "Date",
//               "Name", "Count");

//        foreach (var row in b)
//        {
//            table.AddRow(row.Code, row.PriceofSale, row.Date);
//        }
//        table.Write();
//    }
//}
