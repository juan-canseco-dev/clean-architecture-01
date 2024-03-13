using Microsoft.AspNetCore.Mvc;
using MediatR;
using CleanArchitecture.Application.Alquileres.GetAlquiler;
using CleanArchitecture.Application.Alquileres.ReservarAlquiler;

namespace CleanArchitecture.Api.Controllers.Alquileres;

[ApiController]
[Route("api/alquileres")]
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
        var result = await _sender.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> ReservarAlquiler(
        AlquilerReservarRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AlquilerReservaCommand(
            request.VehiculoId,
            request.UserId,
            request.StartDate,
            request.EndDate
        );

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure) 
        {
            return BadRequest(result.Error);
        }        
        return CreatedAtAction(nameof(GetAlquiler), new {id = result.Value}, result.Value);
    }
}