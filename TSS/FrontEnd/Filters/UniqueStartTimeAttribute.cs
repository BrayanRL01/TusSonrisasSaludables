using FrontEnd.Helpers;
using FrontEnd.Models;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Filters
{
    public class UniqueStartTimeAttribute : ValidationAttribute
    {
        AppointmentsHelper appointmentsHelper = new();
        public override bool IsValid(object value)
        {
            var start = (DateTime)value;
            if (start == null)
            {
                return true;
            }

            // Verifica si el nombre ya existe en la lista
            var items = GetItemsFromDatabase();
            return !items.Any(x => x.StartTime == start);
        }

        private List<VWAdminAppointmentViewModel> GetItemsFromDatabase()
        {
            List<VWAdminAppointmentViewModel> list = appointmentsHelper.GetAdminAppointmentsView();
            return list;
        }
    }
}
