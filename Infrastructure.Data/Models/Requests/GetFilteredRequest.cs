using System.Collections.Generic;

#nullable disable

namespace Infrastructure.Data.Models.Requests
{
    public class GetFilteredRequest
    {
        public int First { get; set; }
        public int Rows { get; set; }
        public string SortField { get; set; }
        public int SortOrder { get; set; }
        public Dictionary<string, List<Filter>> Filters { get; set; }
        public string Language { get; set; }
    }

    public class TheFilter
    {
        public GetFilteredRequest Filters { get; set; }
        public string Language { get; set; }
        public int SelectedLevelOfDetail { get; set; }
    }
}
