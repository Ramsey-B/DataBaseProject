using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBaseProject.Data;
using DataBaseProject.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataBaseProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        // GET: api/Student
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(DataBaseActions.GetAllData(new Student()));
        }

        // GET: api/Student/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Student
        [HttpPost]
        public IActionResult CreateStudent([FromBody]Student newStudent)
        {
            try
            {
                DataBaseActions.Save(newStudent);
                return Created("", null);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Student/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
