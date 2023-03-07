using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Flight{
    [Key]
    public string FlightId { get; set; }
    public string DepartureAt { get; set; }
    public string ArrivalAt { get; set; }
    public int AvailableSeats { get; set; }
    public Price Prices { get; set; }
    
        [ForeignKey ("FlightRoute")]
    public string  RouteId { get; set; }
    public virtual FlightRoute FlightRoute { get; set; }

   
}