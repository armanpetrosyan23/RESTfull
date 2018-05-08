using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfullAPI_1.Entities
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
           : base(options)
        {           
            Database.Migrate(); 
           
                       
        }
    

        public  DbSet<Author> Authors { get; set; }
        public  DbSet<Book> Books { get; set; }

    }
}
