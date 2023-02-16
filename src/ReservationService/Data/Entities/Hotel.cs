using System.ComponentModel.DataAnnotations.Schema;
using SharedKernel.Common.AbstractClasses;

namespace ReservationService.Data.Entities;

public class Hotel : EntityBase
{
    public Guid HotelUid { get; set; }
    
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int? Stars { get; set; }
    public int Price { get; set; }

    [NotMapped] public string FullAddress => $"{Country}, {City}, {Address}";

    public List<Reservation>? Reservations { get; set; }
}