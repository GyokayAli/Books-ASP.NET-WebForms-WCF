using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace BooksWebApp.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBookService" in both code and config file together.
    [ServiceContract]
    public interface IBooksService
    {
        //Author start
        [OperationContract()]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Authors")]
        List<Author> GetAuthorList();

        [OperationContract()]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddAuthor")]
        void AddAuthor(Author author);
        //Author end

        //Book start
        [OperationContract()]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Books")]
        List<Book> GetBookList();
        //Book end

        //DetailedBook start
        [OperationContract()]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "DetailedBooks")]
        List<DetailedBook> GetDetailedBookList();

        [OperationContract()]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddDetailedBook")]
        void AddDetailedBook(DetailedBook book);
        //DetailedBook end

        //Category start
        [OperationContract()]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Categories")]
        List<Category> GetCategoryList();

        [OperationContract()]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddCategory")]
        void AddCategory(Category category);
        //Category end

        //Publisher start
        [OperationContract()]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Publishers")]
        List<Publisher> GetPublisherList();

        [OperationContract()]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddPublisher")]
        void AddPublisher(Publisher publisher);
        //Publisher end
    }
}
