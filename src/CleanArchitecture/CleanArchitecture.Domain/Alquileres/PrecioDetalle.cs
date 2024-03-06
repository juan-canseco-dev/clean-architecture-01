using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Alquileres;

public record PrecioDetalle(
    Moneda PrecioPorPeriodo,
    Moneda Mantenimiento,
    Moneda Accessorios,
    Moneda PrecioTotal
);