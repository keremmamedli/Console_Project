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
            Console.Clear();
            int option;

            do
            {
                Console.WriteLine("1. Add new Sale");
                Console.WriteLine("2. Returning any product on sale");
                Console.WriteLine("3. Deletion of sales");
                Console.WriteLine("4. Show all Sales");
                Console.WriteLine("5. Show of sales according to the given date range");
                Console.WriteLine("6. Display of sales according to the given amount range");
                Console.WriteLine("7. Showing sales on a given date");
                Console.WriteLine("8. The given number is basically a display of the sales data of that number");
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
                        // Add new Sale
                        break;
                    case 2:
                        // Update Product
                        break;
                    case 3:
                        // Deletion of sales
                        break;
                    case 4:
                        // Show all Sales
                        break;
                    case 5:
                        //  Show of sales according to the given date range
                        break;
                    case 6:
                        // Display of sales according to the given amount range
                        break;
                    case 7:
                        // Showing sales on a given date
                        break;
                    case 8:
                        // The given number is basically a display of the sales data of that number
                    case 0:
                        // Go Back...
                        break;


                    default:
                        Console.WriteLine("No such option!");
                        break;
                }

            } while (option != 0);
        }
    }
}
