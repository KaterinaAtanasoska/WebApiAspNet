using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Class03Homework.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Class03Homework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Book>> Get()
        {
            try
            {
                return StaticDb.Books;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured");
            }
        }

        [HttpGet("{index}")]
        public ActionResult<Book> Get(int index)
        {
            try
            {
                if (index < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad request!");
                }

                if (index >= StaticDb.Books.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Book not found!");
                }

                return StatusCode(StatusCodes.Status200OK, StaticDb.Books[index]);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured");
            }
        }

        [HttpGet("multipleQuery")] 
        public ActionResult<Book> GetByMultipleQueryParams(string title, string author)
        {
            try
            {
                if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(author))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Parameters are missing");
                }

                if (string.IsNullOrEmpty(title))
                {
                    Book book = StaticDb.Books.FirstOrDefault(x => x.Author.Equals(author));
                    if (book == null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "note not found");
                    }

                    return Ok(book);
                }

                if (string.IsNullOrEmpty(author))
                {
                    Book book = StaticDb.Books.FirstOrDefault(x => x.Title.Contains(title));
                    if (book == null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "note not found");
                    }
                    return Ok(book);
                }

                Book bookDb = StaticDb.Books
                    .FirstOrDefault(x => x.Title.Contains(title) && x.Author.Contains(author));
                if (bookDb == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Note not found!");
                }
                return Ok(bookDb);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured");
            }
        }

        [HttpGet("acceptHeader")] 
        public IActionResult GetAcceptHeader([FromHeader(Name = "Accept")] string acceptHeader)
        {
            var request = Request;
            return Ok(acceptHeader);
        }

        [HttpPost("postBook")] 
        public IActionResult postBook([FromBody] Book book)
        {
            try
            {
                StaticDb.Books.Add(book);
                return StatusCode(StatusCodes.Status201Created, "Book added");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured");
            }
        }

        [HttpPost("postList")]
        public IActionResult postList([FromBody] List<Book> booksList)
        {
            try
            {
                StaticDb.Books.AddRange(booksList);
                List<string> bookTitles = StaticDb.Books
                    .Select(x => x.Title)
                    .ToList();
               return StatusCode(StatusCodes.Status201Created, bookTitles);
                
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured");
            }
        }
    }
}
