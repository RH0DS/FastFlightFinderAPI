using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]

public class BookingsController : ControllerBase{

    
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public BookingsController( DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    [HttpPost("Booking")] 
    public async Task<ActionResult> CreateAsync( IncommingBookingDTO bookingDTO )
    {
    
        foreach(var flightNumber in bookingDTO.FlightNumbers){
        var seatsAvailable =await _context.Flights.Where(x => x.FlightId == flightNumber).Select(z =>z.AvailableSeats).FirstOrDefaultAsync();
        if(seatsAvailable <bookingDTO.Seats && bookingDTO.Seats >= 1){
            return NotFound("No available seats where found");
        }
        }
        

    return Ok("I guess we booked stuff");
    }




}


