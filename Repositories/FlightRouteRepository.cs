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