using System.Runtime.Serialization;

namespace BooksWebApp
{
    [DataContract(Namespace = "")]
    public class Publisher
    {
        [DataMember(Order = 0, Name = "id")]
        public int PublisherId { get; set; }
        [DataMember(Order = 1, Name = "publisherName")]
        public string PublisherName { get; set; }
    }
}