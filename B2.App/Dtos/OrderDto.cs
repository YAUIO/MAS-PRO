using System.ComponentModel.DataAnnotations;

namespace B2.App.Dtos;

public class CreateOrderDto : IValidatableObject
{
    [Required]
    public required Guid DeliveryMethodId { get; init; }
    
    [Required]
    [Length(1, int.MaxValue)]
    public required List<PurchaseDto> Purchases { get; init; } = [];
    
    public LocationDto? Location { get; init; }
    
    public Dictionary<Guid, LocationDto>? Pickups { get; init; }


    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Location is null && Pickups is null)
        {
            yield return new ValidationResult("One of two is required", [nameof(Location), nameof(Pickups)]);
        }
    }
}