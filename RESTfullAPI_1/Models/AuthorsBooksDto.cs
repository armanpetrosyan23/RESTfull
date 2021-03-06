﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfullAPI_1.Models
{
    public class AuthorsBooksDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Genre { get; set; }

        public ICollection<BookDto> Books { get; set; }
    }
}
