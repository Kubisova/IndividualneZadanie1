using AutobazarApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutobazarApp
{
    static class InputValidator
    {
        public static int GetPositiveNumber(string message)
        {
            bool isValid;
            int validInput;
            do
            {
                Common.ConsoleHorizontalLine('-');
                Console.WriteLine(message);
                isValid = int.TryParse(Console.ReadLine(), out validInput);
                if (isValid == false || validInput <= 0)
                {
                    Console.WriteLine($"You didn't enter positive number (integer)");
                    isValid = false;
                }
            } while (isValid == false);

            return validInput;
        }

        public static int GetNumberFromInterval(string message, int min, int max)
        {
            bool isValid;
            int validInput;
            do
            {
                Common.ConsoleHorizontalLine('-');
                Console.WriteLine(message);
                isValid = int.TryParse(Console.ReadLine(), out validInput);
                if (isValid == false || (validInput < min || validInput > max))
                {
                    Console.WriteLine($"You didn't enter number from interval {min} - {max}");
                    isValid = false;
                }
            } while (isValid == false);

            return validInput;
        }

        public static string GetText(string message)
        {
            string input;
            do
            {
                Common.ConsoleHorizontalLine('-');
                Console.WriteLine(message);
                input = Console.ReadLine();
                if (input == string.Empty)
                {
                    Console.WriteLine("It can't be empty");
                }
            } while (input == string.Empty);
            
            return input;
        }

        public static bool GetBoolen(string message)
        {
            bool isValid = false;
            bool isYes = false;
            do
            {
                Common.ConsoleHorizontalLine('-');
                Console.WriteLine(message);
                string crashed = Console.ReadLine().ToLower();
                if (crashed == "yes"||crashed =="y")
                {
                    isYes = true;
                    isValid = true;
                    
                }
                else if (crashed == "no"||crashed =="n")
                {
                    isYes = false;
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("You didn't enter valid answer");
                    isValid = false;
                }
            } while (isValid == false);

            return isYes;
        }

        public static decimal GetPositiveDecimal(string message)
        {
            bool isValid;
            decimal price;
            do
            {
                Common.ConsoleHorizontalLine('-');
                Console.WriteLine(message);
                isValid = decimal.TryParse(Console.ReadLine(), out price);
                if (isValid == false || price <= 0)
                {
                    Console.WriteLine($"You didn't enter positive number");
                    isValid = false;
                }
            } while (isValid == false);

            return price;
        }

        public static Fuel GetFuel()
        {
            bool isValid;
            int fuelType;
            do
            {
                Common.ConsoleHorizontalLine('-');
                Console.WriteLine("Choose the type of fuel");
                Console.WriteLine("0 - Petrol");
                Console.WriteLine("1 - Diesel");
                Console.WriteLine("2 - Gas");
                Common.ConsoleHorizontalLine('-');

                isValid = int.TryParse(Console.ReadLine(), out fuelType);
                if (isValid == false || fuelType < 0 || fuelType > 2)
                {
                    Console.WriteLine("You didn't choose valid type of fuel");
                    isValid = false;
                }
            } while (isValid == false);

            return (Fuel)fuelType;
        }


    }
}
