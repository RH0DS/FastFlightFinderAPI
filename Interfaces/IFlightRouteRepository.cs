public interface IFlightRouteRepository{


     Task <IEnumerable<FlightRoute>> GetAllFlightRoutes();
     Task <FlightRoute> GetFlightRoute(string id);
     Task<bool> CreateFlightRoute(FlightRoute createFlightRoute);

    Task <bool> DeleteFlightRoute(string routeId);

     Task<bool> Save();

     Task<bool> FlightRoutesFound();
     Task<bool> FlightRouteExists(string id);


}