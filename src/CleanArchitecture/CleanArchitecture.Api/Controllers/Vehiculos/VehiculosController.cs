using Microsoft.AspNetCore.Mvc;
using MediatR;
using CleanArchitecture.Application.Vehiculos.SearchVehiculos;

namespace CleanArchitecture.Api.Controllers.Vehiculos;

[ApiController]
[Route("api/vehiculos")]
public class VehiculosController : ControllerBase 
{
    private readonly ISender _sender;

    public VehiculosController(ISender sender) 
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> SearchVehiculos(
        DateOnly startDate,
        DateOnly endDate,
        CancellationToken cancellationToken
    )
    {
        var query = new SearchVehiculosQuery(startDate, endDate);
        var result = await _sender.Send(query, cancellationToken);
        return Ok(result.Value);
    }
}