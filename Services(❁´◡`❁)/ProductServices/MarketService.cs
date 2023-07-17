using Console_Project.Abstract;
using Console_Project.Common.Enum;
using Console_Project.Common.Model;
using ConsoleTables;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Media;
namespace Console_Project.Services.ProductService
{
    #region ProductOperations
    public class ProductOpeations : IMarketable
    {
        public List<Product> product; // Created a list from Product
        public List<Sale> sale; // Created a list from Sale
        public List<SaleItem> saleitem; // Created a list from SaleItem
        public ProductOpeations()
        {
            product = new();
            sale = new();
            saleitem = new();
        }
        public int AddProduct(string productName, int productCount, decimal productPrice, string category)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new FormatException("Error... Name is empty!"); // if string is empty: ERROR

            if (productCount <= 0)
                throw new FormatException("Count can't be lower than zero or equal zero!"); // if Product Count lower than 0 or equal 0: ERROR
            if (productPrice < 0)
                throw new FormatException("Price is lower than 0!"); // if Product Price lower than zero: ERROR (Price may be equal 0 (in Dreams))

            if (string.IsNullOrWhiteSpace(category))
                throw new FormatException("Category is empty!"); //Check ,is category input Empty?

            bool isSuccessful = Enum.TryParse(typeof(Categories), category, true, out object parsedCategories); // it search input string in Category and answer True or False

            if (!isSuccessful)
            {
                throw new InvalidDataException("Category cannot be found");
            }
            var existingProduct = product.FirstOrDefault(p => p.ProdcutName.ToLower() == productName.ToLower()); // it Search ProductName in Products
            if (existingProduct == null)
            {
                var newProduct = new Product //Created new Product
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
            var selectedProduct = product.FirstOrDefault(x => x.Code == newCode); //Search input ID in Product IDs

            if (selectedProduct == null)
            {
                throw new Exception($"Product code {newCode} not found!");
            }

            bool isSuccessful = Enum.TryParse(typeof(Categories), newCategory, true, out object newParsedCategory); // Check, is there input string in Categories?
            if (!isSuccessful)
            {
                throw new Exception("This category was not found.");
            }
            // Updated product here
            selectedProduct.ProductPrice = newPrice;
            selectedProduct.ProdcutName = newName;
            selectedProduct.ProductCount = newCount;
            selectedProduct.Categories = (Categories)newParsedCategory;
        }
        public void RemoveProduct(int CodeOfProduct)
        {
            var RemoveProduct = product.FirstOrDefault(x => x.Code == CodeOfProduct); // it find Input ID in Product IDs

            if (RemoveProduct == null)
                throw new Exception($"{CodeOfProduct} Product not found! ");

            product = product.Where(x => x.Code != CodeOfProduct).ToList();// if input ID is not equal Product IDs add list :)
        }
        public List<Product> ShowAllProducts()
        {
            return product; // it show all Products and return List
        }
        public void ShowProductsofCategories(string category_1)
        {
            string namePattern = @"^[A-Za-z]+$"; // Add Regex only for alphabetic symbols
            if (!Regex.IsMatch(category_1, namePattern))
            {
                throw new FormatException("Category Name should only contain alphabetic characters!");
            }

            var c_list = new List<Product>();
            foreach (var i in Enum.GetValues(typeof(Categories)))
            {
                var e = product.Where(p => p.Categories.ToString().ToLower().Equals(category_1.ToLower())).ToList();
                c_list.AddRange(e);
            }
            if (c_list.Count == 0)
            {
                throw new Exception($"There is not product in this category :(");
            }
            var bar = c_list.GroupBy(x => x.ProdcutName).Select(x => x.First()).ToList();// remove dublicates in list

            var table = new ConsoleTable("Product Name", "Product Price", "Product Categories", "Product Count", "Product code");
            foreach (var en in bar)
            {
                table.AddRow(en.ProdcutName, en.ProductPrice, en.Categories, en.ProductCount, en.Code);
            }
            table.Write(Format.Alternative);
        }// it show all products by categories
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
            var prod = product.Where(x => x.ProductPrice > startprice && x.ProductPrice < lastprice).ToList(); // This Line find products in Price range and add List
            list_c.AddRange(prod);
            var table = new ConsoleTable("Product Name", "Product Price",
                "Product Categories", "Product Count", "Product code");
            foreach (var pr in list_c)
            {
                table.AddRow(pr.ProdcutName, pr.ProductPrice, pr.Categories, pr.ProductCount, pr.Code);
            }
            table.Write(Format.Alternative);
        }
        public void SearchWithName(string name_)
        {
            string namePattern = @"^[A-Za-z]+$"; // Add Regex only for alphabetic symbols
            if (!Regex.IsMatch(name_, namePattern))
            {
                throw new FormatException("Product name should only contain alphabetic characters!");
            }
            var list_ = new List<Product>();
            if (name_.Count() == 0)
            {
                throw new Exception("String is Null");
            }
            //var pre = from i in product // LINQ find Where ProductName equal input name it look like product.Where(); method
            //          where i.ProdcutName == name_
            //          select i;
            var pre = product.Where(x => x.ProdcutName.ToLower().Trim() == name_.ToLower().Trim()).ToList();

            if (pre.Count() == 0)
            {
                throw new NullReferenceException($"Product with {name_} was not found");
            }
            var table = new ConsoleTable("Product Name", "Product Price",
                "Product Categories", "Product Count", "Product code");
            foreach (var pr in pre)
            {
                table.AddRow(pr.ProdcutName, pr.ProductPrice, pr.Categories, pr.ProductCount, pr.ProductCount);
            }
            table.Write(Format.Alternative);
        }
        #endregion
        public decimal SaleItemPrice { get; private set; } // It`s ALl Sale items Price and İt Help us for calculate SalePrice
        int salecode = 0; // each time the method is called, ID is incremented by 1 and equals code
        public void AddSale(int number)
        {
            int item_ = 0; //It used for Sale Item`s ID and each time the method is called, it equal 0 then Increases by 1 for each SaleItem and Equal it Saleİtem İD
            decimal b = 0; //It`s Price of Sale each time the method called,it equal 0 and increases by "SaleItemPrice" for each SaleItem and equal PriceofSale 
            saleitem = new(); // Create new SaleItem list
            if (number <= 0)
            {
                throw new Exception("Sale Item cannot be lower than 0 or equal to 0");
            }
            for (int i = 0; i < number; i++) // Created loop for How many Sale item in Sale
            {
                Console.WriteLine("Enter Product ID for Sale: ");
                int ID = int.Parse(Console.ReadLine().Trim());
                if (ID < 0)
                {
                    throw new Exception("ID cannot be lower than 0");
                }
                var find = product.FirstOrDefault(x => x.Code == ID); // Find Product with ID

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
                var saleItem = new SaleItem // Create new Sale Item
                {
                    Code = item_,
                    SaleItemCount = count,
                    Product = find,
                    SaleItemPrice = count * find.ProductPrice

                };
                item_++; // Sale Item ID +1
                b += saleItem.SaleItemPrice; // SaleItem Price added to Price OF Sale
                saleitem.Add(saleItem); // Add new Sale Item to sale Item list 

                find.ProductCount -= count; // The product comes out of the warehouse
            }

            var newsale = new Sale // Create new Sale
            {
                Code = salecode,
                PriceofSale = b,
                Date = DateTime.Now.AddHours(0).AddMinutes(0).AddSeconds(0), // for show Hours,Minutes and Seconds
                saleItems = saleitem

            };
            sale.Add(newsale); // Add New Sale to Sale List

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
            itemTable.Write(Format.Alternative);

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Sale Table:");
            Console.WriteLine("-----------------------------------------------");
            saleTable.Write(Format.Alternative);
            salecode++; // Sale ID + 1
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
            saleTable.Write(Format.Alternative);

            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("All Sale Items in Sales:");
            saleItemsTable.Write(Format.Alternative);
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

            var saleRecord = sale.FirstOrDefault(x => x.Code == saleID); // Find Sale Which it`s ID equal input Sale ID

            if (saleRecord == null)
            {
                throw new Exception($"Sale with ID {saleID} cannot be found");
            }
            if (saleRecord.Date.AddDays(14) <= DateTime.Now)
            {
                throw new Exception("It is not possible to return the goods after 14 days from the sale");
            }
            var saleItem = saleRecord.saleItems.FirstOrDefault(item => item.Code == saleItemID); // Find Sale Item with Input Sale Item ID in Sale

            if (saleItem == null)
            {
                throw new Exception($"Sale Item with ID {saleItemID} could not be found");
            }

            if (saleItem.SaleItemCount == saleItemCount)
            {
                saleItem.Product.ProductCount += saleItemCount; // Increases the number of products in the warehouse
                saleRecord.PriceofSale -= saleItemCount * saleItem.Product.ProductPrice; // Reduces Price of Sale
                saleRecord.saleItems.Remove(saleItem); // Delete it SaleItem in Sale
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
            var RemoveSale = sale.Where(x => x.Code == SaleID).ToList(); // Find sale which SaleID equal input ID
            if (RemoveSale.Count == 0)
            {
                throw new Exception($"Sale with {SaleID} could not be found");
            }
            foreach (var item in RemoveSale)
            {
                foreach (var item1 in item.saleItems)
                {
                    item1.Product.ProductCount += item1.SaleItemCount;  // Increases the number of products in the warehouse
                }
            }
            sale = sale.Where(x => x.Code != SaleID).ToList(); //Find Sales which their IDs not equal to input ID
            ShowAllSales(); //use ShowAllSales(); method that it show us Sales Table
        }
        public void ShowSaleByDateRange(DateTime startDate, DateTime endDate)
        {
            int SaleItemCountinSale = 0; // it is equal to 0 every time the method is called and in loop increases 1 by Sale Items and used for calculate count of SaleItems in Sale

            if (endDate <= startDate)
            {
                throw new Exception("Start Date must be lower than End Date");
            }
            var b = sale.Where(x => x.Date >= startDate && x.Date <= endDate).ToList(); // It search Sale which it date in range of input Dates and add list
            if (b.Count == 0)
            {
                throw new Exception($"There is no sale in the range of {startDate} and {endDate}");
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
            table.Write(Format.Alternative);
        }
        public void ShowSaleByPriceRange(decimal startPrice, decimal endPrice)
        {
            int SaleItemCountinSale = 0;  // it is equal to 0 every time the method is called and in loop increases 1 by Sale Items and used for calculate count of SaleItems in Sale
            if (endPrice <= 0)
            {
                throw new Exception("End Price cannot be lower than 0 or equal to 0");
            }
            if (startPrice < 0)
            {
                throw new Exception("Start Price cannot be lower than 0");
            }

            if (endPrice <= startPrice)
            {
                throw new Exception("Start price must be lower than End price");
            }
            var b = sale.Where(x => x.PriceofSale >= startPrice && x.PriceofSale <= endPrice).ToList(); // It search Sale which it date in range of input Dates and Add List
            if (b.Count == 0)
            {
                throw new Exception($"There is no sale in the range of {startPrice}$ and {endPrice}$");
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
            Console.WriteLine($"All Sales {startPrice}$ and {endPrice}$ price range: ");
            table.Write(Format.Alternative);


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
            ).ToList(); // it search Sale which it Date equal input date and add it to List

            if (saleList.Count == 0)
            {
                throw new Exception($"There are no sales on this date");
            }
            var table = new ConsoleTable("Sale ID", "Price of Sale", "Sale Items Count in Sale", "Date");
            foreach (var item in saleList)
            {
                int count = item.saleItems.Count;
                table.AddRow(item.Code, item.PriceofSale, count, item.Date);
            }
            table.Write(Format.Alternative);
        }
        public void ShowSalebyCode(int ID)
        {
            int SaleItemCountinSale = 0; // it is equal to 0 every time the method is called and in loop increases 1 by Sale Items and used for calculate count of SaleItems in Sale
            if (ID < 0)
            {
                throw new Exception("Sale ID cannot be lower than 0 ");
            }
            var b = sale.Where(x => x.Code == ID).ToList(); // it find Sale which it ID equal to input ID and add it to List
            if (b.Count() == 0)
            {
                throw new Exception($"Sale with {ID} ID not found! :(");
            }
            var saletable = new ConsoleTable("Sale ID", "Price of Sale", "Count of Sale Item in Sale", "Date");
            var listitemTable = new ConsoleTable("Sale Item ID in Sale", "Sale Item Name", "Product Count in this Sale Item");
            foreach (var item2 in b)
            {
                SaleItemCountinSale = 0;
                foreach (var item3 in item2.saleItems)
                {
                    listitemTable.AddRow(item3.Code, item3.Product.ProdcutName, item3.SaleItemCount);
                    SaleItemCountinSale++;
                }
                saletable.AddRow(item2.Code, item2.PriceofSale, SaleItemCountinSale, item2.Date);
            }
            Console.WriteLine("------------------------------------------");

            Console.WriteLine($"Sale with {ID} ID: ");
            saletable.Write(Format.Alternative);

            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Sale items table in Sale with {ID} ID");
            listitemTable.Write(Format.Alternative);

        }
    }
}