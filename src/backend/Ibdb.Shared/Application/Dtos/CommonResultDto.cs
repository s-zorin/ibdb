namespace Ibdb.Shared.Application.Dtos
{
    public record CommonResultDto(ErrorDto[] Errors);

    public record CommonResultDto<T>(T? Value, ErrorDto[] Errors);
}
