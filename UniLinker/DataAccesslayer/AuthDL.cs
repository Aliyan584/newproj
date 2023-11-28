using MySql.Data.MySqlClient;
using System.Data.Common;
using UniLinker.Model;
using static UniLinker.Model.SignUp;

namespace UniLinker.DataAccesslayer
{
    public class AuthDL : IAuthDL
    {   
        public readonly IConfiguration _configuration;
        public readonly MySqlConnection _mySqlConnection;
        public AuthDL(IConfiguration configuration)
        {
            _configuration = configuration;
            _mySqlConnection = new MySqlConnection(_configuration[key: "ConnectionStrings:MySqlDBConnection"]);
        }

        public async Task<SignInResponse> SignIn(SignInRequest request)
        {
            SignInResponse response = new SignInResponse();
            response.isSuccess = true;
            response.Message = "Successful";
            try
            {

                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"SELECT * 
                        FROM crudoperation.userdetail 
                        WHERE Email=@Email AND PassWord=@PassWord AND Role=@Role AND IsActive = 1;";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@Email", request.Email);
                    sqlCommand.Parameters.AddWithValue("@PassWord", request.Password);
                    sqlCommand.Parameters.AddWithValue("@Role", request.Role);
                    using (DbDataReader dataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (dataReader.HasRows)
                        {
                            response.Message = "Login Successfully";
                        }
                        else
                        {
                            response.isSuccess = false;
                            response.Message = "Login Unsuccessfully";
                            return response;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {

            }

            return response;
        }
    

        public async Task<SignUp.SignUpResponse> SignUp(SignUp.SignUpRequest request)
        {
            SignUpResponse response = new SignUpResponse();
            response.isSuccess = true;
            response.Message = "Successful";
            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                if (!request.Password.Equals(request.ConfirmPassword))
                {
                    response.isSuccess = false;
                    response.Message = "Password & Confirm Password not Match";
                    return response;
                }

                string SqlQuery = @"INSERT INTO crudoperation.userdetail 
                            (Email, PassWord, Role,IsActive) VALUES 
                            (@Email, @PassWord, @Role, @IsActive)";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@Email", request.Email);
                    sqlCommand.Parameters.AddWithValue("@PassWord", request.Password);
                    sqlCommand.Parameters.AddWithValue("@Role", request.Role);
                    sqlCommand.Parameters.AddWithValue("@IsActive", request.Role.Equals("university", StringComparison.OrdinalIgnoreCase) ? 0 : 1);

                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if(Status <= 0)
                    {
                        response.isSuccess = false;
                        response.Message = "Something Went Wrong";
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }

            return response;
        }
    }
}
