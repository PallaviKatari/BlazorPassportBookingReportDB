using PassportBookingReportDB.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public class BookingPdfDocument : IDocument
{
    private readonly BookingsWithLogo _booking;

    public BookingPdfDocument(BookingsWithLogo booking)
    {
        _booking = booking;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(20);
            page.Content().Column(col =>
            {
                col.Item().Text("Booking Details").FontSize(20).Bold();
                col.Item().Text($"Booking Code: {_booking.BookingCode}");
                col.Item().Text($"Full Name: {_booking.FullName}");
                col.Item().Text($"Appointment: {_booking.AppointmentDate:dd-MM-yyyy}");
                col.Item().Text($"Office: {_booking.OfficeName}");
                col.Item().Text($"Phone: {_booking.PhoneNumber}");

                if (_booking.Logo != null && _booking.Logo.Length > 0)
                {
                    col.Item().Image(_booking.Logo);
                }
            });
        });
    }
}
