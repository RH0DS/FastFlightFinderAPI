using Microsoft.EntityFrameworkCore;

public class BookingRepository : IBookingRepository{


private readonly DataContext _context;
public BookingRepository(DataContext context)
{
    _context = context;   
}

   public async Task<bool> CreateBooking(IncommingBookingDTO incommingBooking)
    {
        var bookingRequestFlights = await _context.Flights.Where(x => incommingBooking.FlightNumbers.Contains(x.FlightId)).ToListAsync();


        foreach(var flight in bookingRequestFlights){

            flight.AvailableSeats -= incommingBooking.Seats;

            _context.Entry(flight).State = EntityState.Modified;

        }
        
        return await Save()? true : false;
    }

    public async Task<bool> SeatsAvailableAsync(string flightNumber , int numberOfSeats) {
        
       var availableSeats = await _context.Flights.Where(x =>x.FlightId == flightNumber).Select( x => x.AvailableSeats).FirstOrDefaultAsync();

        if (numberOfSeats < availableSeats){

            return true;
        }

        return false;
    }

            public async Task<bool> Save()
        {
            var saved =await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

}