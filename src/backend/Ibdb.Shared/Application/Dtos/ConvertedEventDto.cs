namespace Ibdb.Shared.Application.Dtos
{
    public record ConvertedEventDto(Guid EntityId, string Name, int DataVersion, string Data);
}
