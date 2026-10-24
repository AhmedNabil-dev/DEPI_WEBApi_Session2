namespace DEPI_WebApi_Session2.Results
{
    public class PaginatedList<T> 
    {
        private const int MaxPageSize = 50;
        private int _pageSize = 10;

        public int pageNumber { get; set; } = 1;

        public int TotalRecords { get; set; }
        public int TotalPage { get; set; }
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public IEnumerable<T>? Data { get; set; }

    }
}
