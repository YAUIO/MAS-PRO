using System.ComponentModel.DataAnnotations;

namespace B2.App.Dtos;

public class CreateOrderDto
{
    [Required]
    public required Guid DeliveryMethodId { get; init; }
    
    [Required]
    [Length(1, int.MaxValue)]
    public required List<PurchaseDto> Purchases { get; init; } = [];
    
    public required LocationDto Location { get; init; }
}