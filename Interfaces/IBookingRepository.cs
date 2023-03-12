public interface IBookingRepository{

Task<bool> CreateBooking(IncommingBookingDTO incommingBooking);

Task<bool> SeatsAvailableAsync(string flightNumber , int numberOfSeats);
}