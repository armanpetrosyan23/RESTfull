using Microsoft.AspNetCore.Mvc;
using RESTfullAPI_1.Models;
using RESTfullAPI_1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RESTfullAPI_1.Helpers;
using AutoMapper;
using RESTfullAPI_1.Entities;
using Microsoft.AspNetCore.Rewrite.Internal;

namespace RESTfullAPI_1.Controllers
{
    [Route("api/authors")]
    public class AuthorsController : Controller
    {
        private ILibraryRepository _repository;

        public AuthorsController(ILibraryRepository rep)
        {
            _repository = rep;
        }


        [HttpGet()]
        public IActionResult GetAuthors()
        {
            var authorFromRepo = _repository.GetAuthors();
            var list = Mapper.Map<IEnumerable<AuthorDto>>(authorFromRepo);

            return new JsonResult(list);
        }

        [HttpGet("{id}", Name = "GetAuthor")]
        public IActionResult GetAuthor(Guid id)
        {
            var authorFromRepo = _repository.GetAuthor(id);
            if (authorFromRepo == null)
            {
                return NotFound("Author not found");
            }

            var authordto = Mapper.Map<AuthorDto>(authorFromRepo);

            return new JsonResult(authordto);

        }
       

        [HttpPost]
        public IActionResult CreateAuthor([FromBody]AuthorForCreationDto author)
        {
            if (author == null)
            {
                return BadRequest();
            }

            Author a = Mapper.Map<Author>(author);
            
            _repository.AddAuthor(a);


            if (!_repository.Save())
            {
                return StatusCode(500, "A problem happend with handinq your request");
            }

            //  var mapaut = Mapper.Map<AuthorDto>(aut);

            AuthorDto authorforreturn = Mapper.Map<AuthorDto>(a);
            return CreatedAtRoute("GetAuthor",new { id=authorforreturn.Id},authorforreturn);

        }
        [HttpPost("{id}")]
        public IActionResult BlockAuthorCollection(Guid id)
        {
            if (_repository.AuthorExists(id))
            {
                return new StatusCodeResult(409);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(Guid id)
        {
            var author = _repository.GetAuthor(id);
            if (author==null)
            {
                return NotFound();
            }
            _repository.DeleteAuthor(author);
            if (!_repository.Save())
            {
                throw new Exception("Not found author");
            }

            return Ok();
        }
    }
}
