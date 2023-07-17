using Console_Project.SubMenu;

namespace Console_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int option;
            string d = @"

     ██╗    ██╗███████╗██╗      ██████╗ ██████╗ ███╗   ███╗███████╗
     ██║    ██║██╔════╝██║     ██╔════╝██╔═══██╗████╗ ████║██╔════╝
     ██║ █╗ ██║█████╗  ██║     ██║     ██║   ██║██╔████╔██║█████╗  
     ██║███╗██║██╔══╝  ██║     ██║     ██║   ██║██║╚██╔╝██║██╔══╝  
     ╚███╔███╔╝███████╗███████╗╚██████╗╚██████╔╝██║ ╚═╝ ██║███████╗
      ╚══╝╚══╝ ╚══════╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝     ╚═╝╚══════╝
                                                                   
 
";
            string s = @"

      ██████╗ ██████╗ ███╗   ██╗███████╗ ██████╗ ██╗     ███████╗     █████╗ ██████╗ ██████╗ ██╗     ██╗ ██████╗ █████╗ ████████╗██╗ ██████╗ ███╗   ██╗
     ██╔════╝██╔═══██╗████╗  ██║██╔════╝██╔═══██╗██║     ██╔════╝    ██╔══██╗██╔══██╗██╔══██╗██║     ██║██╔════╝██╔══██╗╚══██╔══╝██║██╔═══██╗████╗  ██║
     ██║     ██║   ██║██╔██╗ ██║███████╗██║   ██║██║     █████╗      ███████║██████╔╝██████╔╝██║     ██║██║     ███████║   ██║   ██║██║   ██║██╔██╗ ██║
     ██║     ██║   ██║██║╚██╗██║╚════██║██║   ██║██║     ██╔══╝      ██╔══██║██╔═══╝ ██╔═══╝ ██║     ██║██║     ██╔══██║   ██║   ██║██║   ██║██║╚██╗██║
     ╚██████╗╚██████╔╝██║ ╚████║███████║╚██████╔╝███████╗███████╗    ██║  ██║██║     ██║     ███████╗██║╚██████╗██║  ██║   ██║   ██║╚██████╔╝██║ ╚████║
      ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝ ╚═════╝ ╚══════╝╚══════╝    ╚═╝  ╚═╝╚═╝     ╚═╝     ╚══════╝╚═╝ ╚═════╝╚═╝  ╚═╝   ╚═╝   ╚═╝ ╚═════╝ ╚═╝  ╚═══╝
                                                                                                                                                       ";
            Console.WriteLine(d);
            Console.WriteLine(s);
            int[] fq = { 261, 277, 293, 311, 329, 349, 370, 392, 415, 440, 466, 493 };
            int duration = 300;
            do
            {
                Console.WriteLine("1. For Operation on Products");
                Console.WriteLine("2. For Operation on Sale");
                Console.WriteLine("3. For Clear Console");
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
                        Console.Beep(fq[5], duration);
                        SubMenuforProducts.SubMenuForProduct();// For enter Product Services
                        break;
                    case 2:
                        Console.Beep(fq[5], duration);
                        SubMenuforSale.SubMenuForSale(); // For Enter Sale Services
                        break;
                    case 3:
                        Console.Beep(fq[5], duration);
                        Console.Clear(); //Clear Terminal
                        break;
                    case 0:
                        Console.Beep(fq[0], duration);
                        Console.WriteLine("Program was ended... Bye :)");
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
