using Dapper;
using Flight_System.Interfaces;
using Flight_System.MOdels;
using Flight_System.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;

namespace Flight_System.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly IConfiguration _configuration;
        private readonly int _iteration = 3;
        public UserRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<UserLoginResponseClass> userLogin(UserLoginDto value)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnectionString");
            var selectQuery = "select * from UserTable where Email=@email";
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var user = await connection.QueryFirstOrDefaultAsync<UserModel>(selectQuery, new { email = value.email });
                if (user == null)
                {
                    return new UserLoginResponseClass(user: new UserData(), success: false, message: "email is not found");
                }

                var passwordHash = PasswordHasher.ComputeHash(value.password, user.PasswordSalt, _iteration);

                if (passwordHash != user.PasswordHash)
                {
                    return new UserLoginResponseClass(user: new UserData(), success: false, message: "password is wrong");
                }
                var userData = await connection.QueryFirstOrDefaultAsync<UserData>(selectQuery, new { email = value.email });
                return new UserLoginResponseClass(user: userData, success: true, message: "logged in successfully");

            }

        }

        public async Task<UserRegistrationResponse> userRegistration(UserRegistrationDto value)
        {
            if (value == null)
            {
                return new UserRegistrationResponse( success: false, message: "Empty request body");
            }
            var checkMail = "select * from UserTable where Email =@Email";
            var connectionString = _configuration.GetConnectionString("DefaultConnectionString");
            string myuuidAsString = Guid.NewGuid().ToString();
            var createNewUser = "insert into UserTable (userId, name,email, passwordSalt, passwordHash,userType, isParent) values(@Id,@Name,@Email,@PasswordSalt, @PasswordHash, @UserType,@IsParent)";
            var passwordSalt = PasswordHasher.GenerateSalt();
            var passwordHash = PasswordHasher.ComputeHash(value.password, passwordSalt, _iteration);
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var data = await connection.QueryFirstOrDefaultAsync<UserModel>(checkMail, new { Email = value.email });
                if (data != null)
                {
                    return new UserRegistrationResponse(success: false, message: "email is already registered");
                }
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {

                        var result = await connection.ExecuteAsync(createNewUser, new
                        {
                            Id = myuuidAsString,
                            Name = value.name,
                            Email = value.email,
                            PasswordSalt = passwordSalt,
                            PasswordHash = passwordHash,
                            UserType = value.userType,
                            IsParent = true,
                        }, transaction) ;
                        if (result != 0)
                        {
                            transaction.Commit();
                            return new UserRegistrationResponse(success: true, message: "user created successfully");
                        }

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return new UserRegistrationResponse(success: false, message: $"failed to create user:- {ex}");
                    }
                }

                return new UserRegistrationResponse(success: false, message: "failed to register the user");
            }
        }

        public async Task<UserRegistrationResponse> createSubUser(SubUserRegistrationDto value)
        {
            if (value == null)
            {
                return new UserRegistrationResponse(success: false, message: "Empty request body");
            }
            var checkMail = "select * from UserTable where Email =@Email";
            var connectionString = _configuration.GetConnectionString("DefaultConnectionString");
            string myuuidAsString = Guid.NewGuid().ToString();
            var createNewUser = "insert into UserTable (userId, name,email, passwordSalt, passwordHash,userType, isParent, parentId) values(@Id,@Name,@Email,@PasswordSalt, @PasswordHash, @UserType,@IsParent, @ParentId)";
            var passwordSalt = PasswordHasher.GenerateSalt();
            var passwordHash = PasswordHasher.ComputeHash(value.password, passwordSalt, _iteration);
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var data = await connection.QueryFirstOrDefaultAsync<UserModel>(checkMail, new { Email = value.email });
                if (data != null)
                {
                    return new UserRegistrationResponse(success: false, message: "email is already registered");
                }
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {

                        var result = await connection.ExecuteAsync(createNewUser, new
                        {
                            Id = myuuidAsString,
                            Name = value.name,
                            Email = value.email,
                            PasswordSalt = passwordSalt,
                            PasswordHash = passwordHash,
                            UserType = value.userType,
                            IsParent = false,
                            ParentId= value.parentId,   
                        }, transaction);
                        if (result != 0)
                        {
                            transaction.Commit();
                            return new UserRegistrationResponse(success: true, message: "user created successfully");
                        }

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return new UserRegistrationResponse(success: false, message: $"failed to create user:- {ex}");
                    }
                }

                return new UserRegistrationResponse(success: false, message: "failed to register the user");
            }
        }

        public async Task<UserLoginResponseClass> getUserById(string id)
        {
            var query = "select * from usertable where userId = @Id";
            var connectionString = _configuration.GetConnectionString("DefaultConnectionString");
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var data = await connection.QueryFirstOrDefaultAsync<UserData>(query, new { Id = id });
                if (data != null)
                {
                    return new UserLoginResponseClass(success: true, message: "user found", user: data);

                }
                return new UserLoginResponseClass(success: false, message: "user not found", user: new UserData());

            }
        }
    }
}
