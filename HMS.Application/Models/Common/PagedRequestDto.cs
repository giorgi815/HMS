
namespace HMS.Application.Models.Common
{
    public class PagedRequestDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; }
        public bool Ascending { get; set; }
    }
}
