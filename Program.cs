using Console_Project.SubMenu;

namespace Console_Project
{
    internal class Program 
    {
        static void Main(string[] args)
        {
            int option;

            do
            {
                Console.WriteLine("1. For Operation on Products");
                Console.WriteLine("2. For Operation on Sale");
                
                Console.WriteLine("0. Exit");
                Console.WriteLine("-----------");

                Console.WriteLine("Enter option:");

                while (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Invalid number!");
                    Console.WriteLine("-----------");
                    Console.WriteLine("Enter option:.");
                }

                switch (option)
                {
                    case 1:
                        SubMenuforProducts.SubMenuForProduct();
                        break;
                    case 2:
                        SubMenuforSale.SubMenuForSale();
                        break;
                    case 0:
                        Console.WriteLine("Program was ended... Bye :)");
                        break;
                    
                    default:
                        Console.WriteLine("No such option!");
                        break;
                }

            } while (option != 0);
        }
    }
}