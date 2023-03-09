using AutoMapper;
using Microsoft.AspNetCore.Mvc;


namespace FastFlightFinderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightsController : ControllerBase
    {
        
    private readonly IFlightRepository _context;
    private readonly IMapper _mapper;
    public FlightsController( IFlightRepository context, IMapper mapper )
    {
      
        _context = context;
        _mapper = mapper;

    }


      [HttpGet]
    public async Task<ActionResult<IEnumerable<Flight>>> GetFlightsAsync()
    {
        if (_context.FlightExists == null)
        {
            return NotFound();
        }
    var  flight = await _context.GetAllFlights();
        return Ok(flight);
    }



    [HttpGet("{id}")]
    public async Task<ActionResult<Flight>> GetFlightAsync(string id)
    {
        if (_context.FlightExists == null)
        {
            return NotFound();
        }
        var Flight = await _context.GetFlight(id);

        if (Flight == null)
        {
            return NotFound();
        }

        return Flight;
    }



    

    
    [HttpPost] 
    public async Task<ActionResult<Flight>> PostFlightAsync(Flight Flight)
    {

    if (_context.FlightExists == null)
    {
        return Problem("Entity set 'DataContext.Flight'  is null.");
    }
    await _context.CreateFlight(Flight);

    return CreatedAtAction("GetFlight", new { id = Flight.FlightId }, Flight);
    }


    [HttpDelete("{id}")]
    
    public async Task<IActionResult> DeleteProductAsync(string id)
    {
        if (_context.FlightExists == null)
        {
            return NotFound();
        }
        var product = await _context.GetFlight(id);
        if (product == null)
        {
            return NotFound();
        }

        await  _context.DeleteFlight(id);
        

        return NoContent();
    }

  



    }
}