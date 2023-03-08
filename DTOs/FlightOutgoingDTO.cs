public class FlightOutgoingDTO{
    public string FlightId { get; set; }
    public DateTime DepartureAt { get; set; }
    public DateTime ArrivalAt { get; set; }
    public int AvailableSeats { get; set; }
    public PriceOutgoingDTO Prices { get; set; }

}