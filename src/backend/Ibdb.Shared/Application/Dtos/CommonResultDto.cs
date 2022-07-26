namespace Ibdb.Shared.Application.Dtos
{
    public record CommonResultDto<T>(T? Value, ErrorDto[] Errors);
}
