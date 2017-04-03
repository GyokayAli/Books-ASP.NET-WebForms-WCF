using System.Runtime.Serialization;
using System.Collections.Generic;

namespace BooksWebApp
{
    [DataContract(Namespace = "")]
    public class Author
    {
        [DataMember(Order = 0, Name = "id")]
        public int AuthorId { get; set; }

        [DataMember(Order = 1, Name = "firstName")]
        public string FirstName { get; set; }

        [DataMember(Order = 2, Name = "lastName")]
        public string LastName { get; set; }
    }
}