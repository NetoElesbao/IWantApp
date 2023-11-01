


namespace IWantApp.Models.DTOs.Product
{
    public record ProductRequestDTO
    (
        string Name,
        Guid CategoryId,
        string Description,
        decimal Price,
        bool HasStock
    );
}