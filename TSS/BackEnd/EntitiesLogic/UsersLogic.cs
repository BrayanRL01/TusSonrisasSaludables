using Entities.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.EntitiesLogic
{
    public class UsersLogic
    {
        private TusSonrisasSaludablesContext _context;

        public UsersLogic(TusSonrisasSaludablesContext context)
        {
            _context = context;
        }

        public bool Add(User entity)
        {
            try
            {
                string Query = "EXEC SP_CreateUser @TypeID, @GenreID, @ProvinceID, @IDNumber, @Username, @FirstName, @LastName, @BirthDate, @Email, @Phone, @UserAddress, @Password";
                var param = new SqlParameter[]
                {
                    new SqlParameter()
                    {
                        ParameterName = "@TypeID",
                        SqlDbType= System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value= entity.TypeId
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@GenreID",
                        SqlDbType= System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value= entity.GenreId
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@ProvinceID",
                        SqlDbType= System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value= entity.ProvinceId
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@IDNumber",
                        SqlDbType= System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value= entity.Idnumber
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@Username",
                        SqlDbType= System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value= entity.UserName
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@FirstName",
                        SqlDbType= System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value= entity.FirstName
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@LastName",
                        SqlDbType= System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value= entity.LastName
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@BirthDate",
                        SqlDbType= System.Data.SqlDbType.Date,
                        Direction = System.Data.ParameterDirection.Input,
                        Value= entity.BirthDate
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@Email",
                        SqlDbType= System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value= entity.Email
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@UserAddress",
                        SqlDbType= System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value= entity.UserAddress
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@Password",
                        SqlDbType= System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value= entity.PasswordHash
                    },
                };
                int resultado = _context.Database.ExecuteSqlRaw(Query, param);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
