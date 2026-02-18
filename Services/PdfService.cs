using Microsoft.EntityFrameworkCore;
using PassportBookingReportDB.Models;

public class PdfService
{
    private readonly AppDbContext _context;

    public PdfService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<byte[]> GenerateBookingPdf(int bookingId) //1
    {
        var booking = await _context.Bookings
            .FirstOrDefaultAsync(x => x.Id == bookingId);

        if (booking == null)
            throw new Exception("Booking not found");

        var report = new PassportAppointmentPdf(); //1
        return report.Generate(booking);
    }
}
