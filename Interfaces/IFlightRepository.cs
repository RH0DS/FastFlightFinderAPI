public interface IFlightRepository{


     Task <IEnumerable<Flight>> GetAllFlights();
     Task <Flight> GetFlight(string id);
     
     Task<bool> CreateFlight(Flight createFlight);

    Task <bool> DeleteFlight(string Id);

     Task<bool> Save();

     Task<bool> FlightsFound();
     Task<bool> FlightExists(string id);


}