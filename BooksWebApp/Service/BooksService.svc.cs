using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using System.Data;

namespace BooksWebApp.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BookService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select BookService.svc or BookService.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class BooksService : IBooksService
    {
        //Author start
        public List<Author> GetAuthorList()
        {
            var dt = (new DbHelper()).GetResultSet("SELECT id, first_name, last_name FROM Author;");
            var authors = from dr in dt.AsEnumerable()
                          select new Author()
                          {
                              AuthorId = (int)dr["id"],
                              FirstName = dr["first_name"].ToString(),
                              LastName = dr["last_name"].ToString()
                          };
            return authors.ToList();
        }

        public void AddAuthor(Author author)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO Author (first_name, last_name) VALUES ");
            sb.AppendFormat("( '{0}', '{1}');", author.FirstName, author.LastName);

            (new DbHelper()).SqlExecute(sb.ToString());
        }
  
        //Author end

        //Book start
        public List<Book> GetBookList()
        {
            var dt = (new DbHelper()).GetResultSet("SELECT isbn, title, author_id, category_id, publisher_id FROM Book;");
            var books = from dr in dt.AsEnumerable()
                          select new Book()
                          {
                              ISBN = dr["isbn"].ToString(),
                              Title = dr["title"].ToString(),
                              AuthorId = (int)dr["author_id"],
                              CategoryId = (int)dr["category_id"],
                              PublisherId = (int)dr["publisher_id"]
                          };
            return books.ToList();
        }

        public void AddBook(Book book)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO Book (isbn, title, author_id, category_id, publisher_id) VALUES ");
            sb.AppendFormat("( '{0}', '{1}', {2}, {3}, {4});", book.ISBN, book.Title, book.AuthorId, book.CategoryId, book.PublisherId);

            (new DbHelper()).SqlExecute(sb.ToString());
        }

        //Book end

        //DetailedBook start
        public List<DetailedBook> GetDetailedBookList()
        {
            var dt = (new DbHelper()).GetResultSet("SELECT isbn, title, author_id, category_id, publisher_id FROM Book;");
            var detailedBooks = from dr in dt.AsEnumerable()
                        select new DetailedBook()
                        {
                            ISBN = dr["isbn"].ToString(),
                            Title = dr["title"].ToString(),
                            Author = GetAuthorById((int)dr["author_id"]),
                            Category = GetCategoryById((int)dr["category_id"]),
                            Publisher = GetPublisherById((int)dr["publisher_id"])
                        };
            return detailedBooks.ToList();
        }


        public void AddDetailedBook(DetailedBook detailedBook)
        {
            var authorId = GetAuthorIdByName(detailedBook.Author.FirstName, detailedBook.Author.LastName);
            if (authorId == 0)
            {
                AddAuthor(detailedBook.Author);
                authorId = GetAuthorIdByName(detailedBook.Author.FirstName, detailedBook.Author.LastName);
            }

            var publisherId = GetPublisherIdByName(detailedBook.Publisher.PublisherName);
            if (publisherId == 0)
            {
                AddPublisher(detailedBook.Publisher);
                publisherId = GetPublisherIdByName(detailedBook.Publisher.PublisherName);
            }

            var categoryId = GetCategoryIdByName(detailedBook.Category.CategoryName);
            if (categoryId == 0)
            {
                AddCategory(detailedBook.Category);
                categoryId = GetCategoryIdByName(detailedBook.Category.CategoryName);
            }

            var book = new Book()
            {
                ISBN = detailedBook.ISBN,
                Title = detailedBook.Title,
                AuthorId = authorId,
                PublisherId = publisherId,
                CategoryId = categoryId
            };

            AddBook(book);
        }
        //DetailedBook end

        //Category start
        public List<Category> GetCategoryList()
        {
            var dt = (new DbHelper()).GetResultSet("SELECT id, category_name FROM Category;");
            var categories = from dr in dt.AsEnumerable()
                          select new Category()
                          {
                              CategoryId = (int)dr["id"],
                              CategoryName = dr["category_name"].ToString()
                          };
            return categories.ToList();
        }

        public void AddCategory(Category category)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO Category (category_name) VALUES ");
            sb.AppendFormat("( '{0}');", category.CategoryName);

            (new DbHelper()).SqlExecute(sb.ToString());
        }
        //Category end

        //Publisher start
        public List<Publisher> GetPublisherList()
        {
            var dt = (new DbHelper()).GetResultSet("SELECT id, publisher_name FROM Publisher;");
            var publishers = from dr in dt.AsEnumerable()
                             select new Publisher()
                             {
                                 PublisherId = (int)dr["id"],
                                 PublisherName = dr["publisher_name"].ToString()
                             };
            return publishers.ToList();
        }

        public void AddPublisher(Publisher publisher)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO Publisher (publisher_name) VALUES ");
            sb.AppendFormat("( '{0}');", publisher.PublisherName);

            (new DbHelper()).SqlExecute(sb.ToString());
        }
        //Publisher end

        /** Service helper methods **/
        public static Author GetAuthorById(int authorId)
        {
            var dt = (new DbHelper()).GetResultSet("SELECT id, first_name, last_name FROM Author WHERE id = " + authorId);
            if (dt.Rows.Count > 0)
            {
                var dr = dt.Rows[0];
                return new Author()
                {
                    AuthorId = (int)dr["id"],
                    FirstName = dr["first_name"].ToString(),
                    LastName = dr["last_name"].ToString()
                };
            }
            return null;
        }

        public static Category GetCategoryById(int categoryId)
        {
            var dt = (new DbHelper()).GetResultSet("SELECT id, category_name FROM Category WHERE id = " + categoryId);
            if (dt.Rows.Count > 0)
            {
                var dr = dt.Rows[0];
                return new Category()
                {
                    CategoryId = (int)dr["id"],
                    CategoryName = dr["category_name"].ToString()
                };
            }
            return null;
        }

        public static Publisher GetPublisherById(int publisherId)
        {
            var dt = (new DbHelper()).GetResultSet("SELECT id, publisher_name FROM Publisher WHERE id = " + publisherId);
            if (dt.Rows.Count > 0)
            {
                var dr = dt.Rows[0];
                return new Publisher()
                {
                    PublisherId = (int)dr["id"],
                    PublisherName = dr["publisher_name"].ToString()
                };
            }
            return null;
        }

        public static int GetAuthorIdByName(string firstName, string lastName)
        {
            var dt = (new DbHelper())
                .GetResultSet("SELECT id FROM Author WHERE first_name = '" + firstName + "' AND last_name =  '" + lastName + "'");
            if (dt.Rows.Count > 0)
            {
                var dr = dt.Rows[0];
                return (int)dr["id"];
            }
            return 0;
        }

        public static int GetCategoryIdByName(string categoryName)
        {
            var dt = (new DbHelper())
                .GetResultSet("SELECT id FROM Category WHERE category_name = '" + categoryName + "'");
            if (dt.Rows.Count > 0)
            {
                var dr = dt.Rows[0];
                return (int)dr["id"];
            }
            return 0;
        }

        public static int GetPublisherIdByName(string publisherName)
        {
            var dt = (new DbHelper())
                .GetResultSet("SELECT id FROM Publisher WHERE publisher_name = '" + publisherName + "'");
            if (dt.Rows.Count > 0)
            {
                var dr = dt.Rows[0];
                return (int)dr["id"];
            }
            return 0;
        }

        /** end **/
    }
}
