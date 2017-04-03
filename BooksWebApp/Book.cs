using System.Runtime.Serialization;

namespace BooksWebApp
{
    [DataContract(Namespace = "")]
    public class Book
    {
        [DataMember(Order = 0, Name = "isbn")]
        public string ISBN { get; set; }
        [DataMember(Order = 1, Name = "title")]
        public string Title { get; set; }
        [DataMember(Order = 2, Name = "authorId")]
        public int AuthorId { get; set; }
        [DataMember(Order = 3, Name = "categoryId")]
        public int CategoryId { get; set; }
        [DataMember(Order = 4, Name = "publisherId")]
        public int PublisherId { get; set; }
    }
}