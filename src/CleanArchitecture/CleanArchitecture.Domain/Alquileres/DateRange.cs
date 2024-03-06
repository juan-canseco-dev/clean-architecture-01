namespace CleanArchitecture.Domain.Alquileres;

public sealed record DateRange
{
    public DateOnly Inicio {get; init;}
    public DateOnly Final {get; init;}
    public int CantidadDias => Final.DayNumber - Inicio.DayNumber;
    
    public static DateRange Create(DateOnly inicio, DateOnly final)
    {
        if (inicio > final)
        {
            throw new ApplicationException("La fecha final es anterior a la fecha de inicio");
        }
        return new DateRange(inicio, final);
    }
    
    private DateRange(DateOnly inicio, DateOnly final) 
    {
        Inicio = inicio;
        Final = final;
    }
}