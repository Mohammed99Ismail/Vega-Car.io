namespace vega.vscode.Extensions
{
    public interface IQueryObject
    {
        string SortBy { get; set; }
        bool IsSortAsending { get; set; }
        int Page { get; set; }
        byte PageSize { get; set; }
    }
}