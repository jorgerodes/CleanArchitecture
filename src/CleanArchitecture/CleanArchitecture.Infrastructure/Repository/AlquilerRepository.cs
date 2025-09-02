
using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repository;

internal sealed class AlquilerRepository : Repository<Alquiler>, IAlquilerRepository
{

    private static readonly AlquilerStatus[] ActiveAlquilerStatuses = {
        AlquilerStatus.Reservado,
        AlquilerStatus.Confirmado,
        AlquilerStatus.Completado
    };
    public AlquilerRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> IsOverlappingAsync(
        Vehiculo vehiculo,
        DateRange duracion,
        CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Alquiler>()
       .AnyAsync(
           a =>
                a.VehiculoId == vehiculo.Id &&
                a.Duracion!.inicio <= duracion.fin &&
                a.Duracion.fin >= duracion.inicio &&
                ActiveAlquilerStatuses.Contains(a.Status),
            cancellationToken
       );
    }
}

