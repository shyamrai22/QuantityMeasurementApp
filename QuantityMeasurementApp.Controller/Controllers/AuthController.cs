using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementApp.Model.DTO;
using QuantityMeasurementApp.Service;


namespace QuantityMeasurementApp.Controller.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AuthController : ControllerBase
  {
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
      _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
      var result = await _authService.Register(request);

      if (result.Contains("exists"))
        return BadRequest(result);

      return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
      var result = await _authService.Login(request);

      if (result.Contains("Invalid"))
        return BadRequest(result);

      return Ok(new { token = result });
    }

    [HttpPost("google-login")]
    public async Task<IActionResult> GoogleLogin(GoogleLoginRequest request)
    {
      var token = await _authService.GoogleLogin(request.IdToken);

      return Ok(new { token });
    }
  }
}