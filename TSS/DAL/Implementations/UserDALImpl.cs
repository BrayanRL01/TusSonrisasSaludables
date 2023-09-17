using DAL.Interfaces;
using Entities.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class UserDALImpl : IUserDAL
    {
        private readonly TusSonrisasSaludablesContext _context;

        public UserDALImpl()
        {
        }

        public UserDALImpl(TusSonrisasSaludablesContext context)
        {
            _context = context;
        }

        public async Task<int> Add(User user)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@TypeID", user.TypeId));
            parameter.Add(new SqlParameter("@GenreID", user.GenreId));
            parameter.Add(new SqlParameter("@ProvinceID", user.ProvinceId));
            parameter.Add(new SqlParameter("@IDNumber", user.Idnumber));
            parameter.Add(new SqlParameter("@Username", user.UserName));
            parameter.Add(new SqlParameter("@FirstName", user.FirstName));
            parameter.Add(new SqlParameter("@LastName", user.LastName));
            parameter.Add(new SqlParameter("@BirthDate", user.BirthDate));
            parameter.Add(new SqlParameter("@Email", user.Email));
            parameter.Add(new SqlParameter("@Phone", user.PhoneNumber));
            parameter.Add(new SqlParameter("@UserAddress", user.UserAddress));
            parameter.Add(new SqlParameter("@Password", user.PasswordHash));

            var result = await Task.Run(() => _context.Database
           .ExecuteSqlRawAsync(@"EXEC SP_CreateUser @TypeID, @GenreID, @ProvinceID, @IDNumber, @Username,"" +
              //        ""@FirstName, @LastName, @BirthDate, @Email,"" +
              //        ""@Phone, @UserAddress, @Password", parameter.ToArray()));

            return result;
            //try
            //{
            //    string Query = "EXEC SP_CreateUser @TypeID, @GenreID, @ProvinceID, @IDNumber, @Username," +
            //        "@FirstName, @LastName, @BirthDate, @Email," +
            //        "@Phone, @UserAddress, @Password";
            //    var param = new SqlParameter[]
            //    {
            //        new SqlParameter()
            //        {
            //            ParameterName = "@TypeID",
            //            SqlDbType = System.Data.SqlDbType.Int,
            //            Direction = System.Data.ParameterDirection.Input,
            //            Value = entity.TypeId
            //        },
            //         new SqlParameter()
            //        {
            //            ParameterName = "@GenreID",
            //            SqlDbType = System.Data.SqlDbType.Int,
            //            Direction = System.Data.ParameterDirection.Input,
            //            Value = entity.GenreId
            //        },
            //         new SqlParameter()
            //        {
            //            ParameterName = "@ProvinceID",
            //            SqlDbType = System.Data.SqlDbType.Int,
            //            Direction = System.Data.ParameterDirection.Input,
            //            Value = entity.ProvinceId
            //        },
            //         new SqlParameter()
            //        {
            //            ParameterName = "@IDNumber",
            //            SqlDbType = System.Data.SqlDbType.VarChar,
            //            Direction = System.Data.ParameterDirection.Input,
            //            Value = entity.Idnumber
            //        },
            //         new SqlParameter()
            //        {
            //            ParameterName = "@Username",
            //            SqlDbType = System.Data.SqlDbType.VarChar,
            //            Direction = System.Data.ParameterDirection.Input,
            //            Value = entity.UserName
            //        },
            //         new SqlParameter()
            //        {
            //            ParameterName = "@FirstName",
            //            SqlDbType = System.Data.SqlDbType.VarChar,
            //            Direction = System.Data.ParameterDirection.Input,
            //            Value = entity.FirstName
            //        },
            //         new SqlParameter()
            //        {
            //            ParameterName = "@LastName",
            //            SqlDbType = System.Data.SqlDbType.VarChar,
            //            Direction = System.Data.ParameterDirection.Input,
            //            Value = entity.LastName
            //        },
            //         new SqlParameter()
            //        {
            //            ParameterName = "@BirthDate",
            //            SqlDbType  = System.Data.SqlDbType.Date,
            //            Direction = System.Data.ParameterDirection.Input,
            //            Value = entity.BirthDate
            //        },
            //         new SqlParameter()
            //        {
            //            ParameterName = "@Email",
            //            SqlDbType = System.Data.SqlDbType.VarChar,
            //            Direction = System.Data.ParameterDirection.Input,
            //            Value = entity.Email
            //        },
            //         new SqlParameter()
            //        {
            //            ParameterName = "@Phone",
            //            SqlDbType = System.Data.SqlDbType.VarChar,
            //            Direction = System.Data.ParameterDirection.Input,
            //            Value = entity.PhoneNumber
            //        },
            //         new SqlParameter()
            //        {
            //            ParameterName = "@UserAddress",
            //            SqlDbType = System.Data.SqlDbType.VarChar,
            //            Direction = System.Data.ParameterDirection.Input,
            //            Value = entity.UserAddress
            //        },
            //         new SqlParameter()
            //        {
            //            ParameterName = "@Password",
            //            SqlDbType = System.Data.SqlDbType.VarChar,
            //            Direction = System.Data.ParameterDirection.Input,
            //            Value = entity.PasswordHash
            //        }
            //    };
            //    var result = await _context.Database.ExecuteSqlRawAsync(Query, param);

            //    return result;
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        public Task<List<User>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<int> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
