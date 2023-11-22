

namespace IWantApp.Models.DTOs.Order
{
    public record OrderResponseDTO
    (
        Guid Id,
        string ClientEmail,
        List<OrderProduct> Products,
        string DeliveryAddress
    );

    public record OrderProduct
    (
        Guid Id, string Name
    );
}

