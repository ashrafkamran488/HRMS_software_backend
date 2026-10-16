
using HrOne_Rep.DTOs;
using HrOne_Rep.IRepository;
using HrOne_Rep.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Data.SqlClient;

namespace HrOne_Rep.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration _configuration;

        public AuthRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Register(RegisterRequestDto request)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection con =
            new SqlConnection(connectionString))
            {
                string query = @"
                    INSERT INTO Users
                    (
                    UserName,
                    Password,
                    RoleName
                    )
                    VALUES
                    (
                    @UserName,
                    @Password,
                    @RoleName
                    )";

                SqlCommand cmd =
                new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@UserName", request.UserName);

                cmd.Parameters.AddWithValue("@Password", request.Password);

                cmd.Parameters.AddWithValue("@RoleName", request.RoleName);

                con.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public User? Login(LoginRequestDto request)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection con =
                new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT Id,
                           UserName,
                           Password,
                           RoleName
                    FROM Users
                    WHERE UserName = @UserName";

                SqlCommand cmd =
                    new SqlCommand(query, con);

                cmd.Parameters.AddWithValue(
                    "@UserName",
                    request.UserName);

                con.Open();

                SqlDataReader reader =
                    cmd.ExecuteReader();

                if (reader.Read())
                {
                    var user = new User
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        UserName = reader["UserName"].ToString(),
                        Password = reader["Password"].ToString(),
                        RoleName = reader["RoleName"].ToString()
                    };

                    if (user.Password == request.Password)
                    {
                        return user;
                    }
                }

                return null;
            }
        }


    }
}
