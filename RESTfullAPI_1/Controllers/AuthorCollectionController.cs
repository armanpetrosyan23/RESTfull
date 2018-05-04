using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RESTfullAPI_1.Services;
using Microsoft.AspNetCore.Mvc;
using RESTfullAPI_1.Models;
using AutoMapper;
using RESTfullAPI_1.Entities;
using RESTfullAPI_1.Helpers;

namespace RESTfullAPI_1.Controllers
{
    [Route("api/authorcollections")]
    public class AuthorCollectionController : Controller
    {
        ILibraryRepository repo;

        public AuthorCollectionController(ILibraryRepository library)
        {
            repo = library;
        }

        [HttpPost]
        public IActionResult CreateAuthorCollection([FromBody]IEnumerable<AuthorForCreationDto> authorCollection)
        {
            if (authorCollection == null)
            {
                return BadRequest();
            }

            var authorentities = Mapper.Map<IEnumerable<Author>>(authorCollection);

            foreach (var author in authorentities)
            {
                repo.AddAuthor(author);
            }

            if (!repo.Save())
            {
                return NotFound("Internal error");
            }

            var param = string.Join(',', authorentities.Select(x => x.Id.ToString()));   


            return CreatedAtRoute("GetAuthorCollection", new { ids=param},param);
        }


        [HttpGet("({ids})",Name = "GetAuthorCollection")]
        public IActionResult GetAuthorCollection
            ([ModelBinder(binderType:typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids==null)
            {
                return BadRequest();
            }

            var authorEntities = repo.GetAuthors(ids);

            if (ids.Count()!=authorEntities.Count())
            {
                return NotFound();
            }

            var authorforReturn = Mapper.Map<IEnumerable<AuthorDto>>(authorEntities);
            return Ok(authorforReturn);
        }
    }
}