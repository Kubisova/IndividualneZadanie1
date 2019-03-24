using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AutobazarApp
{
    public enum Fuel { Petrol, Diesel, Gas }

    public class Vehicle
    {
        public int Id { get; private set; }
        public int YearOfProduction { get; set; }
        public int NumberOfKm { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleType { get; set; }
        public Fuel VehicleFuel { get; set; }
        public decimal Price { get; set; }
        public string City { get; set; }
        public int NumberOfDoors { get; set; }
        public bool IsCrashed { get; set; }

        public Vehicle(int id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return $"{Id}\t{YearOfProduction}\t{NumberOfKm}\t{VehicleBrand}\t{VehicleType}\t{VehicleFuel}\t{Price}\t{City}\t{NumberOfDoors}\t{IsCrashed}";
        }
    }
}
