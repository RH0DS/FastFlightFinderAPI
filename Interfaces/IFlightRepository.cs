public interface IFlightRepository{


     Task <IEnumerable<Flight>> GetAllFlights();
     Task <Flight> GetFlight(string id);
     Task <IEnumerable<FlightRoute>> GetFlightByRoute(string departureDestination, string arrivalDestination, DateTime departureTime);
     Task<bool> CreateFlight(Flight createFlight);

    Task <bool> DeleteFlight(string Id);

     Task<bool> Save();

     Task<bool> FlightsFound();
     Task<bool> FlightExists(string id);


}