using System;
using DataBaseProject.Business;
using DataBaseProject.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DataBaseProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private DataService DbService = new DataService(Environment.CurrentDirectory + "/DataBaseFiles/student_data.txt");

        // GET: api/Student
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(DbService.GetAllData());
        }

        // GET: api/Student/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(string id)
        {
            try
            {
                return Ok(DbService.FindById(id));
            }
            catch(Exception exception)
            {
                return NotFound(exception);
            }
        }

        // POST: api/Student
        [HttpPost]
        public IActionResult CreateStudent([FromBody]Student newStudent)
        {
            try
            {
                DbService.Save(newStudent);
                return Created("", null);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Student/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]Student editStudent)
        {
            try
            {
                return Ok(DbService.Edit(id, editStudent));
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                DbService.DeleteDataById(id);
                return Ok();
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
