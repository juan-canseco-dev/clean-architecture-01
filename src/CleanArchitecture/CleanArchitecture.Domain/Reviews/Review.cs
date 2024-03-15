using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Reviews.Events;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Domain.Reviews;

public sealed class Review : Entity<ReviewId>
{
    public VehiculoId VehiculoId { get; private set; }
    public AlquilerId AlquilerId { get; private set; }
    public UserId UserId {get; private set;}
    public Rating? Rating {get; private set;}
    public Comentario? Comentario {get; private set;}
    public DateTime FechaCreacion {get; private set;}
    
    public static Result<Review> Create(
        Alquiler alquiler, 
        Rating rating, 
        Comentario comentario, 
        DateTime fechaCreacion) 
    {
        if (alquiler.Status != AlquilerStatus.Completado) 
        {
            return Result.Failure<Review>(ReviewErrors.NotEligible);
        }
        var review = new Review(
            ReviewId.New(),
            alquiler.VehiculoId,
            alquiler.Id,
            alquiler.UserId,
            rating,
            comentario,
            fechaCreacion
        );

        review.RaiseDomainEvent(new ReviewCreatedDomainEvent(review.Id));

        return review;
    }

    private Review() : base() 
    {
        VehiculoId = default!;
        AlquilerId = default!;
        UserId = default!;
    }

    private Review(
        ReviewId id,
        VehiculoId vehiculoId,
        AlquilerId alquilerId,
        UserId userId,
        Rating rating,
        Comentario cometario,
        DateTime fechaCreacion
    ) : base(id) 
    {
        VehiculoId = vehiculoId;
        AlquilerId = alquilerId;
        UserId = userId;
        Rating = rating;
        Comentario = cometario;
        FechaCreacion = fechaCreacion;
    }
}