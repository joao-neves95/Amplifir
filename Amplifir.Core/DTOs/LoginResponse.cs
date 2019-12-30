namespace Amplifir.Core.DTOs
{
    public class LoginResponse
    {
        public LoginResponse()
        {
        }

        public LoginResponse(string jwt)
        {
            this.JWT = jwt;
        }

        public string JWT { get; set; }
    }
}
