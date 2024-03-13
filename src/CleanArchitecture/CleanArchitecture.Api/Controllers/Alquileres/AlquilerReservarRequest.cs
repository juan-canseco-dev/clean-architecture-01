namespace CleanArchitecture.Controllers.Alquileres;

public sealed record AlquilerReservarRequest(
    Guid VehiculoId,
    Guid UserId,
    DateOnly StartDate,
    DateOnly EndDate
);