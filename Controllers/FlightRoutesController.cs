using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace FastFlightFinderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightRoutesController : ControllerBase
    {
        
    private readonly IFlightRouteRepository _context;
    private readonly IMapper _mapper;

    public FlightRoutesController( IFlightRouteRepository context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


      [HttpGet]
    public async Task<ActionResult<IEnumerable<FlightRoute>>> GetFlightRoutesAsync()
    {
        if (_context.FlightRouteExists == null)
        {
            return NotFound();
        }
    var  flightRoutes = await _context.GetAllFlightRoutes();
        return Ok(flightRoutes);
    }



    [HttpGet("ByDepartureDestination")]
    public async Task<ActionResult<IEnumerable<FlightRoute>>> GetFlightRouteByDepartureDestinationAsync(string departureDestinationd )
    {
        if (_context.FlightRouteExists == null)
        {
            return NotFound();
        }
        var flightRoutes = await _context.GetFlightRouteByDepartureDestination(departureDestinationd);
         var response = _mapper.Map<IEnumerable<FlightRouteOutgoingDTO>> (flightRoutes);

        if (flightRoutes == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("ByDate")]
    public async Task<ActionResult<IEnumerable<FlightOutgoingDTO>>> GetFlightByDateAsync(string departureDestination, string arrivalDestination, DateTime departureTime)
    {
        if (_context.FlightRouteExists == null)
        {
            return NotFound();
        }
        var flightRoutes = await _context.GetFlightByDate(departureDestination, arrivalDestination, departureTime);

        var response = _mapper.Map<IEnumerable<FlightRouteOutgoingDTO>> (flightRoutes);


        if (flightRoutes == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

   [HttpGet("ByDateWithLayover")]
    public async Task<ActionResult<IEnumerable<Flight>>> GetFlightsWithLayoverAsync(string departureDestination, string arrivalDestination, DateTime departureTime)
    {
        if (_context.FlightRouteExists == null)
        {
            return NotFound();
        }
        var flights = await _context.GetFlightsWithLayoverAsync( departureDestination, arrivalDestination, departureTime);

       // var response = _mapper.Map<IEnumerable<FlightRouteOutgoingDTO>> (flightRoutes);


        if (flights == null)
        {
            return NotFound();
        }

        return Ok(flights);
    }

    

    
    [HttpPost("nogot")] 
    public async Task<ActionResult<FlightRoute>> PostFlightRouteAsync(FlightRoute FlightRoute)
    {

    
    if (_context.FlightRouteExists == null)
    {
        return Problem("Entity set 'DataContext.FlightRoute'  is null.");
    }
    await _context.CreateFlightRoute(FlightRoute);

    return CreatedAtAction("GetFlightRoute", new { id = FlightRoute.RouteId }, FlightRoute);
    }
    [HttpDelete("{id}")]
    
    public async Task<IActionResult> DeleteProductAsync(string id)
    {
        if (_context.FlightRouteExists == null)
        {
            return NotFound();
        }
        // var product = await _context.GetFlightRoute(id);
        // if (product == null)
        // {
        //     return NotFound();
        // }

        await  _context.DeleteFlightRoute(id);
        

        return NoContent();
    }

  



    }
}