using Console_Project.Services.ProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Project.Services.MenuService
{
    public class MenuService
    {
        #region Product
        public static ProductOpeations productService = new();
        public static void MenuAddProducts()
        {
            try
            {
                Console.WriteLine("Enter Product name:");
                string productName = Console.ReadLine();

                Console.WriteLine("Enter Product count:");
                int productCount= int.Parse(Console.ReadLine());

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
    }
}