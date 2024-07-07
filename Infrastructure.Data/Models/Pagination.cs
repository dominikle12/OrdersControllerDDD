namespace Infrastructure.Data.Models
{
    public class Pagination
    {
        public int First { get; set; }
        public int Rows { get; set; }
        public string SortField { get; set; }
        public int SortOrder { get; set; }
        public Dictionary<string, List<Filter>> Filters { get; set; }
    }


}
