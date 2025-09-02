
namespace CleanArchitecture.Domain.Shared;
public record TipoMoneda
{
    public static readonly TipoMoneda Usd = new("USD");
    public static readonly TipoMoneda Eur = new("EUR");
    public static readonly TipoMoneda None = new("");


    private TipoMoneda(string codigo) => Codigo = codigo;
    public string? Codigo { get; init; }

    public static readonly IReadOnlyList<TipoMoneda> All = new[]
    {
        Usd,
        Eur,
    };

    public static TipoMoneda FromCodigo(string codigo)
    {
        return All.FirstOrDefault(c => c.Codigo == codigo) ??
            throw new ApplicationException($"TipoMoneda with codigo '{codigo}' not found.");
    }

}