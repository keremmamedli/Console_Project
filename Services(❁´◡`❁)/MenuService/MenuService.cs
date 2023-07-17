using Console_Project.Common.Enum;
using Console_Project.Common.Model;
using Console_Project.Services.ProductService;
using ConsoleTables;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Console_Project.Services.MenuService
{
    public class MenuService
    {
        #region MenuProductServices
        public static ProductOpeations productService = new();
        public static void MenuAddProducts() 
        {
            try
            {
                var tableCategory = new ConsoleTable("Categories"); // Show all Categories on table
                tableCategory.AddRow("0.Bakery");
                tableCategory.AddRow("1.Dairy");
                tableCategory.AddRow("2.SeaFood");
                tableCategory.AddRow("3.Meat");
                tableCategory.AddRow("4.PersonalCare");
                tableCategory.AddRow("5.Fruits");
                tableCategory.AddRow("6.Vegetables");
                tableCategory.AddRow("7.Drinks");

                Console.WriteLine("Enter Product name:");
                string productName = Console.ReadLine().Trim(); // Trim(); Delete spaces before and after input

                string namePattern = @"^[A-Za-z]+$"; // Add Regex only for alphabetic symbols
                if (!Regex.IsMatch(productName, namePattern))
                {
                    throw new FormatException("Product name should only contain alphabetic characters!");
                }
                //Product name may not be numbers; 
                Console.WriteLine("Enter Product count:");
                int productCount = int.Parse(Console.ReadLine().Trim());

                tableCategory.Write(Format.Alternative);
                Console.WriteLine("Enter Product's category with name or numbers from this Table:");
               
                string category = Console.ReadLine().Trim(); // Create Regex for Alphabetic symbols or integres from 0 to 7 because there are categories in Market
                string pattern = @"^(?:[A-Za-z]|[0-7])+$";

                if (!Regex.IsMatch(category, pattern))
                {
                    throw new FormatException("This Category was not found");
                }
                if (int.TryParse(category, out int categoryNumber) && categoryNumber > 7) // if input > 7 ERROR
                {
                    throw new Exception("This Category was not be found");
                }

                Console.WriteLine("Enter Product price:");
                decimal productPrice = decimal.Parse(Console.ReadLine().Trim());

                int productCode = productService.AddProduct(productName, productCount, productPrice, category);

                Console.WriteLine($"Added Product code is {productCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! Got an error!");
                Console.WriteLine(ex.Message);
            }
        }// Add Product with Name,Price,Category and Count
        public static void MenuUpdateProduct() 
        {
            try
            {
                var tableCategory = new ConsoleTable("Categories"); // Show all Categories on table
                tableCategory.AddRow("0.Bakery");
                tableCategory.AddRow("1.Dairy");
                tableCategory.AddRow("2.SeaFood");
                tableCategory.AddRow("3.Meat");
                tableCategory.AddRow("4.PersonalCare");
                tableCategory.AddRow("5.Fruits");
                tableCategory.AddRow("6.Vegetables");
                tableCategory.AddRow("7.Drinks");

                Console.WriteLine("Enter product Code: ");
                int code_ = int.Parse(Console.ReadLine().Trim()); 

                Console.WriteLine("Enter new Name: ");
                string NewProductName = Console.ReadLine().Trim();
                string namePattern = @"^[A-Za-z]+$";

                if (!Regex.IsMatch(NewProductName, namePattern)) // Check  is NewProductName suitable to Regex ?
                {
                    throw new FormatException("Product name should only contain alphabetic characters!");
                }
                Console.WriteLine("Enter new price of product: ");
                decimal NewProductPrice = decimal.Parse(Console.ReadLine().Trim());

                Console.WriteLine("Enter new product`s count");
                int NewProductCount = int.Parse(Console.ReadLine().Trim());

                tableCategory.Write(Format.Alternative);
                Console.WriteLine("Enter Product's category with name or numbers from this Table:");

                string NewProductCategory = Console.ReadLine().Trim(); // Create Regex for Alphabetic symbols or integres from 0 to 7 because there are categories in Market
                string pattern = @"^(?:[A-Za-z]|[0-7])+$";

                if (!Regex.IsMatch(NewProductCategory, pattern))
                {
                    throw new FormatException("This Category was not found");
                }
                if (int.TryParse(NewProductCategory, out int categoryNumber) && categoryNumber > 7) // if input > 7 ERROR
                {
                    throw new Exception("This Category was not be found");
                }

                productService.UpdateProduct(code_, NewProductName, NewProductCount, NewProductCount, NewProductCategory);

                Console.WriteLine("------------------------------------------");
                Console.WriteLine($"Updated {code_} product`s information");
                MenuShowAllProduct();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error,Be more careful");
                Console.WriteLine(ex.Message);
            }
        } // Update Added Product , change name,price category or Count ...
        public static void MenuRemoveProduct()
        {
            try
            {
                Console.WriteLine("Enter the code of the product to be deleted");
                int productcode = int.Parse(Console.ReadLine().Trim()); // Trim(); Delete spaces before and after input

                productService.RemoveProduct(productcode);
                Console.WriteLine($"{productcode} Product deleted :) ");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error,Be careful broo...");
                Console.WriteLine(ex.Message);
            }
            MenuShowAllProduct();
        } // Delete Added Product,for this enter Product ID
        public static void MenuShowAllProduct()
        {
            try
            {
                var products_ = productService.ShowAllProducts();
                var table = new ConsoleTable("Product Code", "Product name", "Product count",
                    "Product price", "Categoory");

                if (products_.Count == 0)
                {
                    Console.WriteLine("No Product yet.");
                    return;
                }
                foreach (var productt in products_)
                {
                    table.AddRow(productt.Code, productt.ProdcutName, productt.ProductCount, productt.ProductPrice, productt.Categories);
                }
                table.Write(Format.Alternative);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error! , Be Careful");
                Console.WriteLine(ex.Message);
            }
        } // Show all Added products
        public static void MenuShowAllProductbyCategories()
        {
            try
            {
                var tableCategory = new ConsoleTable("Categories"); // Show All Categories on table
                tableCategory.AddRow("Bakery");
                tableCategory.AddRow("Dairy");
                tableCategory.AddRow("SeaFood");
                tableCategory.AddRow("Meat");
                tableCategory.AddRow("PersonalCare");
                tableCategory.AddRow("Fruits");
                tableCategory.AddRow("Vegetables");
                tableCategory.AddRow("Drinks");

                tableCategory.Write(Format.Alternative); // Write table different format
                Console.WriteLine("Enter Product's category with name or numbers from this Table:");
                string category = Console.ReadLine().Trim(); // Create Regex for Alphabetic symbols or integres from 0 to 7 because there are categories in Marke


                productService.ShowProductsofCategories(category);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! Error: ");
                Console.WriteLine(ex.Message);
            }
        } // Add Category and Method show you all products which all of them in this category
        public static void MenuShowProductPriceRange()
        {
            try
            {
                Console.WriteLine("Write start Price");
                decimal firstprice_ = decimal.Parse(Console.ReadLine().Trim());

                Console.WriteLine("Enter Last Price:");
                decimal lastprice_ = decimal.Parse(Console.ReadLine().Trim());

                productService.ShowProductsPriceRange(firstprice_, lastprice_);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Got an ERROR");
                Console.WriteLine(ex.Message);
            }
        }   // Show the product in two price ranges
        public static void MenuSearchProductWithName()
        {
            try
            {
                Console.WriteLine("Enrer product Name");
                string Example = Console.ReadLine().Trim();

                productService.SearchWithName(Example);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oww, Error!!!");
                Console.WriteLine(ex.Message);
            }
        }// Search Product with Name and Show it`s Category,Name,Count,Price...
        #endregion
        #region MenuproductServices
        public static void MenuAddSale()
        {
            try
            {
                Console.WriteLine("Enter number of Sale items in Sale: ");
                int number = int.Parse(Console.ReadLine().Trim());
                Console.WriteLine("-------------------------------------");

                productService.AddSale(number); // using ProductService`s methods
                Console.WriteLine("New Sale Items added");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! Error, Be Careful!");
                Console.WriteLine(ex.Message);
            }
        } //Add Sale using products and sale items
        public static void MenuShowAllSales() // Show all added sales
        {
            try
            {
                productService.ShowAllSales();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception founded");
                Console.WriteLine(ex.Message);
            }
        }
        public static void MenuRemoveSaleItemInSale()
        {
            try
            {
                Console.WriteLine("Enter Sale ID: ");
                int SaleID = int.Parse(Console.ReadLine().Trim());

                Console.WriteLine("-----------------------------------------");

                Console.WriteLine("Enter Sale Item ID for delete");
                int SaleItemID = int.Parse(Console.ReadLine().Trim());

                Console.WriteLine("-----------------------------------------");

                Console.WriteLine("Enter number of returned sale item...");
                int SaleItemCount = int.Parse(Console.ReadLine().Trim());

                productService.DeleteSaleItemInSale(SaleID, SaleItemID, SaleItemCount);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops,Got an Error");
                Console.WriteLine(ex.Message);
            }
        }// Remove Added sale item in Sale by Sale ID and Sale item ID
        public static void MenuDeleteSaleByID()
        {
            try
            {
                Console.WriteLine("Enter the Sale ID to be deleted: ");
                int SaleID = int.Parse(Console.ReadLine().Trim());

                productService.DeleteSalebyID(SaleID);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oww,There is error in here broo :( ");
                Console.WriteLine(ex.Message);
            }
        }//Remove Added Sale by ID,The number of products in the warehouse increases
        public static void MenuShowsalebydateRange()
        {
            try
            {
                Console.WriteLine("Enter First Date: Input type (MM/dd/yyyy HH:mm:ss ) Be Careful here please! Write all of 0s and Spaces");
                DateTime startDate = DateTime.ParseExact(Console.ReadLine().Trim(), "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture); // It`s DateTime`s input typr month,day,year ....
                Console.WriteLine("------------------------------------------");

                Console.WriteLine("Enter Last Date: Input type (MM/dd/yyyy HH:mm:ss ) Be Careful here please! Write all of 0s and Spaces ");
                DateTime endDate = DateTime.ParseExact(Console.ReadLine().Trim(), "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                productService.ShowSaleByDateRange(startDate, endDate);
            }
            catch (Exception ex)
            {
                {
                    Console.WriteLine("OWW,Error founded :( , Be more careful to Date`s");
                    Console.WriteLine(ex.Message);
                }
            }
        }// Show the Sales in two Date ranges
        public static void MenuShowSaleByPriceRange()
        {
            try
            {
                Console.WriteLine("Enter Start Price: ");
                decimal startPrice = decimal.Parse(Console.ReadLine().Trim());
                Console.WriteLine("------------------------------------------");

                Console.WriteLine("Enter End Price: ");
                decimal endPrice = decimal.Parse(Console.ReadLine().Trim());
                Console.WriteLine("------------------------------------------");

                productService.ShowSaleByPriceRange(startPrice, endPrice);
            }
            catch (Exception ex)
            {
                {
                    Console.WriteLine("OWW,Error founded :( , Be more careful to Date`s");
                    Console.WriteLine(ex.Message);
                }
            }
        } // Show the Sales in two price ranges
        public static void MenuShowSaleByDate()
        {
            try
            {
                Console.WriteLine("Enter Date: Input type (MM/dd/yyyy HH:mm:ss) Be careful here please! Write all of 0s and spaces");
                DateTime date = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture); // It`s DateTime`s input typr month,day,year ....
                productService.ShowSalebyDate(date);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid date format. Please enter a date in the format: MM/dd/yyyy HH:mm:ss tt");
                Console.WriteLine(ex.Message);
            }
        } // Input Date and This Method Show all sales which they date equal input date
        public static void MenuShowSaleByID()
        {
            try
            {
                Console.WriteLine("Enter Sale ID");
                int ID = int.Parse(Console.ReadLine().Trim());

                productService.ShowSalebyCode(ID);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error founded fuuh..:(");
                Console.WriteLine(ex.Message);
            }
        }//Input ID and Method show Sale which it ID equal Input ID
        #endregion
    }
}

