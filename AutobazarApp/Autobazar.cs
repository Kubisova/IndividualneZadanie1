using AutobazarApp.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutobazarApp
{
    static class Autobazar
    {
        private static List<Vehicle> _vehicles;
        private static string _dataPath = "Autobazar.txt";

        static Autobazar()
        {
            _vehicles = new List<Vehicle>();
        }

        public static void AddVehicle(Vehicle vehicle)
        {
            _vehicles.Add(vehicle);
            SaveLastVehicle();            
        }

        public static void EditVehicle(Vehicle changedVehicle)
        {
            int index = 0;
            bool isFound = false;
            foreach (var vehicle in _vehicles)
            {
                if (vehicle.Id == changedVehicle.Id)
                {
                    index = _vehicles.IndexOf(vehicle);
                    isFound = true;
                    break;
                }
            }
            if (isFound)
            {
                _vehicles[index] = changedVehicle;
                SaveVehicles();
            }
            else
            {
                throw new AutobazarException("Vehicle not found");
            }
            
        }

        public static void DeleteVehicle(int id)
        {
            Vehicle vehicle = GetVehicleById(id);
            if (vehicle != null)
            {
                _vehicles.Remove(vehicle);
            }
            else
            {
                throw new AutobazarException("Vehicle not found!");
            }

            SaveVehicles();
        }

        public static void LoadVehicles()
        {
            if (File.Exists(_dataPath))
            {
                try
                {
                    string[] vehiclesFromFile = File.ReadAllLines(_dataPath);

                    if (vehiclesFromFile.Count() > 0)

                    {
                        _vehicles.Clear();
                        foreach (var vehicleFromFile in vehiclesFromFile)
                        {
                            string[] propertiesOfVehicle = vehicleFromFile.Split('\t');
                            Vehicle vehicle = new Vehicle(int.Parse(propertiesOfVehicle[0]));
                            vehicle.YearOfProduction = int.Parse(propertiesOfVehicle[1]);
                            vehicle.NumberOfKm = int.Parse(propertiesOfVehicle[2]);
                            vehicle.VehicleBrand = propertiesOfVehicle[3];
                            vehicle.VehicleType = propertiesOfVehicle[4];
                            string fuel = propertiesOfVehicle[5];
                            if (fuel == "Petrol")
                            {
                                vehicle.VehicleFuel = Fuel.Petrol;
                            }
                            else if (fuel == "Diesel")
                            {
                                vehicle.VehicleFuel = Fuel.Diesel;
                            }
                            else if (fuel == "Gas")
                            {
                                vehicle.VehicleFuel = Fuel.Gas;
                            }
                            vehicle.Price = decimal.Parse(propertiesOfVehicle[6]);
                            vehicle.City = propertiesOfVehicle[7];
                            vehicle.NumberOfDoors = int.Parse(propertiesOfVehicle[8]);
                            vehicle.IsCrashed = bool.Parse(propertiesOfVehicle[9]);
                            _vehicles.Add(vehicle);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorLogger.ErrorLogging(ex);
                    throw;
                }
            }
        }

        public static void SaveLastVehicle()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(_dataPath, true))
                {
                    sw.WriteLine(_vehicles.Last().ToString());
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ErrorLogging(ex);
                throw;
            }
        }
        public static void SaveVehicles() 
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(_dataPath))
                {
                    foreach (var vehicle in _vehicles)
                    {
                        sw.WriteLine(vehicle.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ErrorLogging(ex);
                throw;                
            }
        }

        public static int GetNextId()
        {
            if (_vehicles.Count() == 0)
            {
                return 1;
            }
            else
            {
                return _vehicles.Last().Id + 1;
            }
        }

        public static List<string> ShowVehicles()
        {
            List<string> showVehicles = new List<string>();
            foreach (var vehicle in _vehicles)
            {
                showVehicles.Add($"{vehicle.Id} - {vehicle.VehicleBrand} {vehicle.VehicleType}, year of production: {vehicle.YearOfProduction}, number of km: {vehicle.NumberOfKm},\n price: {vehicle.Price}, number of doors: {vehicle.NumberOfDoors}, fuel: {vehicle.VehicleFuel}, crashed: {vehicle.IsCrashed}, city: {vehicle.City}");
                showVehicles.Add(new string('-', Console.WindowWidth - 1));
            }

            return showVehicles;
        }

        public static Vehicle GetVehicleById(int id)
        {
            Vehicle vehicleById = null;
            foreach (var vehicle in _vehicles)
            {
                if (vehicle.Id == id)
                {
                    vehicleById = vehicle;
                    break;
                }
            }

            return vehicleById;
        }

        public static int GetVehicleCount()
        {
            return _vehicles.Count;
        }
    }
}
