using FrontEnd.Helpers;
using FrontEnd.Models;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Filters
{
    public class UniqueEndTimeAttribute : ValidationAttribute
    {
        private AppointmentsHelper appointmentsHelper = new();
        public override bool IsValid(object value)
        {
            var end = (DateTime)value;
            if (end == null)
            {
                return true;
            }

            // Verifica si el nombre ya existe en la lista
            var items = GetItemsFromDatabase();
            return !items.Any(x => x.EndTime == end);
        }

        private List<VWAdminAppointmentViewModel> GetItemsFromDatabase()
        {
            List<VWAdminAppointmentViewModel> list = appointmentsHelper.GetAdminAppointmentsView();
            return list;
        }
    }
}
