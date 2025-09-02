using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.Alquileres.GetAlquiler;

public sealed record GetAlquilerQuery(Guid AlquilerID) : IQuery<AlquilerResponse>;
