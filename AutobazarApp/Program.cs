using System;

namespace AutobazarApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Autobazar bazar = new Autobazar();
            try
            {
                bazar.LoadVehicles();
            }
            catch
            {
                Console.WriteLine("Error! Something went wrong with loading vehicles.");
                Console.WriteLine("Application will be closed. Press any key.");
                Console.ReadKey();
                return;
            }

            AutobazarMenu.Run(bazar);          
        }
    }
}
