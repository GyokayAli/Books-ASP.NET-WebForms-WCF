using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;

namespace BooksWebApp
{
    [DataContract(Namespace = "")]
    public class Category
    {
        [DataMember(Order = 0, Name = "id")]
        public int CategoryId { get; set; }
        [DataMember(Order = 1, Name = "categoryName")]
        public string CategoryName { get; set; }
    }
}