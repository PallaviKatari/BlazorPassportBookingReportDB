using PassportBookingReportDB.Models;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ZXing;
using ZXing.Common;
using System.Drawing;
using System.Drawing.Imaging;


public class PassportAppointmentPdf
{
    public byte[] Generate(Booking booking) // Booking model from the database - BookingId 1
    {
        QuestPDF.Settings.License = LicenseType.Community;

        FontManager.RegisterFont(File.OpenRead("wwwroot/fonts/Cairo-Regular.ttf"));
        FontManager.RegisterFont(File.OpenRead("wwwroot/fonts/Cairo-Bold.ttf"));

        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);
                page.DefaultTextStyle(x => x.FontFamily("Cairo").FontSize(12));

                page.Content().Column(col =>
                {
                    col.Spacing(10);

                    // HEADER
                    col.Item().AlignCenter().Text("بسم الله الرحمن الرحيم").Bold();
                    col.Item().AlignCenter().Text("وزارة الداخلية");
                    col.Item().AlignCenter().Text("الإدارة العامة للجوازات والهجرة");

                    col.Item().LineHorizontal(1);

                    // BOOKING INFO
                    col.Item().Text($"كود الحجز: {booking.BookingCode}").Bold();
                    col.Item().Text($"رقم الهاتف: {booking.PhoneNumber}");

                    col.Item().AlignCenter()
                        .Text(booking.OfficeName)
                        .FontSize(16).Bold();

                    col.Item().AlignCenter()
    .Text(booking.AppointmentDate.Value.ToString("yyyy/MM/dd"));


                    col.Item().LineHorizontal(1);

                    // PERSON TABLE
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("الاسم").Bold();
                            header.Cell().Text("الرقم الوطني").Bold();
                            header.Cell().Text("الجنس").Bold();
                            header.Cell().Text("صلة القرابة").Bold();
                            header.Cell().Text("تاريخ الميلاد").Bold();
                        });

                        table.Cell().Text(booking.FullName);
                        table.Cell().Text(booking.NationalId);
                        table.Cell().Text(booking.Gender);
                        table.Cell().Text(booking.Relation);
                        table.Cell()
     .Text(booking.BirthDate?.ToString("yyyy-MM-dd") ?? "");

                    });

                    col.Item().LineHorizontal(1);

                    col.Item().AlignCenter()
                        .Image(GenerateBarcode(booking.BookingCode));
                });
            });
        }).GeneratePdf();
    }

    private byte[] GenerateBarcode(string text)
    {
        var writer = new BarcodeWriterPixelData
        {
            Format = BarcodeFormat.CODE_128,
            Options = new EncodingOptions
            {
                Height = 30,
                Width = 150,
                Margin = 0
            }
        };

        var pixelData = writer.Write(text);

        using var bitmap = new Bitmap(pixelData.Width, pixelData.Height,
            PixelFormat.Format32bppRgb);

        var bitmapData = bitmap.LockBits(
            new Rectangle(0, 0, pixelData.Width, pixelData.Height),
            ImageLockMode.WriteOnly,
            PixelFormat.Format32bppRgb);

        System.Runtime.InteropServices.Marshal.Copy(
            pixelData.Pixels, 0, bitmapData.Scan0,
            pixelData.Pixels.Length);

        bitmap.UnlockBits(bitmapData);

        using var ms = new MemoryStream();
        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        return ms.ToArray();
    }
}
