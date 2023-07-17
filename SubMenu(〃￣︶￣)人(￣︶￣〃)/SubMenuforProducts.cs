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
            int option;
            int[] fq = {261,277,293,311,329,349,370,392,415,440,466,493 };
            int duration = 300;
            do
            {
                Console.WriteLine("1. Add Products");
                Console.WriteLine("2. Update Products");
                Console.WriteLine("3. Remove Products");
                Console.WriteLine("4. Show all Products");
                Console.WriteLine("5. Show products by Category");
                Console.WriteLine("6. Show products by price range");
                Console.WriteLine("7. Search products by name");
                Console.WriteLine("8. For Clear Console");
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
                        Console.Beep(fq[7], duration);
                        MenuService.MenuAddProducts();// Add Product with Name,Price,Category and Count
                        break;
                    case 2:
                        Console.Beep(fq[9], duration);
                        MenuService.MenuUpdateProduct();// Update Added Product , change name,price category or Count ...
                        break;
                    case 3:
                        Console.Beep(fq[11], duration);
                        MenuService.MenuRemoveProduct();// Delete Added Product,for this enter Product ID
                        break;
                    case 4:
                        Console.Beep(fq[0], duration);
                        MenuService.MenuShowAllProduct();// Show all Added products
                        break;
                    case 5:
                        Console.Beep(fq[7], duration);
                        MenuService.MenuShowAllProductbyCategories(); // Add Category and Method show you all products which all of them in this category
                        break;
                    case 6:
                        Console.Beep(fq[2], duration);
                        MenuService.MenuShowProductPriceRange();// Show the product in two price ranges
                        break;
                    case 7:
                        Console.Beep(fq[4], duration);
                        MenuService.MenuSearchProductWithName();// Search Product with Name and Show it`s Category,Name,Count,Price...
                        break;
                    case 8:
                        Console.Beep(fq[5], duration);
                        Console.Clear();//Clear Terminal
                        break;
                    case 0:
                        Console.Beep(fq[0], duration);//For go back
                        break;
                    default:
                        Console.Beep(fq[0], duration);
                        Console.WriteLine("No such option!");
                        break;
                }
            } while (option != 0);
        }
     }
}
