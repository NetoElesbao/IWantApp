


namespace IWantApp.Models.DTOs
{
    public record ProductDTO
    (
        string Name,
        Guid CategoryId,
        string Description,
        bool HasStock
    );
}