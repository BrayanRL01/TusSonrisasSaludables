namespace Lab3MVC.Models.Domain
{
    public class Student
    {
        private string name;
        private string email;
        private string password;

        public Student()
        {

        }

        public Student(string name, string email, string password)
        {
            this.name = name;
            this.email = email;
            this.password = password;
        }

        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
    }
}
