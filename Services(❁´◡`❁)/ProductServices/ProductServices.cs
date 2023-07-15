using Console_Project.Common.Enum;
using Console_Project.Common.Model;
using ConsoleTables;
using System.Globalization;
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

            var existingProduct = product.FirstOrDefault(p => p.ProdcutName.ToLower() == productName.ToLower());
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
            int item_ = 0;
            decimal b = 0;
            saleitem = new();
            if (number <= 0)
            {
                throw new Exception("Sale Item cannot be lower than 0 or equal to 0");
            }
            for (int i = 0; i < number; i++)
            {
                Console.WriteLine("Enter Product ID for Sale: ");
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
                    Code = item_,
                    SaleItemCount = count,
                    Product = find,
                    SaleItemPrice = count * find.ProductPrice

                };
                item_++;
                b += saleItem.SaleItemPrice;
                saleitem.Add(saleItem);

                find.ProductCount -= count;
            }

            var newsale = new Sale
            {
                Code = salecode,
                PriceofSale = b,
                Date = DateTime.Now.AddHours(0).AddMinutes(0).AddSeconds(0),
                saleItems = saleitem

            };
            sale.Add(newsale);

            var itemTable = new ConsoleTable("Sale Item ID", "Sale Item Name", "Sale Item Count", "Sale Item Price");
            foreach (var item in saleitem)
            {
                itemTable.AddRow(item.Code, item.Product.ProdcutName, item.SaleItemCount, item.SaleItemPrice);
            }
            var saleTable = new ConsoleTable("Sale ID", "Total Sale Price", "Date");
            foreach (var item1 in sale)
            {
                saleTable.AddRow(item1.Code, item1.PriceofSale, item1.Date);
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
                saleTable.AddRow(saleRecord.Code, saleRecord.PriceofSale, saleRecord.Date);
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
        public void DeleteSalebyID(int SaleID)
        {
            if (SaleID < 0)
            {
                throw new Exception("Sale ID cannot be lower than 0 or equal 0");
            }
            var RemoveSale = from i in sale
                             where i.Code == SaleID
                             select i;
            if (RemoveSale == null)
            {
                throw new Exception($"Sale with {SaleID} could not be found");
            }
            foreach (var item in RemoveSale)
            {
                foreach (var item1 in item.saleItems)
                {
                    item1.Product.ProductCount += item1.SaleItemCount;
                }
            }
            sale = sale.Where(x => x.Code != SaleID).ToList();
            ShowAllSales();
        }
        public void ShowSaleByDateRange(DateTime startDate, DateTime endDate)
        {
            int SaleItemCountinSale = 0;

            if (endDate <= startDate)
            {
                throw new Exception("Start Date must be lower than End Date");
            }
            //if (endDate > DateTime.Now.AddHours(1).AddMinutes(1).AddSeconds(1))
            //{
            //    throw new Exception("End Date must be before the Present Time");
            //}
            var b = sale.Where(x => x.Date >= startDate && x.Date <= endDate).ToList();
            if (b == null)
            {
                throw new Exception($"There is not sale in range of {startDate} and {endDate}");
            }
            var table = new ConsoleTable("Sale ID", "Price of Sale", "Count of Sale Item in Sale", "Date");
            foreach (var item2 in b)
            {
                SaleItemCountinSale = 0;
                foreach (var item3 in item2.saleItems)
                {
                    SaleItemCountinSale++;
                }
                table.AddRow(item2.Code, item2.PriceofSale, SaleItemCountinSale, item2.Date);
            }

            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine($"Between {startDate} and {endDate} sales: ");
            table.Write();


        }
        public void ShowSalebyDate(DateTime date)
        {
            if (date > DateTime.Now)
            {
                throw new Exception("Date must be before the current time.");
            }

            var saleList = sale.Where(x =>
            x.Date.Year == date.Year &&
            x.Date.Month == date.Month &&
            x.Date.Day == date.Day &&
            x.Date.Hour == date.Hour &&
            x.Date.Minute == date.Minute &&
            x.Date.Second == date.Second
            ).ToList();

            if (saleList == null)
            {
                throw new Exception($"There are no sales on this date");
            }

            var table = new ConsoleTable("Sale ID", "Price of Sale", "Sale Items Count in Sale", "Date");
            foreach (var item in saleList)
            {
                int count = item.saleItems.Count;
                table.AddRow(item.Code, item.PriceofSale, count, item.Date);
            }

            table.Write();
        }

        public void ShowSaleByPriceRange(decimal startPrice, decimal endPrice)
        {
            if (startPrice < 0)
            {
                throw new Exception("Start Price cannot be lower than 0");
            }

            if (endPrice <= 0)
            {
                throw new Exception("End Price cannot be lower than or equal to 0");
            }

            if (startPrice >= endPrice)
            {
                throw new Exception("End Price cannot be lower than or equal to Start Price");
            }

            var table = new ConsoleTable("Sale ID", "Price of Sale", "Count of Sale Items in Sale", "Date");
            int countOfSaleItems = 0;

            var c = from d in sale
                    where d.PriceofSale > startPrice && d.PriceofSale <= endPrice
                    select d;
            if (c == null)
            {
                throw new Exception("Sale cannot be found");
            }

            foreach (var sale in c)
            {
                countOfSaleItems = 0;
                foreach (var item in sale.saleItems)
                {
                    countOfSaleItems++;
                }
                table.AddRow(sale.Code, sale.PriceofSale, countOfSaleItems, sale.Date);
            }

            table.Write();
        }
        public void ShowSalebyCode(int Code)
        {
            var table = new ConsoleTable("Sale ID", "Sale Price", "Count of Sale Item", "Date");
            var table1 = new ConsoleTable("Sale Item ID", "Sale Item Name", "Sale Items Count");
            if (Code < 0)
            {
                throw new Exception("ID cannot be lower than 0");
            }
            var b = from d in sale
                    where d.Code == Code
                    select d;

            if (b == null)
            {
                throw new Exception($"Sale with {Code} ID couldn`t found");
            }
            int count = 0;
            foreach (var item in b)
            {

                foreach (var item1 in item.saleItems)
                {
                    table1.AddRow(item1.Code, item1.Product.ProdcutName, item1.SaleItemCount);
                    count++;
                }
                table.AddRow(item.Code, item.PriceofSale, count, item.Date);
            }
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Sale table : ");
            table.Write();

            Console.WriteLine("------------------------------------");
            Console.WriteLine("Sale Item Table: ");
            table1.Write();
            Console.WriteLine("Completed...");
        }
    }
}





