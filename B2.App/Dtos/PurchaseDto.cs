using System.ComponentModel.DataAnnotations;

namespace B2.App.Dtos;

public class PurchaseDto
{
    [Required]
    public required Guid ListingId { get; init; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public required int Amount { get; init; }
}