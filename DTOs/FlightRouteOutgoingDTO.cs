public class FlightRouteOutgoingDTO{

    public string DepartureDestination { get; set; }
    public string ArrivalDestination { get; set; }
    public ICollection<FlightOutgoingDTO> Itineraries { get; set; }
    
 


}