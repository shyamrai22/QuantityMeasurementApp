using QuantityMeasurementApp.Model.DTO;

namespace QuantityMeasurementApp.Service
{
  public interface IAuthService
  {
    Task<string> Register(RegisterRequest request);
    Task<string> Login(LoginRequest request);
    Task<string> GoogleLogin(string idToken);
  }
}