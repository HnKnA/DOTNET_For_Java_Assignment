﻿using ClassManager.Api.Services;
using ClassManager.Shared.DTOs;

namespace ClassManager.Api.Endpoints
{
    public static class AuthEndpoints
    {
        public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/auth/login", async(LoginDto dto, AuthService authService) =>
                       Results.Ok(await authService.LoginAsync(dto)));

            app.MapPost("/api/auth/register", async (RegisterDto dto, AuthService authService) =>
            Results.Ok(await authService.RegisterAsync(dto)));


            return app;
        }
    }
}
