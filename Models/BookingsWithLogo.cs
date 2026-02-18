using System;
using System.Collections.Generic;

namespace PassportBookingReportDB.Models;

public partial class BookingsWithLogo
{
    public int Id { get; set; }

    public string? BookingCode { get; set; }

    public DateTime? AppointmentDate { get; set; }

    public string? OfficeName { get; set; }

    public string? FullName { get; set; }

    public string? NationalId { get; set; }

    public string? Gender { get; set; }

    public string? Relation { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string? PhoneNumber { get; set; }

    public byte[]? Logo { get; set; }
}
