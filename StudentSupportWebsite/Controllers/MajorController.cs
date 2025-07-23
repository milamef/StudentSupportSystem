using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentSupportViewModels;
using System.Diagnostics;
using System.Reflection;

namespace StudentSupportWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MajorController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                MajorViewModel viewmodel = new();
                List<MajorViewModel> allMajors = await viewmodel.GetAll();
                return Ok(allMajors);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError); // something went wrong
            }
        }


    }
}
