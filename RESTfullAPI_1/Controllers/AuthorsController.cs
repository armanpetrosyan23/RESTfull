using Microsoft.AspNetCore.Mvc;
using RESTfullAPI_1.Models;
using RESTfullAPI_1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RESTfullAPI_1.Helpers;
using AutoMapper;


namespace RESTfullAPI_1.Controllers
{
     [Route("api/authors")]
    public class AuthorsController:Controller
    {
        private ILibraryRepository _repository;

        public AuthorsController(ILibraryRepository rep)
        {
            _repository = rep;
        }


        [HttpGet()]
        public IActionResult GetAuthors()
        {
            var authorFromRepo=_repository.GetAuthors();
            var list = Mapper.Map<IEnumerable<AuthorDto>>(authorFromRepo);

            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthor(Guid id)
        {  
                var authorFromRepo = _repository.GetAuthor(id);
                if (authorFromRepo == null)
                {
                    return NotFound();
                }

                var authordto = Mapper.Map<AuthorDto>(authorFromRepo);
                return Ok(authordto);
            
        }


    }
}
 