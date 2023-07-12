using Console_Project.Services.MenuService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Project.SubMenu
{
     class SubMenuforProducts
     {
        public static void SubMenuForProduct() // Method that Menu of Product operations
        {
            Console.Clear();
            int option;

            do
            {
                Console.WriteLine("1. Add Products");
                Console.WriteLine("2. Update Products");
                Console.WriteLine("3. Remove Products");
                Console.WriteLine("4. Show all Products");
                Console.WriteLine("5. Show products by Category");
                Console.WriteLine("6. Show products by price range");
                Console.WriteLine("7. Search products by name");
                Console.WriteLine("0. Go Back...");
                Console.WriteLine("-----------");

                Console.WriteLine("Enter option :");

                while (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Invalid number! ");
                    Console.WriteLine("-----------");
                    Console.WriteLine("Enter option:");
                }

                switch (option)
                {
                    case 1:
                        MenuService.MenuAddProducts();
                        break;
                    case 2:
                        MenuService.MenuUpdateProduct();
                        break;
                    case 3:
                        MenuService.MenuRemoveProduct();
                        break;
                    case 4:
                        MenuService.MenuShowAllProduct();
                        break;
                    case 5:
                        MenuService.MenuShowAllProductbyCategories(); 
                        break;
                    case 6:
                        MenuService.MenuShowProductPriceRange();
                        break;
                    case 7:
                        MenuService.MenuSearchProductWithName();
                        break;
                    case 0:
                        //For go back
                        break;
                    default:
                        Console.WriteLine("No such option!");
                        break;
                }
            } while (option != 0);
        }
     }
}
