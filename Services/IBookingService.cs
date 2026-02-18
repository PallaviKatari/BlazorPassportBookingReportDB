using PassportBookingReportDB.DTO;

namespace PassportBookingReportDB.Services
{
    // Security - Interfaces
    public interface IBookingService
    {
        Task<BookingDto> GetBookingByIdAsync(int bookingId); //1
    }
}
