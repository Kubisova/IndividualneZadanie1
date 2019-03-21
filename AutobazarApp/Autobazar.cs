using AutobazarApp.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutobazarApp
{
    public class Autobazar
    {
        private List<Vehicle> vehicles;
        string dataPath = "Autobazar.txt";

        public Autobazar()
        {
            vehicles = new List<Vehicle>();
        }

        public void AddVehicle(Vehicle vehicle)
        {
            vehicles.Add(vehicle);
            SaveLastVehicle();            
        }

        public void EditVehicle(Vehicle changedVehicle)
        {
            int index = 0;
            bool isFound = false;
            foreach (var vehicle in vehicles)
            {
                if (vehicle.Id == changedVehicle.Id)
                {
                    index = vehicles.IndexOf(vehicle);
                    isFound = true;
                    break;
                }
            }
            if (isFound)
            {
                vehicles[index] = changedVehicle;
                SaveVehicles();
            }
            else
            {
                throw new AutobazarException("Vehicle not found");
            }
            
        }

        public void DeleteVehicle(int id)
        {
            Vehicle vehicle = GetVehicleById(id);
            if (vehicle != null)
            {
                vehicles.Remove(vehicle);
            }
            else
            {
                throw new AutobazarException("Vehicle not found!");
            }

            SaveVehicles();
        }

        public void LoadVehicles()
        {
            if (File.Exists(dataPath))
            {
                try
                {
                    string[] vehiclesFromFile = File.ReadAllLines(dataPath);

                    if (vehiclesFromFile.Count() > 0)

                    {
                        vehicles.Clear();
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
                            vehicles.Add(vehicle);
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

        public void SaveLastVehicle()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(dataPath, true))
                {
                    sw.WriteLine(vehicles.Last().ToString());
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ErrorLogging(ex);
                throw;
            }
        }
        public void SaveVehicles() 
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(dataPath))
                {
                    foreach (var vehicle in vehicles)
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

        public int GetNextId()
        {
            if (vehicles.Count() == 0)
            {
                return 1;
            }
            else
            {
                return vehicles.Last().Id + 1;
            }
        }

        public List<string> ShowVehicles()
        {
            List<string> showVehicles = new List<string>();
            foreach (var vehicle in vehicles)
            {
                showVehicles.Add($"{vehicle.Id} - {vehicle.VehicleBrand} {vehicle.VehicleType}, year of production: {vehicle.YearOfProduction}, number of km: {vehicle.NumberOfKm},\n price: {vehicle.Price}, number of doors: {vehicle.NumberOfDoors}, fuel: {vehicle.VehicleFuel}, crashed: {vehicle.IsCrashed}, city: {vehicle.City}");
                showVehicles.Add(new string ('-', 79 ));
            }

            return showVehicles;
        }

        public Vehicle GetVehicleById(int id)
        {
            Vehicle vehicleById = null;
            foreach (var vehicle in vehicles)
            {
                if (vehicle.Id == id)
                {
                    vehicleById = vehicle;
                    break;
                }
            }

            return vehicleById;
        }

        public int GetVehicleCount()
        {
            return vehicles.Count;
        }
    }
}
