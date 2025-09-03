using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Infrastructure.Repository;

internal sealed class VehiculoRepository : Repository<Vehiculo,VehiculoId>, IVehiculosRepository
{
    public VehiculoRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}

