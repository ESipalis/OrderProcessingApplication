using Domain.Order.SpecificOrderData;

namespace WebApi.Models;

public record BookOrderDataDto(string BookName, string AuthorName, int PublishYear);

public static class BookOrderDataDtoExtensions
{
    public static BookOrderData ToDomain(this BookOrderDataDto dto)
    {
        return new BookOrderData(dto.BookName, dto.AuthorName, dto.PublishYear);
    }
}