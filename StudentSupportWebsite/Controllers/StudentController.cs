using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentSupportViewModels;
using System.Diagnostics;
using System.Reflection;

namespace StudentSupportWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet("{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            try
            {
                StudentViewModel viewmodel = new() { Email = email };
                await viewmodel.GetByEmail();
                return Ok(viewmodel);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
