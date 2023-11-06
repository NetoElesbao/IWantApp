


namespace IWantApp.Models.DTOs.Product
{
    public record ProductResponseDTO
    (
        string Name,
        string CategoryName,
        string Description,
        bool HasStock,
        decimal Price,
        bool Active
    );
}