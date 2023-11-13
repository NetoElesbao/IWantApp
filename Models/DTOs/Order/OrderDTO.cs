

namespace IWantApp.Models.DTOs.Order
{
    public record OrderDTO
    (
        List<Guid> ProductsIds,
        string DeliveryAddress
    );
}