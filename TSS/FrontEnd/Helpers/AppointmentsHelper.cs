using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Helpers
{
    public class AppointmentsHelper
    {
        ServiceRepository repository;

        public AppointmentsHelper()
        {
            repository = new ServiceRepository();
        }

        #region GetAll
        public List<VWAppointmentViewModel> GetAppointmentsView()
        {
            List<VWAppointmentViewModel> list = new List<VWAppointmentViewModel>();
            HttpResponseMessage responseMessage = repository.GetResponse("api/Appointments/UserAppointments");
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<VWAppointmentViewModel>>(content);
            }
            return list;
        }

        public List<VWAdminAppointmentViewModel> GetAdminAppointmentsView()
        {
            List<VWAdminAppointmentViewModel> list = new();
            HttpResponseMessage responseMessage = repository.GetResponse("api/Appointments/AdminAppointments");
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<VWAdminAppointmentViewModel>>(content);
            }
            return list;
        }

        public VWAdminAppointmentViewModel GetViewByID(int id)
        {
            HttpResponseMessage responseMessage = repository.GetResponse("api/Appointments/AdminAppointment/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            VWAdminAppointmentViewModel Appointment = JsonConvert.DeserializeObject<VWAdminAppointmentViewModel>(content);

            return Appointment;
        }

        public AppointmentViewModel GetByID(int id)
        {
            HttpResponseMessage responseMessage = repository.GetResponse("api/Appointments/Appointment/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            AppointmentViewModel Appointment = JsonConvert.DeserializeObject<AppointmentViewModel>(content);

            return Appointment;
        }
        #endregion

        #region Put
        public AppointmentViewModel Edit(AppointmentViewModel Cita)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PutResponse("api/Appointments/Appointment/", Cita);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                AppointmentViewModel CitaAPI = JsonConvert.DeserializeObject<AppointmentViewModel>(content);

                return CitaAPI;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Add
        public AppointmentViewModel Add(AppointmentViewModel Cita)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PostResponse("api/Appointments/Appointment", Cita);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                AppointmentViewModel CitaAPI = JsonConvert.DeserializeObject<AppointmentViewModel>(content);
                return CitaAPI;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Delete
        public AppointmentViewModel Delete(int id)
        {
            try
            {
                AppointmentViewModel Cita = new();
                HttpResponseMessage responseMessage = repository.DeleteResponse("api/Appointments/" + id);
                return Cita;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
