using System.ComponentModel.DataAnnotations.Schema;
using SharedKernel.Common.AbstractClasses;

namespace ReservationService.Data.Entities;

public class Reservation : EntityBase
{
    public Guid ReservationUid { get; set; }
    
    public string Username { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }

    public Guid PaymentUid { get; set; }

    [NotMapped] public Hotel? Hotel { get; set; }
    public Guid? HotelUid { get; set; }
}