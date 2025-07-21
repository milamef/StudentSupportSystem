﻿using Microsoft.AspNetCore.Http;
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


        [HttpPut]
        public async Task<ActionResult> Put(StudentViewModel viewmodel)
        {
            try
            {
                int retVal = await viewmodel.Update();
                return retVal switch
                {
                    1 => Ok(new { msg = "Student " + viewmodel.Lastname + " updated!" }),
                    -1 => Ok(new { msg = "Student " + viewmodel.Lastname + " not updated!" }),
                    -2 => Ok(new { msg = "Data is stale for " + viewmodel.Lastname + ", Student not updated!" }),
                    _ => Ok(new { msg = "Student " + viewmodel.Lastname + " not updated!" }),
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError); // something went wrong
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                StudentViewModel viewmodel = new();
                List<StudentViewModel> allStudents = await viewmodel.GetAll();
                return Ok(allStudents);
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
