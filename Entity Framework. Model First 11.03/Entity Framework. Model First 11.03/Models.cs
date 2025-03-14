using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace LibraryApp
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }

    
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string Language { get; set; }

        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
    }


    public class LibraryContext : DbContext
    {
        public LibraryContext() : base("LibraryDB") { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
