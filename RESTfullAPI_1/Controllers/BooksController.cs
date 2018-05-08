using Microsoft.AspNetCore.Mvc;
using RESTfullAPI_1.Models;
using RESTfullAPI_1.Services;
using System;
using AutoMapper;
using RESTfullAPI_1.Entities;

namespace RESTfullAPI_1.Controllers
{
    [Route("api/author/{authorid}/books")]
    public class BooksController:Controller
    {
        private ILibraryRepository _repository;


        public BooksController(ILibraryRepository rep)
        {
            _repository = rep;
        }

        [HttpGet]
        public IActionResult GetBooksForAuthor(Guid authorid)
        {
            var authorFromRepo = _repository.GetBooksForAuthor(authorid);

            if (authorFromRepo == null)
            {
                return NotFound();
            }

            return Ok(authorFromRepo);

        }
    

        [HttpGet("{id}",Name ="GetBookForAuthor")]
        public IActionResult GetBookForAuthor(Guid authorid, Guid id)
        {
            var authorFromRepo = _repository.GetBookForAuthor(authorid, id);

            if (authorFromRepo == null)
            {
                return NotFound();
            }

            return Ok(authorFromRepo);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBookForAuthor(Guid authorid, Guid id)
        {
            if (!_repository.AuthorExists(authorid)) 
            {
                return NotFound();
            }

            var bookforauthor = _repository.GetBookForAuthor(authorid, id);
            if (bookforauthor==null)
            {
                return NotFound();
            }
            else
            {
                _repository.DeleteBook(bookforauthor);
                _repository.Save();
                return NoContent();
            }

        }
        
        [HttpPost]
        public IActionResult CreateBookForAuthor(Guid authorid,
            [FromBody] BookForCreationDto book)
        {
            if (book==null)
            {
                return BadRequest("Error body");
            }

            if (!_repository.AuthorExists(authorid))
            {
                return NotFound("Author non exist");
            }
            Book b = Mapper.Map<Book>(book);
            _repository.AddBookForAuthor(authorid, b);

            if (!_repository.Save())
            {
                return NotFound("Internal error");
            }

            BookDto dto = Mapper.Map<BookDto>(b);
            CreatedAtRouteResult resault=CreatedAtRoute("GetBooksForAuthor", new {authorid , id = b.Id },dto);

            return resault;
        }
    }
}
 