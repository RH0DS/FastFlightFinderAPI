public class TreavelPlan{

public Guid Id { get; set; }
public String TravelTime { get; set; }

public List<FlightRouteOutgoingDTO> FlightRoutes { get; set; }

}