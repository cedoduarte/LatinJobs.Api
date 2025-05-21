namespace LatinJobs.Api.DTOs
{
    public class PaginationParametersDto
    {
        private int _pageSize = 10;

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > 0) ? value : _pageSize;
        }
    }
}
