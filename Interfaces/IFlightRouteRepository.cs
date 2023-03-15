public interface IFlightRouteRepository{


     Task <IEnumerable<FlightRoute>> GetAllFlightRoutes();
     Task<IEnumerable<FlightRoute>> GetFlightRouteByDepartureDestination(string departureDestination);
     
     Task<IEnumerable<TravelPlan>> GetFlightsByDate(string departureDestination, string arrivalDestination, DateTime departureTime);
     
     Task<bool> CreateFlightRoute(FlightRoute createFlightRoute);

    Task <bool> DeleteFlightRoute(string routeId);

     Task<bool> Save();

     Task<bool> FlightRoutesFound();
     Task<bool> FlightRouteExists(string id);



     Task<IEnumerable<TravelPlan>> GetFlightsWithLayoverAsync(string departureDestination, string arrivalDestination, DateTime departureTime);


}