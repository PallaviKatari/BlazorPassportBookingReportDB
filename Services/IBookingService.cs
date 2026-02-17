using PassportBookingReportDB.DTO;

namespace PassportBookingReportDB.Services
{
    public interface IBookingService
    {
        Task<BookingDto> GetBookingByIdAsync(int bookingId);
    }
}
