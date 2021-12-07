namespace Vega.Web.Models
{
    public class QueryResult<T>{
        public QueryResult()
        {
            Items = default(T);
        }
        public int TotalItems { get; set; }
        public T Items {get;set;}
    }
}