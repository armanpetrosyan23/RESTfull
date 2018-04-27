using Microsoft.AspNetCore.Mvc;
using RESTfullAPI_1.Models;
using RESTfullAPI_1.Services;
using System;
using AutoMapper;


namespace RESTfullAPI_1.Controllers
{
    [Route("api/[Controller]/{authorid}/books")]
    public class BooksController:Controller
    {
        private ILibraryRepository _repository;


        public BooksController(ILibraryRepository rep)
        {
            _repository = rep;
        }

        [HttpGet()]
        public IActionResult GetBooksForAuthor(Guid authorid)
        {
            var authorFromRepo = _repository.GetBooksForAuthor(authorid);
            if (authorFromRepo == null)
            {
                return NotFound();
            }

            return Ok(authorFromRepo);

        }
    }
}
