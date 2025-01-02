using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        //GET :https://localhost:portNumber/api/students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            String[] studentNames = new String[] { "john", "jano", "mark", "joy" };

           return  Ok(studentNames);
         }
    }
}
