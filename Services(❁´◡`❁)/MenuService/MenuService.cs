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
                var tableCategory = new ConsoleTable("Categories");
                tableCategory.AddRow("Bakery");
                tableCategory.AddRow("Dairy");
                tableCategory.AddRow("SeaFood");
                tableCategory.AddRow("Meat");
                tableCategory.AddRow("PersonalCare");
                tableCategory.AddRow("Fruits");
                tableCategory.AddRow("Vegetables");

                Console.WriteLine("Enter Product name:");
                string productName = Console.ReadLine().Trim();


                string namePattern = @"^[A-Za-z]+$";


                if (!Regex.IsMatch(productName, namePattern))
                {
                    throw new FormatException("Product name should only contain alphabetic characters!");
                }

                Console.WriteLine("Enter Product count:");
                int productCount = int.Parse(Console.ReadLine().Trim());

                tableCategory.Write();
                Console.WriteLine("Enter Product's category from this Table:");
                string category = Console.ReadLine().Trim();

                if (!Regex.IsMatch(category, namePattern))
                {
                    throw new FormatException("Category name should only contain alphabetic characters!");
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
        }
        public static void MenuUpdateProduct()
        {
            try
            {
                Console.WriteLine("Enter product Code: ");
                int code_ = int.Parse(Console.ReadLine().Trim());

                Console.WriteLine("Enter new Name: ");
                string NewProductName = Console.ReadLine().Trim();
                string namePattern = @"^[A-Za-z]+$";


                if (!Regex.IsMatch(NewProductName, namePattern))
                {
                    throw new FormatException("Product name should only contain alphabetic characters!");
                }
                Console.WriteLine("Enter new price of product: ");
                decimal NewProductPrice = decimal.Parse(Console.ReadLine().Trim());

                Console.WriteLine("Enter new product`s count");
                int NewProductCount = int.Parse(Console.ReadLine().Trim());

                Console.WriteLine("Enter new product category");
                string NewProductCategory = Console.ReadLine().Trim();


                if (!Regex.IsMatch(NewProductCategory, namePattern))
                {
                    throw new FormatException("Product name should only contain alphabetic characters!");
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
        }
        public static void MenuRemoveProduct()
        {
            try
            {
                Console.WriteLine("Enter the code of the product to be deleted");
                int productcode = int.Parse(Console.ReadLine().Trim());

                productService.RemoveProduct(productcode);
                Console.WriteLine($"{productcode} Product deleted :) ");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error,Be careful broo...");
                Console.WriteLine(ex.Message);
            }
            MenuShowAllProduct();
        }
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
                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error! , Be Careful");
                Console.WriteLine(ex.Message);
            }
        }
        public static void MenuShowAllProductbyCategories()
        {
            try
            {
                Console.WriteLine("Enter Category: ");
                string category = Console.ReadLine().Trim();

                productService.ShowProductsofCategories(category);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! Error: ");
                Console.WriteLine(ex.Message);
            }
        }
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
        }
        public static void MenuSearchProductWithName()
        {
            Console.WriteLine("Enrer product Name");
            string Example = Console.ReadLine().Trim();

            productService.SearchWithName(Example);
        }
        #endregion
        #region MenuproductServices
        public static void MenuAddSale()
        {
            try
            {
                Console.WriteLine("Enter number of Sale items in Sale: ");
                int number = int.Parse(Console.ReadLine());
                Console.WriteLine("-------------------------------------");

                productService.AddSale(number);
                Console.WriteLine("New Sale Items added");


            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! Error, Be Careful!");
                Console.WriteLine(ex.Message);
            }
        }
        public static void MenuShowAllSales()
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
        }
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
        }
        public static void MenuShowsalebydateRange()
        {
            try
            {
                Console.WriteLine("Enter First Date: Input type (MM/dd/yyyy HH:mm:ss) Be Careful here please! Write all of 0s");
                DateTime startDate = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy HH:mm:ss tt", CultureInfo.InvariantCulture);
                Console.WriteLine("------------------------------------------");

                Console.WriteLine("Enter Last Date: Input type (MM/dd/yyyy HH:mm:ss ) Be Careful here please! Write all of 0s ");
                DateTime endDate = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy HH:mm:ss tt", CultureInfo.InvariantCulture);

                productService.ShowSaleByDateRange(startDate, endDate);
            }
            catch (Exception ex)
            {
                {
                    Console.WriteLine("OWW,Error founded :( , Be more careful to Date`s");
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public static void MenuShowSaleByPriceRange()
        {
            try
            {
                Console.WriteLine("Enter Start price: ");
                decimal startPrice = decimal.Parse(Console.ReadLine().Trim());

                Console.WriteLine("----------------------------------------");

                Console.WriteLine("Enter Last price: ");
                decimal lastprice = decimal.Parse(Console.ReadLine().Trim());


                Console.WriteLine($"All sales between {startPrice}$ and {lastprice}$ :");
                productService.ShowProductsPriceRange(startPrice, lastprice);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error found :(");
                Console.WriteLine(ex.Message);
            }
        }
        public static void MenuShowSaleByID()
        {
            try
            {
                Console.WriteLine("Enter Sale ID");
                int ID = int.Parse(Console.ReadLine().Trim());

                productService.DeleteSalebyID(ID);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error founded fuuh..:(");
                Console.WriteLine(ex.Message);
            }
        }
        public static void MenuShowSaleByDate()
        {
            Console.WriteLine("Enter Date: Input type (MM/dd/yyyy HH:mm:ss) Be careful here please! Write all of 0s and AM or PM");

            DateTime date = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy HH:mm:ss tt", CultureInfo.InvariantCulture);

            productService.ShowSalebyDate(date);
        }
        #endregion
    }
}

