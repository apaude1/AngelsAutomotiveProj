using AngelsAutomotive.Data.Entities;
using AngelsAutomotive.Models;

namespace AngelsAutomotive.Helpers
{
    public interface IConverterHelper //All the methods I use to convert I put here
    {
        Vehicle ToVehicle(VehicleViewModel model, string path, bool isNew);



        VehicleViewModel ToVehicleViewModel(Vehicle model);
    }
}
