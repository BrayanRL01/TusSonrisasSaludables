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
        public List<VWAppointmentViewModel>? GetAppointmentsView()
        {
            try
            {
                List<VWAppointmentViewModel>? list = new();
                HttpResponseMessage responseMessage = repository.GetResponse("api/Appointments/Appointments");
                if (responseMessage != null)
                {
                    var content = responseMessage.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<VWAppointmentViewModel>?>(content);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VWAdminAppointmentViewModel>? GetAdminAppointmentsView()
        {
            try
            {
                List<VWAdminAppointmentViewModel>? list = new();
                HttpResponseMessage responseMessage = repository.GetResponse("api/Appointments/AdminAppointments");
                if (responseMessage != null)
                {
                    var content = responseMessage.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<VWAdminAppointmentViewModel>>(content);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public VWAdminAppointmentViewModel? GetViewByID(int id)
        {

            HttpResponseMessage responseMessage = repository.GetResponse("api/Appointments/AdminAppointment/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            VWAdminAppointmentViewModel? Appointment = JsonConvert.DeserializeObject<VWAdminAppointmentViewModel>(content);
            return Appointment;
        }

        public AppointmentViewModel? GetByID(int id)
        {
            HttpResponseMessage responseMessage = repository.GetResponse("api/Appointments/AppointmentInfo/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            AppointmentViewModel? Appointment = JsonConvert.DeserializeObject<AppointmentViewModel>(content);

            return Appointment;
        }

        public List<VWAdminAppointmentViewModel>? GetUserAppointments(string email)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.GetResponse("api/Appointments/UserAppointments/" + email);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                List<VWAdminAppointmentViewModel>? Appointment = JsonConvert.DeserializeObject<List<VWAdminAppointmentViewModel>?>(content);
                return Appointment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Put
        public string Edit(AppointmentViewModel Cita)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PutResponse("api/Appointments/Appointment/", Cita);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                string mensaje = content.ToString();
                return mensaje;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region Add
        public string Add(AppointmentViewModel appointment)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PostResponse("api/Appointments/Appointment", appointment);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                string mensaje = content.ToString();
                return mensaje;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region Delete
        public string Delete(int id)
        {
            HttpResponseMessage responseMessage = repository.DeleteResponse("api/Appointments/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            string mensaje = content;
            return mensaje;
        }
        #endregion

        #region Confirm and Cancel
        public string Confirm(AppointmentViewModel model)
        {
            try
            {
                HttpResponseMessage? responseMessage = repository.PutResponse("api/Appointments/ConfirmAppointment/", model);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                string mensaje = content.ToString();
                return mensaje;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Cancel(AppointmentViewModel model)
        {
            try
            {
                HttpResponseMessage? responseMessage = repository.PutResponse("api/Appointments/CancelAppointment/", model);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                string mensaje = content.ToString();
                return mensaje;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region Counts
        public string AvailableAppointments()
        {
            HttpResponseMessage responseMessage = repository.GetResponse("api/Appointments/AvailableAppointments/");
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            string count = content;
            return count;
        }

        public string ReservedAppointments()
        {
            HttpResponseMessage responseMessage = repository.GetResponse("api/Appointments/ReservedAppointments/");
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            string count = content;
            return count;
        }
        #endregion

        #region Next
        public string NextAppointments()
        {
            HttpResponseMessage responseMessage = repository.GetResponse("api/Appointments/NextAppointment/");
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            string next = content;
            return next;
        }
        #endregion
    }
}
