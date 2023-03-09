public class TreavelPlan{

public Guid Id { get; set; }

public string? FligtsId { get; set; }
public TimeSpan TotalTravelTime { get; set; }

public PriceOutgoingDTO TotalPrice { get; set; }
public int AvailableSeats { get; set; }

public string? To { get; set; }
public string? From { get; set; }

public List<string>? AllDestinations { get; set; }

public List<FlightOutgoingDTO>? FlightList { get; set; }

}