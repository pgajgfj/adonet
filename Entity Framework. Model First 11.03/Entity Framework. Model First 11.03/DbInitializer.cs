using System;
using System.Linq;

namespace LibraryApp
{
    class DbInitializer
    {
        public static void Seed()
        {
            using (var db = new LibraryContext())
            {
                if (!db.Authors.Any()) 
                {
                    var author1 = new Author { Name = "William Shakespeare" };
                    var author2 = new Author { Name = "Taras Shevchenko" };

                    db.Authors.Add(author1);
                    db.Authors.Add(author2);
                    db.SaveChanges();

                    db.Books.Add(new Book { Title = "Hamlet", PageCount = 200, Language = "English", AuthorId = author1.Id });
                    db.Books.Add(new Book { Title = "Kobzar", PageCount = 150, Language = "Ukrainian", AuthorId = author2.Id });

                    db.SaveChanges();
                }
            }
        }
    }
}
