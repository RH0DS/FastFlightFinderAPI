public class SeedModel{

    public string RouteId { get; set; }
    public string DepartureDestination { get; set; }
    public string ArrivalDestination { get; set; }
    public ICollection<Flight> Itineraries { get; set; }
}