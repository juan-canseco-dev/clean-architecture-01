namespace CleanArchitecture.Domain.Vehiculos;

public record Moneda(decimal Monto, TipoMoneda TipoMoneda) 
{
    public static Moneda operator +(Moneda primer, Moneda segundo) {
        if (primer.TipoMoneda != segundo.TipoMoneda) {
            throw new InvalidOperationException("El tipo de moneda debe ser el mismo.");
        }
        return new Moneda(primer.Monto + segundo.Monto, primer.TipoMoneda);
    }
    public static Moneda Zero() => new(0, TipoMoneda.None);
    public static Moneda Zero(TipoMoneda tipoMoneda) => new (0, tipoMoneda);
    public bool IsZero() => this == Zero(TipoMoneda);
}

