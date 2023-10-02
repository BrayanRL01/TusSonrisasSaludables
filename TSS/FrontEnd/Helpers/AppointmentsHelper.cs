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
            HttpResponseMessage responseMessage = repository.GetResponse("api/Appointments");
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
            HttpResponseMessage responseMessage = repository.GetResponse("api/Appointments/GETAdminCitas");
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<VWAdminAppointmentViewModel>>(content);
            }
            return list;
        }

        public VWAdminAppointmentViewModel GetByID(int id)
        {
            VWAdminAppointmentViewModel Appointment = new();
            HttpResponseMessage responseMessage = repository.GetResponse("api/Appointments/GetAdminAppointment/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            Appointment = JsonConvert.DeserializeObject<VWAdminAppointmentViewModel>(content);

            return Appointment;
        }
        #endregion

        #region Put
        public AppointmentViewModel Edit(AppointmentViewModel Cita)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PutResponse("api/Appointments/PutAppointment/", Cita);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                AppointmentViewModel CitaAPI = JsonConvert.DeserializeObject<AppointmentViewModel>(content);

                return CitaAPI;
            }
            catch (Exception ex)
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
                HttpResponseMessage responseMessage = repository.PostResponse("api/Appointments/PostAppointment", Cita);
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
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
