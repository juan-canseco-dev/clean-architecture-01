namespace CleanArchitecture.Domain.Reviews;

public static class ReviewErrors
{
    public static readonly Error NotEligible = new(
        "Review.NotEligible",
        "Este Review y calificacion para el auto no es elegible porque aun no se completa"
    );    
}