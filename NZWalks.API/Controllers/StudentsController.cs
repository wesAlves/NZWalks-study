using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController
{
    [HttpGet]
    public IActionResult GetAllStudents()
    {
        string[] studentNames = new string[] { "john", "Jaca" };

        return Ok(studentNames);
    }
}