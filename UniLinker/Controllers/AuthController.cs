using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniLinker.DataAccesslayer;
using UniLinker.Model;
using static UniLinker.Model.SignUp;

namespace UniLinker.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IAuthDL _authDL;
        public AuthController(IAuthDL authDL)
        {
            _authDL = authDL;
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpRequest request)
        {
            SignUpResponse response = new SignUpResponse();
            try
            {
                response = await _authDL.SignUp(request);

            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.Message = ex.Message;

            }
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInRequest request)

        {
            SignInResponse response = new SignInResponse();
            try
            {
                response = await _authDL.SignIn(request);
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.Message = ex.Message;

            }
            return Ok(response);
        }
    }
}
