namespace Ibdb.Shared.Application.Dtos
{
    public record EventDto(Guid Id, Guid EntityId, string Name, int DataVersion, string Data);
}
