﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ClassManager.Api.Data;
using ClassManager.Api.Data.Entities;
using ClassManager.Shared;
using ClassManager.Shared.DTOs;

namespace ClassManager.Api.Services
{
    public class AuthService
    {
        private readonly TaskContext _context;
        private readonly IConfiguration config;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(TaskContext context, IConfiguration configuration, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            this.config = configuration;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto login)
        {
            var user = await _context.Users
                       .AsNoTracking()
                       .FirstOrDefaultAsync(u => u.Email == login.UserName);
            if (user == null)
            {
                return new AuthResponseDto(default, "Invalid username");
            }

            if (string.IsNullOrEmpty(user.PasswordHash))
            {
                return new AuthResponseDto(default, "Password is not set for this user");
            }

            var passwordResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, login.Password);
            if (passwordResult == PasswordVerificationResult.Failed)
            {
                return new AuthResponseDto(default, "Incorrect password");
            }

            var jwt = GenerateJwtToken(user);
            var loggedInUser = new LoggedInUser(user.Id, user.Name, user.Role, jwt);
            return new AuthResponseDto(loggedInUser);
        }

        public async Task<QuizApiResponse> RegisterAsync(RegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            {
                return QuizApiResponse.Fail("Email already exists... Try logging in");
            }

            var user = new User
            {
                Email = dto.Email,
                Name = dto.Name,
                Phone = dto.Phone,
                Role = nameof(UserRole.Department),
                IsApproved = true  
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, dto.PasswordHash);
            _context.Users.Add(user);

            try
            {
                await _context.SaveChangesAsync();
                return QuizApiResponse.Success();
            }
            catch (Exception ex)
            {
                return QuizApiResponse.Fail(ex.Message);
            }
        }

        private string GenerateJwtToken(User user)
        {
            Claim[] claims =
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var secretKey = config.GetValue<string>("Jwt:Secret") ?? throw new InvalidOperationException("JWT Secret is not set");
            var symmetricKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));
            var signCred = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: config.GetValue<string>("Jwt:Issuer"),
                audience: config.GetValue<string>("Jwt:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(config.GetValue<int>("Jwt:ExpiresInMinutes")),
                signingCredentials: signCred
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return token;
        }
    }
}
