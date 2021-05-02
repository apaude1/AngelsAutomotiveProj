using AngelsAutomotive.Data.Entities;
using AngelsAutomotive.Models;
using System;

namespace AngelsAutomotive.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public Vehicle ToVehicle(VehicleViewModel model, string path, bool isNew)
        {
            return new Vehicle
            {
                Id = isNew ? 0 : model.Id, 
                VehiclePlateNumber = model.VehiclePlateNumber,
               // ImageUrl = path,
                VehMileage = model.VehMileage,
                VehMake = model.VehMake,
                VehModel = model.VehModel,
                VehYear = model.VehYear,
                VinNummber = model.VinNummber,
              //  Color = model.Color,
              //  Fuel = model.Fuel,
               // Remarks = model.Remarks,
                User = model.User
            };
        }



        public VehicleViewModel ToVehicleViewModel(Vehicle model)
        {
            return new VehicleViewModel
            {
                Id = model.Id,
                VehiclePlateNumber = model.VehiclePlateNumber,
                VehMileage = model.VehMileage,
                VehMake = model.VehMake,
                VehModel = model.VehModel,
                VehYear = model.VehYear,
                VinNummber = model.VinNummber,
                //  Color = model.Color,
                //  Fuel = model.Fuel,
                // Remarks = model.Remarks,
                User = model.User
            };
        }
    }
}
