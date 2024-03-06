using CleanArchitecture.Domain.Vehiculos;
using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Alquileres;

public class PrecioService 
{
    public PrecioDetalle CalcularPrecio(Vehiculo vehiculo, DateRange periodo) 
    {
        var tipoMoneda = vehiculo.Precio!.TipoMoneda;
        var monto = periodo.CantidadDias * vehiculo.Precio.Monto;
        var precioPorPeriodo = new Moneda(
            monto,
            tipoMoneda
        );

        decimal porcentageCharge = 0;
        foreach(var accesorio in vehiculo.Accesorios) 
        {
            porcentageCharge += accesorio switch
            {
                Accesorio.AppleCAr or Accesorio.AndroidCard => 0.05m,
                Accesorio.AireAcondicionado => 0.01m,
                Accesorio.Mapas => 0.01m,
                _ => 0m
            };
        }

        var accesorioCharges = Moneda.Zero(tipoMoneda);
        if (porcentageCharge > 0) 
        {
            accesorioCharges = new Moneda(precioPorPeriodo.Monto * porcentageCharge, tipoMoneda);
        }
        var precioTotal = Moneda.Zero();
        precioTotal += precioPorPeriodo;
        
        if (!vehiculo!.Mantenimiento!.IsZero())
        {
            precioTotal += vehiculo.Mantenimiento;
        }

        precioTotal += accesorioCharges;

        return new PrecioDetalle(
            precioPorPeriodo, 
            vehiculo.Mantenimiento, 
            accesorioCharges,
            precioTotal
        );
    }
}