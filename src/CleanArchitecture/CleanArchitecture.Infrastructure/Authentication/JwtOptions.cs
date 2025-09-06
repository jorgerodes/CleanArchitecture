using CleanArchitecture.Application.Abstractions.Authentication;
using CleanArchitecture.Application.Alquileres.GetAlquiler;


public class JwtOptions
{
    public string? Issuer { get; init; }
    public string? Audience { get; init; }
    public string? SecretKey { get; init; }
}