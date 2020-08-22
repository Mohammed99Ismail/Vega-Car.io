using vega.vscode.Extensions;

namespace vega.Models
{
    public class VehicleQuery:IQueryObject
    {
        public int? MakeId { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAsending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}