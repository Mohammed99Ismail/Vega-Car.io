using vega.vscode.Extensions;

namespace vega.Models.Resources
{
    public class VehicleQueryResource:IQueryObject
    {
        public int? MakeId { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAsending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}