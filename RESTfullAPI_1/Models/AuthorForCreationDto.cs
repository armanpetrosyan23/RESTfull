using RESTfullAPI_1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfullAPI_1.Models
{
    public class AuthorForCreationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string Genre { get; set; }

        public List<BookForCreationDto> Books { get; set; } 
           = new List<BookForCreationDto>();

    }
}
