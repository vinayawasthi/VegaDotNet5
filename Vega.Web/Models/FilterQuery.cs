using System;

namespace Vega.Web.Models
{
    public interface IPagingObject
    {
        int PageIndex { get; set; }
        int PageSize { get; set; }
    }

    public interface ISortObject
    {
        string SortBy { get; set; }
        bool IsSortAscending { get; set; }
    }
    public class BaseFilterQuery : ISortObject, IPagingObject
    {
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
