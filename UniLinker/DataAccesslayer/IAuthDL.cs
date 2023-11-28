using UniLinker.Model;
using static UniLinker.Model.SignUp;

namespace UniLinker.DataAccesslayer
{
    public interface IAuthDL
    {
        public Task<SignUpResponse> SignUp(SignUpRequest request);
        public Task<SignInResponse> SignIn(SignInRequest request);

    }
}
