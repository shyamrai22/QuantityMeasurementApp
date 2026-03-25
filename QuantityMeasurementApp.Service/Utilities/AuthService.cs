using Microsoft.EntityFrameworkCore;
using QuantityMeasurementApp.Model.DTO;
using QuantityMeasurementApp.Model.Entity;
using QuantityMeasurementApp.Repository.Data;
using Google.Apis.Auth;

namespace QuantityMeasurementApp.Service
{
  public class AuthService : IAuthService
  {
    private readonly AppDbContext _context;
    private readonly JwtTokenGenerator _tokenGenerator;


    public AuthService(AppDbContext context)
    {
      _context = context;
    }


    public AuthService(AppDbContext context, JwtTokenGenerator tokenGenerator)
    {
      _context = context;
      _tokenGenerator = tokenGenerator;
    }

    public async Task<string> Register(RegisterRequest request)
    {
      var existingUser = await _context.Users
          .FirstOrDefaultAsync(u => u.Email == request.Email);

      if (existingUser != null)
        return "User already exists";

      string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

      var user = new User
      {
        Email = request.Email,
        PasswordHash = hashedPassword
      };

      _context.Users.Add(user);
      await _context.SaveChangesAsync();

      return "User registered successfully";
    }

    public async Task<string> Login(LoginRequest request)
    {
      var user = await _context.Users
          .FirstOrDefaultAsync(u => u.Email == request.Email);

      if (user == null)
        return "Invalid email or password";

      bool isValid = BCrypt.Net.BCrypt.Verify(
          request.Password,
          user.PasswordHash
      );

      if (!isValid)
        return "Invalid email or password";

      // Generate JWT
      var token = _tokenGenerator.GenerateToken(user);

      return token;
    }

    public async Task<string> GoogleLogin(string idToken)
    {
      // 1. Validate token with Google
      var payload = await GoogleJsonWebSignature.ValidateAsync(idToken);

      string email = payload.Email;

      // 2. Check if user exists
      var user = await _context.Users
          .FirstOrDefaultAsync(u => u.Email == email);

      if (user == null)
      {
        // 3. Create new user (no password needed)
        user = new User
        {
          Email = email,
          PasswordHash = "", // Google user
          Role = "User"
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
      }

      // 4. Generate JWT
      var token = _tokenGenerator.GenerateToken(user);

      return token;
    }
  }
}