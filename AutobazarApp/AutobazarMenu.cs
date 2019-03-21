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
        public static void Run(Autobazar autobazar)
        {
            string action = "0";

            do
            {
                Common.ConsoleHorizontalLine();
                Console.WriteLine("AUTOBAZAR MENU");
                Console.WriteLine("A - Add vehicle");
                Console.WriteLine("E - Edit vehicle");
                Console.WriteLine("D - Delete vehicle");
                Console.WriteLine("L - Load vehicles");
                Console.WriteLine("S - Save vehicles");
                Console.WriteLine("Q - Quit");
                Common.ConsoleHorizontalLine();

                action = Console.ReadLine().ToUpper();

                switch (action)
                {                    
                    case "A":
                        {
                            Add(autobazar);
                            break;
                        }
                    case "E":
                        {
                            Edit(autobazar);
                            break;
                        }
                    case "D":
                        {
                            Delete(autobazar);
                            break;
                        }
                    case "L":
                        {
                            Load(autobazar);
                            
                            break;
                        }
                    case "S":
                        {
                            Save(autobazar);
                            
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

        private static void Add(Autobazar autobazar)
        {
            Vehicle vehicle = new Vehicle(autobazar.GetNextId());

            vehicle.YearOfProduction = InputValidator.GetPositiveNumber("Enter year of production");
            vehicle.NumberOfKm = InputValidator.GetPositiveNumber("Enter number of km");
            vehicle.VehicleBrand = InputValidator.GetText("Enter brand of vehicle");
            vehicle.VehicleType = InputValidator.GetText("Enter type of vehicle");
            vehicle.VehicleFuel = InputValidator.GetFuel();
            vehicle.Price = InputValidator.GetPositiveDecimal("Enter price");
            vehicle.City = InputValidator.GetText("Enter city");
            vehicle.NumberOfDoors = InputValidator.GetPositiveNumber("Enter number of doors");
            vehicle.IsCrashed = InputValidator.GetBoolen("Enter if vehicle was crashed - Yes/No");
            
            try
            {
                autobazar.AddVehicle(vehicle);
            }
            catch
            {
                
                Console.WriteLine("Error! Something went wrong with adding vehicle. Application will be closed. Press any key.");
                Console.ReadKey();
                return;
            }

            // TODO: nepouzivat cisla, prepisat vsetky cisla
            Console.WriteLine(new string('-', 79));
            Console.WriteLine("Vehicle was added.");

        }

        private static void Edit(Autobazar autobazar)
        {
            WriteVehiclesToScreen(autobazar);

            int vehicleId = InputValidator.GetPositiveNumber("Enter number of vehicle to edit");
            Vehicle vehicle = autobazar.GetVehicleById(vehicleId);
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
                        autobazar.EditVehicle(vehicle);
                    }
                    catch (AutobazarException)
                    {
                        isError = true;
                        Console.WriteLine("Vehicle not found");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Error! Something went wrong with editing vehicle. Application will be closed. Press any key.");
                        Console.ReadKey();
                        return;
                    }

                    if (isError == false)
                    {
                        Console.WriteLine(new string('-', 79));
                        Console.WriteLine("Vehicle was edited.");
                    }

                    toContinue = InputValidator.GetBoolen($"Something else to edit on vehicle {vehicleId} ? Yes/No");
                } while (toContinue);
            }
        }

        private static void Delete(Autobazar autobazar)
        {
            WriteVehiclesToScreen(autobazar);
            int vehicleId = InputValidator.GetPositiveNumber("Enter number of vehicle to delete");

            bool isError = false;
            try
            {
                autobazar.DeleteVehicle(vehicleId);
            }
            catch (AutobazarException)
            {
                isError = true;
                Console.WriteLine("Vehicle not found");
            }
            catch (Exception)
            {
                isError = true;
                Console.WriteLine("Error! Something went wrong with deleting vehicle. Application will be closed. Press any key.");
                Console.ReadKey();
                return;
            }

            if (isError == false)
            {
                Console.WriteLine(new string('-', 79));
                Console.WriteLine("Vehicle was deleted.");
            }
        }

        private static void Load(Autobazar autobazar)
        {
            bool isError = false;
            try
            {
                autobazar.LoadVehicles();
            }
            catch (Exception)
            {
                isError = true;
                Console.WriteLine("Error! Something went wrong with loading vehicles. Application will be closed. Press any key.");
                Console.ReadKey();
                return;
            }

            if (isError == false)
            {
                if (autobazar.GetVehicleCount() == 0)
                {
                    Console.WriteLine("There are no vehicles in autobazar.");
                }
                else
                {
                    WriteVehiclesToScreen(autobazar);
                }
            }
        }

        private static void Save(Autobazar autobazar)
        {
            if (autobazar.GetVehicleCount() == 0)
            {
                Console.WriteLine("There are no vehicles to save");
            }
            else
            {
                bool isError = false;
                try
                {
                    autobazar.SaveVehicles();
                }
                catch (Exception)
                {
                    isError = true;
                    Console.WriteLine("Error! Something went wrong with saving vehicle. Application will be closed. Press any key.");
                    Console.ReadKey();
                    return;
                }
                if (isError == false)
                {
                    Console.WriteLine(new string('-', 79));
                    Console.WriteLine("Vehicles were saved.");
                }
            }
        }


        private static void WriteVehiclesToScreen(Autobazar autobazar)
        {
            Common.ConsoleHorizontalLine();
            List<string> vehicles = autobazar.ShowVehicles();
            foreach (var vehicle in vehicles)
            {
                Console.WriteLine(vehicle);
            }
        }

        private static void WriteMenuForEditToScreen()
        {
            Console.WriteLine(new string('=', 79));
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
