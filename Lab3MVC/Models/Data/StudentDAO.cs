namespace Lab3MVC.Models.Data
    //COMUNICA CON LA BASE DE DATOS
{
    public class StudentDAO
    {

        private readonly IConfiguration _configuration;
        string connectionString;

        public StudentDAO(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
    }
}
