using Console_Project.Services.ProductService;
using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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
            Console.Clear();
            try
            {
                Console.WriteLine("Enter Product name:");
                string productName = Console.ReadLine();

                Console.WriteLine("Enter Product count:");
                int productCount = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter Product`s category:");
                string category = Console.ReadLine();

                Console.WriteLine("Enter Product price:");
                decimal productPrice = decimal.Parse(Console.ReadLine());

                int productCode = productService.AddProduct(productName, productCount, productPrice, category);

                Console.WriteLine($"Added Product code is {productCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! Got an error!");
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
        public static void MenuUpdateProduct()
        {
            Console.Clear();
            try
            {
                Console.WriteLine("Enter product Code: ");
                int code_ = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter new Name: ");
                string NewProductName = Console.ReadLine();

                Console.WriteLine("Enter new price of product: ");
                decimal NewProductPrice = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Enter new product`s count");
                int NewProductCount = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter new product category");
                string NewProductCategory = Console.ReadLine();


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
            Console.Clear();
            try
            {
                Console.WriteLine("Enter the code of the product to be deleted");
                int productcode = int.Parse(Console.ReadLine());

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
            Console.Clear();
            try
            {
                var products_ = productService.ShowAllProducts();
                var table = new ConsoleTable("Product Code", "Product name", "Product count",
                    "Product price", "Department");

                if (products_.Count == 0)
                {
                    Console.WriteLine("No doctor's yet.");
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
    }
}