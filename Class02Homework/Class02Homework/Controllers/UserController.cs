using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Class02Homework.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Class02Homework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<string>> Get()
        {
            return StatusCode(StatusCodes.Status200OK, StaticDb.User);
        }

        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body))
                {
                    string jsonBody = reader.ReadToEnd();
                    User newUser = JsonConvert.DeserializeObject<User>(jsonBody);
                    StaticDb.User.Add(newUser);
                    return StatusCode(StatusCodes.Status201Created);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
