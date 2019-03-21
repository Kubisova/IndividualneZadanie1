using AutobazarApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutobazarApp
{
    static class AutobazarMenu
    {
        public static void Run()
        {
            string action;

            do
            {
                ConsoleWriter.ConsoleHorizontalLine();
                Console.WriteLine("AUTOBAZAR MENU");
                Console.WriteLine("A - Add vehicle");
                Console.WriteLine("E - Edit vehicle");
                Console.WriteLine("D - Delete vehicle");
                Console.WriteLine("L - Load vehicles");
                Console.WriteLine("S - Save vehicles");
                Console.WriteLine("Q - Quit");
                ConsoleWriter.ConsoleHorizontalLine();

                action = Console.ReadLine().ToUpper();

                switch (action)
                {                    
                    case "A":
                        {
                            try
                            {
                                Add();
                            }
                            catch 
                            {
                                Console.WriteLine("Error! Something went wrong with adding vehicle.");
                                Console.WriteLine("Application will be closed. Press any key.");
                                Console.ReadKey();
                                return;
                            }
                            break;
                        }
                    case "E":
                        {
                            try
                            {
                                Edit();
                            }
                            catch 
                            {
                                Console.WriteLine("Error! Something went wrong with editing vehicle.");
                                Console.WriteLine("Application will be closed. Press any key.");
                                Console.ReadKey();
                                return;
                            }
                            break;
                        }
                    case "D":
                        {
                            try
                            {
                                Delete();
                            }
                            catch 
                            {
                                Console.WriteLine("Error! Something went wrong with deleting vehicle.");
                                Console.WriteLine("Application will be closed. Press any key.");
                                Console.ReadKey();
                                return;
                            }
                            break;
                        }
                    case "L":
                        {
                            try
                            {
                                Load();
                            }
                            catch 
                            {
                                Console.WriteLine("Error! Something went wrong with loading vehicles.");
                                Console.WriteLine("Application will be closed. Press any key.");
                                Console.ReadKey();
                                return;
                            }
                            break;
                        }
                    case "S":
                        {
                            try
                            {
                                Save();
                            }
                            catch 
                            {
                                Console.WriteLine("Error! Something went wrong with saving vehicles.");
                                Console.WriteLine("Application will be closed. Press any key.");
                                Console.ReadKey();
                                return;
                            }
                            break;
                        }
                    case "Q":
                        {
                            Console.WriteLine("Bye");
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("You didn't choose valid action");
                            break;
                        }
                }
            } while (action != "Q");
        }

        private static void Add()
        {
            Vehicle vehicle = new Vehicle(Autobazar.GetNextId());

            vehicle.YearOfProduction = InputValidator.GetPositiveNumber("Enter year of production");
            vehicle.NumberOfKm = InputValidator.GetPositiveNumber("Enter number of km");
            vehicle.VehicleBrand = InputValidator.GetText("Enter brand of vehicle");
            vehicle.VehicleType = InputValidator.GetText("Enter type of vehicle");
            vehicle.VehicleFuel = InputValidator.GetFuel();
            vehicle.Price = InputValidator.GetPositiveDecimal("Enter price");
            vehicle.City = InputValidator.GetText("Enter city");
            vehicle.NumberOfDoors = InputValidator.GetPositiveNumber("Enter number of doors");
            vehicle.IsCrashed = InputValidator.GetBoolen("Enter if vehicle was crashed - Yes/No");
            
            Autobazar.AddVehicle(vehicle);

            ConsoleWriter.ConsoleHorizontalLine('-');
            Console.WriteLine("Vehicle was added.");
        }

        private static void Edit()
        {
            WriteVehiclesToScreen();

            int vehicleId = InputValidator.GetPositiveNumber("Enter number of vehicle to edit");
            Vehicle vehicle = Autobazar.GetVehicleById(vehicleId);
            if (vehicle == null)
            {
                Console.WriteLine("Vehicle not found");
            }
            else
            {
                bool toContinue = true;
                do
                {
                    WriteMenuForEditToScreen();
                    int propertyToEdit = InputValidator.GetNumberFromInterval("Enter number of selected property", 1, 9);

                    vehicle = GetNewData(propertyToEdit, vehicle);

                    bool isError = false;
                    try
                    {
                        Autobazar.EditVehicle(vehicle);
                    }
                    catch (VehicleNotFoundException)
                    {
                        isError = true;
                        Console.WriteLine("Vehicle not found");
                    }

                    if (isError == false)
                    {
                        ConsoleWriter.ConsoleHorizontalLine('-');
                        Console.WriteLine("Vehicle was edited.");
                    }

                    toContinue = InputValidator.GetBoolen($"Something else to edit on vehicle {vehicleId} ? Yes/No");
                } while (toContinue);
            }
        }

        private static void Delete()
        {
            WriteVehiclesToScreen();
            int vehicleId = InputValidator.GetPositiveNumber("Enter number of vehicle to delete");

            bool isError = false;
            try
            {
                Autobazar.DeleteVehicle(vehicleId);
            }
            catch (VehicleNotFoundException)
            {
                isError = true;
                Console.WriteLine("Vehicle not found");
            }

            if (isError == false)
            {
                ConsoleWriter.ConsoleHorizontalLine('-');
                Console.WriteLine("Vehicle was deleted.");
            }
        }

        private static void Load()
        {
            Autobazar.LoadVehicles();
            
            if (Autobazar.GetVehicleCount() == 0)
            {
                Console.WriteLine("There are no vehicles in autobazar.");
            }
            else
            {
                WriteVehiclesToScreen();
            }
        }

        private static void Save()
        {
            if (Autobazar.GetVehicleCount() == 0)
            {
                Console.WriteLine("There are no vehicles to save");
            }
            else
            {
                Autobazar.SaveVehicles();

                ConsoleWriter.ConsoleHorizontalLine('-');
                Console.WriteLine("Vehicles were saved.");
            }
        }

        private static void WriteVehiclesToScreen()
        {
            ConsoleWriter.ConsoleHorizontalLine('-');
            Console.WriteLine("YOUR VEHICLES:");
            ConsoleWriter.ConsoleHorizontalLine('-');
            List<string> vehicles = Autobazar.ShowVehicles();
            foreach (var vehicle in vehicles)
            {
                Console.WriteLine(vehicle);
            }
        }

        private static void WriteMenuForEditToScreen()
        {
            ConsoleWriter.ConsoleHorizontalLine('-');
            Console.WriteLine("EDIT MENU");
            Console.WriteLine("1 - Year of production");
            Console.WriteLine("2 - Number of km");
            Console.WriteLine("3 - Brand of vehicle");
            Console.WriteLine("4 - Type of vehicle");
            Console.WriteLine("5 - Type of fuel");
            Console.WriteLine("6 - Price");
            Console.WriteLine("7 - City");
            Console.WriteLine("8 - Number of doors");
            Console.WriteLine("9 - Vehicle has been crashed");
        }

        private static Vehicle GetNewData(int selectedProperty, Vehicle vehicle)
        {
            switch (selectedProperty)
            {
                case 1:
                    {
                        vehicle.YearOfProduction = InputValidator.GetPositiveNumber("Enter year of production");
                        break;
                    }
                case 2:
                    {
                        vehicle.NumberOfKm = InputValidator.GetPositiveNumber("Enter number of km");
                        break;
                    }
                case 3:
                    {
                        vehicle.VehicleBrand = InputValidator.GetText("Enter brand of vehicle");
                        break;
                    }
                case 4:
                    {
                        vehicle.VehicleType = InputValidator.GetText("Enter type of vehicle");
                        break;
                    }
                case 5:
                    {
                        vehicle.VehicleFuel = InputValidator.GetFuel();
                        break;
                    }
                case 6:
                    {
                        vehicle.Price = InputValidator.GetPositiveDecimal("Enter price");
                        break;
                    }
                case 7:
                    {
                        vehicle.City = InputValidator.GetText("Enter city");
                        break;
                    }
                case 8:
                    {
                        vehicle.NumberOfDoors = InputValidator.GetPositiveNumber("Enter number of doors");
                        break;
                    }
                case 9:
                    {
                        vehicle.IsCrashed = InputValidator.GetBoolen("Enter if vehicle was crashed - Yes/No");
                        break;
                    }
            }
            return vehicle;
        }
    }
}
