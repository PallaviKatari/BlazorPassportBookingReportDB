using PassportBookingReportDB.DTO;
using PassportBookingReportDB.Models;

namespace PassportBookingReportDB.Services
{
    public class BookingService : IBookingService
    {
        private readonly AppDbContext _context;

        public BookingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BookingDto> GetBookingByIdAsync(int bookingId) //1
        {
            // select * from Bookings where bookingId=1;
            var b = await _context.Bookings.FindAsync(bookingId); //1
            if (b == null) return null;

            return new BookingDto
            {
                Id = b.Id,
                BookingCode = b.BookingCode,
                AppointmentDate = b.AppointmentDate,
                OfficeName = b.OfficeName,
                FullName = b.FullName,
                NationalId = b.NationalId,
                Gender = b.Gender,
                Relation = b.Relation,
                BirthDate = b.BirthDate,
                PhoneNumber = b.PhoneNumber
            };
        }
    }
}
