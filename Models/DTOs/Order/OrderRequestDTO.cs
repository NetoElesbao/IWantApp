

namespace IWantApp.Models.DTOs.Order
{
    public record OrderRequestDTO
    (
        List<Guid> ProductsIds,
        string DeliveryAddress
    );
} 