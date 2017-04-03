using System.Runtime.Serialization;

namespace BooksWebApp
{
    [DataContract(Namespace = "")]
    public class DetailedBook
    {
        [DataMember(Order = 0, Name = "isbn")]
        public string ISBN { get; set; }

        [DataMember(Order = 1, Name = "title")]
        public string Title { get; set; }

        [DataMember(Order = 2, Name = "author")]
        public Author Author { get; set; }

        [DataMember(Order = 3, Name = "category")]
        public Category Category { get; set; }

        [DataMember(Order = 4, Name = "publisher")]
        public Publisher Publisher { get; set; }
    }
}