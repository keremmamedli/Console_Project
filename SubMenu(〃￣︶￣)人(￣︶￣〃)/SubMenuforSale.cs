using Console_Project.Services.MenuService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Project.SubMenu
{
    public class SubMenuforSale
    {
        public static void SubMenuForSale() // Method that Menu of Sale operations
        {
            int option;
            int[] fq = { 261, 277, 293, 311, 329, 349, 370, 392, 415, 440, 466, 493 };
            int duration = 300;
            do
            {
                Console.WriteLine("1. Add new Sale");
                Console.WriteLine("2. Returning any Sale item on sale");
                Console.WriteLine("3. Deletion of sales");
                Console.WriteLine("4. Show all Sales");
                Console.WriteLine("5. Show of sales according to the given date range");
                Console.WriteLine("6. Display of sales according to the given amount range");
                Console.WriteLine("7. Showing sales on a given date");
                Console.WriteLine("8. The given number is basically a display of the sales data of that number");
                Console.WriteLine("9. For Clear Console");
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
                        MenuService.MenuAddSale();// Add new Sale
                        break;
                    case 2:
                        Console.Beep(fq[9], duration);
                        MenuService.MenuRemoveSaleItemInSale();// Update Product                     
                        break;
                    case 3:
                        Console.Beep(fq[11], duration);
                        MenuService.MenuDeleteSaleByID();// Deletion of sales                       
                        break;
                    case 4:
                        Console.Beep(fq[0], duration);
                        MenuService.MenuShowAllSales();// Show all Sales
                        break;
                    case 5:
                        Console.Beep(fq[7], duration);
                        MenuService.MenuShowsalebydateRange();//  Show of sales according to the given date range                        
                        break;
                    case 6:
                        Console.Beep(fq[2], duration);
                        MenuService.MenuShowSaleByPriceRange();// Display of sales according to the given amount range                       
                        break;
                    case 7:
                        Console.Beep(fq[4], duration);
                        MenuService.MenuShowSaleByDate();// Showing sales on a given date                       
                        break;
                    case 8:
                        Console.Beep(fq[5], duration);
                        MenuService.MenuShowSaleByID();// The given number is basically a display of the sales data of that number                       
                        break;
                    case 9:
                        Console.Beep(fq[11], duration);
                        Console.Clear();//Clear Terminal                       
                        break;
                    case 0:
                        Console.Beep(fq[0], duration);// Go Back...
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
