


namespace IWantApp.Models.DTOs.Product
{
    public record ProductResponseDTO
    (
        string Name,
        string NameCategory,
        string Description,
        decimal Price,
        bool HasStock
    );
}