using System;

namespace AutobazarApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            try
            {
                Autobazar.LoadVehicles();
            }
            catch
            {
                Console.WriteLine("Error! Something went wrong with loading vehicles.");
                Console.WriteLine("Application will be closed. Press any key.");
                Console.ReadKey();
                return;
            }

            AutobazarMenu.Run();          
        }
    }
}
