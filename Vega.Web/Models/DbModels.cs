namespace Vega.Web.Models
{


    public class Author
    {
        public int Id { get; set; }

        public string Name { get; set; }

        /* EF Relations */
        public ICollection<Blog> Blogs { get; set; }
    }

    public class Blog
    {
        public int Id { get; set; }

        public string Text { get; set; }

        /* EF Relations */
        public ICollection<Author> Authors { get; set; }
    }
}