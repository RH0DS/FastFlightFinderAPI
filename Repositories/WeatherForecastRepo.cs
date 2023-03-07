using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text.Json;
public class WeatherForecastRepo : IWeatherForecastRepo{

private readonly DataContext _context;
public WeatherForecastRepo( DataContext context )
{
    _context = context;    
}

   public  async Task <bool> Seedify()
    {
   var fileLocation = "./Repositories/SeedData.Json";
    var jsonData = await File.ReadAllTextAsync(fileLocation);
    var flightRoutes = JsonSerializer.Deserialize<List<FlightRoute>>(jsonData);
  

    foreach (var route in  flightRoutes){
        
        Console.WriteLine(route);
        foreach(var flight in route.Itineraries){

            Console.WriteLine(flight.FlightId);
        }
          
    }

        // Loop through the list of FlightRoute objects and add them to the _Context
    // foreach (var route in flightRoutes)
    // {
    //     Console.WriteLine(route);
    //      _context.FlightRoutes.Add(route.RouteId);
    //     // Loop through the list of Flight objects and add them to the _Context
    //     // foreach (var flight in route.Itineraries)
    //     // {
    //     //     flight.FlightRoute = route;
    //     //     // _context.Flights.Add(flight);

    //     //     // Add the Price object to the _Context
    //     //     // _context.Prices.Add(flight.Prices);
    //     // }
       
    // }

    // // Save changes to the database
    // await Save();

    return true;
    }

    public async Task<IEnumerable<FlightRoute>> GetAllFlightRoutes()
    {
          return await _context.FlightRoutes.ToListAsync();
    }

    public async Task<FlightRoute> GetFlightRoute(string id)
    {
        return await _context.FlightRoutes.FindAsync(id);

    }

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