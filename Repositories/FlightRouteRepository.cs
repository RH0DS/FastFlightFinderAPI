using Microsoft.EntityFrameworkCore;
public class  FlightRouteRepository : IFlightRouteRepository{


    
private readonly DataContext _context;

public FlightRouteRepository( DataContext context )
{
       _context = context;    
}


    public async Task<IEnumerable<FlightRoute>> GetAllFlightRoutes()
    {
          return await _context.FlightRoutes.ToListAsync();
    }

    public async Task<IEnumerable<FlightRoute>> GetFlightRouteByDepartureDestination(string departureDestination)
    {

        var flightRoutes = 
        await _context.FlightRoutes.Include(flightRoutes => flightRoutes.Itineraries
        .OrderBy(flight =>flight.DepartureAt))
        .ThenInclude(flight => flight.Prices)
        .Where(flightRoutes => flightRoutes.DepartureDestination.ToUpper().Contains( departureDestination.ToUpper())).ToListAsync();
        

        return flightRoutes;
    }


    public async Task<IEnumerable<FlightRoute>> GetFlightByDate(string departureDestination, string arrivalDestination, DateTime departureTime)
            //   public async Task<IEnumerable<FlightRoute>> GetFlightByRoute(string departureDestination, string arrivalDestination, DateTime departureTime, DateTime arrivalTime )
    {
        // .Where(flight => flight.ArrivalAt <= arrivalTime && flight.DepartureAt >= departureTime).OrderBy(x =>x.DepartureAt))

        var flightRoutes = 
        await _context.FlightRoutes.Include(flightRoutes => flightRoutes.Itineraries
        .Where(flight =>  flight.DepartureAt.Date == departureTime.Date).OrderBy(flight => flight.DepartureAt))
        .ThenInclude(flight => flight.Prices)
        .Where(flightRoutes => flightRoutes.DepartureDestination.ToUpper().Contains( departureDestination.ToUpper()) 
        && flightRoutes.ArrivalDestination.ToUpper().Contains( arrivalDestination.ToUpper())).ToListAsync();
        

        return flightRoutes;

    }










/////////////////////////////////////////////////////////////




    public async Task<IEnumerable<Flight>> GetFlightsWithLayoverAsync(string departureDestination, string arrivalDestination , DateTime departureTime)
{
    // Find all possible layover destinations based on departure and arrival cities
    var possibleLayoverDestinations = await _context.FlightRoutes
        .Where(fr => fr.DepartureDestination == departureDestination) // alla flyg som har avgångsDestination Oslo
        .Join(
            _context.FlightRoutes
            .Where(fr => fr.ArrivalDestination == arrivalDestination), // alla flyg som har ankomstDestination i Amsterdam
            fr => fr.ArrivalDestination,  //från osloListans ankommanstdestinationer
            fr => fr.DepartureDestination, // från Amsterdamlistans AvgångsDestinationer
            (fr1, fr2) => fr1.ArrivalDestination // joinga på alla flyg som börjar i oslo som landar i ett land som har Amsterdamgående avgångsflyg.
        )
        .Distinct()
        .ToListAsync();  //ger en lista med strings med möjliga destinationer som sammanlänkar 2 destinarioner


   var travelPlans = new List<TreavelPlan>();
    
    var LISTAN = new List<FlightRoute>();

   

foreach (var possibleLayover in possibleLayoverDestinations){

        var flightRoutesDepatureDest = 
        await _context.FlightRoutes.Include(flightRoutes => flightRoutes.Itineraries
        .Where(flight => flight.DepartureAt.Date == departureTime.Date).OrderBy(flight =>flight.DepartureAt))
        .ThenInclude(flight => flight.Prices)
        .Where(flightRoutes => flightRoutes.DepartureDestination.ToUpper().Contains( departureDestination.ToUpper()) 
        && flightRoutes.ArrivalDestination == possibleLayover).ToListAsync();


        // var flightRoutesFinalDest = 
        //         await _context.FlightRoutes.Include(flightRoutes => flightRoutes.Itineraries
        //         .Where(flight => flight.DepartureAt >= departureTime).OrderBy(x =>x.DepartureAt))

        //         .ThenInclude(flightPrice => flightPrice.Prices)
        //         .Where(flightRoutes => flightRoutes.DepartureDestination == possibleLayover 
        //         && flightRoutes.ArrivalDestination == arrivalDestination).ToListAsync();


        
}


  

    var flightsWithLayover = new List<Flight>();

    foreach (var layoverDestination in possibleLayoverDestinations)
    {
        // Find all flights that connect the departure city to the layover destination
        var flightsToLayover = await _context.Flights
            .Include(f => f.FlightRoute)
            .Where(f => f.FlightRoute.DepartureDestination == departureDestination
                        && f.FlightRoute.ArrivalDestination == layoverDestination)
            .ToListAsync();
        // Find all flights that connect the layover destination to the arrival city
        var flightsFromLayover = await _context.Flights
            .Include(f => f.FlightRoute)
            .Where(f => f.FlightRoute.DepartureDestination == layoverDestination
                        && f.FlightRoute.ArrivalDestination == arrivalDestination)
            .ToListAsync();
        foreach (var flightToLayover in flightsToLayover)
        {
            foreach (var flightFromLayover in flightsFromLayover)
            {
                var layoverDuration = flightFromLayover.DepartureAt - flightToLayover.ArrivalAt;
                flightsWithLayover.Add(new Flight
                {
                    FlightId = $"{flightToLayover.FlightId}_{flightFromLayover.FlightId}",
                    DepartureAt = flightToLayover.DepartureAt,
                    ArrivalAt = flightFromLayover.ArrivalAt,
                    AvailableSeats = Math.Min(flightToLayover.AvailableSeats, flightFromLayover.AvailableSeats),
                    Prices = flightToLayover.Prices, // assuming the prices are the same for both flights
                    FlightRoute = new FlightRoute
                    {
                        DepartureDestination = flightToLayover.FlightRoute.DepartureDestination,
                        ArrivalDestination = flightFromLayover.FlightRoute.ArrivalDestination,
                        Itineraries = new List<Flight> { flightToLayover, flightFromLayover },
                        // LayoverDestination = layoverDestination,
                        // LayoverDuration = layoverDuration
                    }
                });
            }
        }
    }
    return flightsWithLayover.Take(1);
}







/////////////////////////////////////////////////////////







    public async Task<bool> CreateFlightRoute(FlightRoute createFlightRoute)
    {
        throw new NotImplementedException();
    }
   
    public async Task<bool> DeleteFlightRoute(string routeId)
     {
      if (await FlightRouteExists(routeId)){
        var store = await _context.FlightRoutes.FindAsync(routeId);
    
        _context.FlightRoutes.Remove(store);
        await _context.SaveChangesAsync();
        return true;
      }
    return false;
    }
 
    

        public async Task<bool> Save()
    {
        var saved =await _context.SaveChangesAsync();
        return saved > 0 ? true : false;
    }


    public Task<bool> FlightRoutesFound()
    {
        return  _context.FlightRoutes.AnyAsync();
    }


    public Task<bool> FlightRouteExists(string id)
    {
        return _context.FlightRoutes.AnyAsync(s => s.RouteId == id);
    }


}