using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BooksWebApp
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var client = new Service.BooksService();
            var categories = client.GetCategoryList();

            if (!IsPostBack)
            {
                //populate category dropdownlist
                foreach (Category c in categories)
                {
                    var item = new ListItem(c.CategoryName, c.CategoryId.ToString());
                    catDDL.Items.Add(item);
                }

                //populate detailed books table
                booksRepeater.DataSource = client.GetDetailedBookList();
                booksRepeater.DataBind();
            }
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            var searchKeyword = searchTxtBox.Text;
            var client = new Service.BooksService();

            if (!string.IsNullOrWhiteSpace(searchKeyword))
            {
                var bookList = client.GetDetailedBookList();
                List<DetailedBook> resultList = null;

                switch (criteriaDDL.SelectedValue)
                {
                    case "ISBN":
                        resultList = bookList.Where(b => b.ISBN.Contains(searchKeyword)).ToList();
                        break;
                    case "Title":
                        resultList = bookList.Where(b => b.Title.ToLower().Contains(searchKeyword.ToLower())).ToList();
                        break;
                    case "Author":
                        var names = searchKeyword.ToLower().Split(' ').ToList();
                        resultList = bookList
                            .Where(a => names.All(b => (a.Author.FirstName.ToLower() + " " + a.Author.LastName.ToLower()).Split(' ')
                            .Contains(b)))
                            .ToList();
                        break;
                    case "Publisher":
                        resultList = bookList.Where(b => b.Publisher.PublisherName.ToLower().Contains(searchKeyword.ToLower())).ToList();
                        break;
                }

                //check if should filter by category
                if (catDDL.SelectedIndex > 0)
                {
                    var category = catDDL.SelectedItem.Text;
                    booksRepeater.DataSource = resultList
                        .Where(b => b.Category.CategoryName.Equals(category)).ToList();
                }
                else
                    booksRepeater.DataSource = resultList;
            }
            else //default if user input is missing
            {
                //check if should filter by category
                if (catDDL.SelectedIndex > 0)
                {
                    var category = catDDL.SelectedItem.Text;
                    booksRepeater.DataSource = client.GetDetailedBookList()
                                    .Where(b => b.Category.CategoryName.Equals(category)).ToList();
                }
                else
                    booksRepeater.DataSource = client.GetDetailedBookList();
            }

            booksRepeater.DataBind();
        }

        protected void newBookButton_Click(object sender, EventArgs e)
        {
            var isbn = isbnTxtBox.Text;
            var title = titleTxtBox.Text;
            var authorNames = authorTxtBox.Text.Split(' ').ToList();
            var publisher = publisherTxtBox.Text;
            var category = categoryTxtBox.Text;

            if (!string.IsNullOrWhiteSpace(isbn) && !string.IsNullOrWhiteSpace(title) &&
                !string.IsNullOrWhiteSpace(authorTxtBox.Text) && !string.IsNullOrWhiteSpace(publisher) &&
                !string.IsNullOrWhiteSpace(category))
            {
                var client = new Service.BooksService();

                var book = new DetailedBook()
                {
                    ISBN = isbn,
                    Title = title,
                    Author = new Author
                    {
                        FirstName = authorNames.First(),
                        LastName = authorNames.Last()
                    },
                    Publisher = new Publisher
                    {
                        PublisherName = publisher
                    },
                    Category = new Category
                    {
                        CategoryName = category
                    }
                };
                //submit book
                client.AddDetailedBook(book);
                
                //reload table content
                booksRepeater.DataSource = client.GetDetailedBookList();
                booksRepeater.DataBind();
            }
        }
    }
}