using Microsoft.EntityFrameworkCore;
public class  FlightRepository : IFlightRepository{


    private readonly DataContext _context;

    public FlightRepository( DataContext context )
    {
        _context = context;    
    }


        public async Task<IEnumerable<Flight>> GetAllFlights()
        {
            return await _context.Flights.ToListAsync();
        }

        public async Task<Flight> GetFlight(string id)
        {
            return await _context.Flights.FindAsync(id);

        }

           public async Task<IEnumerable<FlightRoute>> GetFlightByRoute(string departureDestination, string arrivalDestination, DateTime departureTime)
            //   public async Task<IEnumerable<FlightRoute>> GetFlightByRoute(string departureDestination, string arrivalDestination, DateTime departureTime, DateTime arrivalTime )
        {
        // .Where(flight => flight.ArrivalAt <= arrivalTime && flight.DepartureAt >= departureTime).OrderBy(x =>x.DepartureAt))

        var flightRoutes = 
        await _context.FlightRoutes.Include(flightRoutes => flightRoutes.Itineraries
        .Where(flight => flight.DepartureAt >= departureTime).OrderBy(x =>x.DepartureAt))
   
        .ThenInclude(flightPrice => flightPrice.Prices)
        .Where(flightRoutes => flightRoutes.DepartureDestination.ToUpper().Contains( departureDestination.ToUpper()) 
        && flightRoutes.ArrivalDestination.ToUpper().Contains( arrivalDestination.ToUpper())).ToListAsync();
        

        return flightRoutes;

        }


        

        public async Task<bool> CreateFlight(Flight createFlight)
        {
            throw new NotImplementedException();
        }
    
        public async Task<bool> DeleteFlight(string Id)
        {
        if (await FlightExists(Id)){
            var store = await _context.Flights.FindAsync(Id);
        
            _context.Flights.Remove(store);
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


        public Task<bool> FlightsFound()
        {
            return  _context.Flights.AnyAsync();
        }


        public Task<bool> FlightExists(string id)
        {
            return _context.Flights.AnyAsync(s => s.FlightId == id);
        }

    


}