namespace Citacoes.Api.Domain.Pagination
{
    public class PaginationParameters
    {
        const int MAXPAGESIZE = 20;
        private int _pageSize = 10;

        public string? SearchQuery { get; set; }
        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set
            {
                _pageSize = value > MAXPAGESIZE ? MAXPAGESIZE : value;
            }
        }
    }
}