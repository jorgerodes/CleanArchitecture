using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using Asp.Versioning;
using CleanArchitecture.Api.Utils;
using CleanArchitecture.Application.Alquileres.GetAlquiler;
using CleanArchitecture.Application.Alquileres.ReservarAlquiler;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers.Alquileres;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/Alquileres")]
public class AlquileresController : ControllerBase

{
    private readonly ISender _sender;

    public AlquileresController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAlquiler(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        var query = new GetAlquilerQuery(id);

        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.IsSuccess ? Ok(resultado.Value) : NotFound();
    }
    [HttpPost]
    public async Task<IActionResult> ReservaAlquiler(
        Guid id,
        AlquilerReservaRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new ReservarAlquilerCommand(
            request.VehiculoId,
            request.UserId,
            request.startDate,
            request.endDate
        );

        var resultado = await _sender.Send(command, cancellationToken);

        if (resultado.IsFailure)
        {
            return BadRequest(resultado.Error);
        }

        return CreatedAtAction(nameof(GetAlquiler), new { id = resultado.Value }, resultado.Value);

    }

    
}