using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper_Webapi.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Dapper_Webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class StudentController : ControllerBase
    {
        private readonly IConfiguration _Config;
        private readonly MySqlConnection _Connection;
        public StudentController(IConfiguration config)
        {
            _Config = config;
            _Connection = new MySqlConnection(_Config.GetConnectionString("DefaultConnection"));
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                string query = "Select * from students";
                var students = await _Connection.QueryAsync<Students>(query);
                return Ok(students);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Details(int Id)
        {
            try
            {
                string query = "Select * from students Where id = @Id";
                var student = await _Connection.QueryFirstOrDefaultAsync<Students>(query, new {Id});
                if ( student is null)
                    return NotFound("No student is found with this id.");
                return Ok(student);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Students st)
        {
           try
           {
                var query = "insert into students(Name, roll, Phonenumber) values(@Name, @Roll, @PhoneNumber)";
                await _Connection.QueryAsync(query, new {st.Name, st.Roll, st.PhoneNumber});
                return Ok("Successfully created");
           }
           catch (System.Exception e)
           {
                return BadRequest(e.Message);
           }
        }

        [HttpPost]
        public async Task<ActionResult> Update([FromBody] Students st)
        {
           try
           {
                string query1 = "Select * from students Where id = @Id";
                var student = await _Connection.QueryFirstOrDefaultAsync<Students>(query1, new {st.Id});
                if ( student is null)
                    return NotFound("No student found with this id.");


                var query = "update students set Name = @Name, roll = @Roll, phoneNumber = @PhoneNumber Where id = @Id";
                await _Connection.QueryAsync(query, new {st.Name, st.Roll, st.PhoneNumber, st.Id});
                return Ok("Successfully updated");
           }
           catch (System.Exception e)
           {
                return BadRequest(e.Message);
           }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int Id)
        {
           try
           {
                string query1 = "Select * from students Where id = @Id";
                var student = await _Connection.QueryFirstOrDefaultAsync<Students>(query1, new {Id});
                if ( student is null)
                    return NotFound("No student is found with this id.");


                var query = "Delete from students Where id = @Id";
                await _Connection.QueryAsync(query, new {Id});
                return Ok("Successfully deleted");
           }
           catch (System.Exception e)
           {
                return BadRequest(e.Message);
           }
        }
        
    }
}