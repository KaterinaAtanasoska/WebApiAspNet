using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Class02Homework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<string>> Get()
        {
            return StatusCode(StatusCodes.Status200OK, StaticDb.UserNames);
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            try
            {
                if (id < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Id can't be lower than zero");
                }
                if (id > StaticDb.UserNames.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                return StaticDb.UserNames[id];
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }

        }


        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body))
                {
                    string username = reader.ReadToEnd();
                    StaticDb.UserNames.Add(username);
                    return StatusCode(StatusCodes.Status201Created, "User name added");
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body))
                {
                    string idFromBody = reader.ReadToEnd();
                    int id = Int32.Parse(idFromBody);
                    StaticDb.UserNames.RemoveAt(id);
                    return StatusCode(StatusCodes.Status204NoContent, "Username deleted");
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }
    }
}
