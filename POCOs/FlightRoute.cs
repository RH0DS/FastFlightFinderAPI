public class FlightRoute{

    public string RouteId { get; set; }
    public string DepartureDestination { get; set; }
    public string ArrivalDestination { get; set; }
    public List<Flight> Itineraries { get; set; }

}