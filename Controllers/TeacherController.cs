using System;
using System.Collections.Generic;
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
    public class TeacherController : ControllerBase
    {
        private readonly IConfiguration _Config;
        private readonly MySqlConnection _Connection;
        public TeacherController(IConfiguration config)
        {
            _Config = config;
            _Connection = new MySqlConnection(_Config.GetConnectionString("DefaultConnection"));
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var teachers = await _Connection.QueryAsync<Teacher>("GetTeachers", commandType: System.Data.CommandType.StoredProcedure);
                return Ok(teachers);
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
                var teacher = await _Connection.QueryFirstOrDefaultAsync<Teacher>("GetTeacher", new 
                {
                    _Id = Id
                }, 
                commandType: System.Data.CommandType.StoredProcedure);

                if ( teacher is null)
                    return NotFound("No teacher is found with this id.");

                return Ok(teacher);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Teacher tch)
        {
           try
           {
                await _Connection.QueryAsync("CreateTeacher", new 
                {
                    p_Name = tch.Name, 
                    p_Salary = tch.Salary, 
                    p_PhoneNumber = tch.PhoneNumber
                }, 
                commandType: System.Data.CommandType.StoredProcedure);
                return Ok("Successfully created");
           }
           catch (System.Exception e)
           {
                return BadRequest(e.Message);
           }
        }


        [HttpPost]
        public async Task<ActionResult> Update([FromBody] Teacher tch)
        {
           try
           {
                var teacher = await _Connection.QueryFirstOrDefaultAsync<Students>("GetTeacher", new 
                {
                    _Id = tch.Id
                }, 
                commandType: System.Data.CommandType.StoredProcedure);

                if (teacher is null)
                    return NotFound("No teacher is found with this id.");


                await _Connection.QueryAsync("UpdateTeacher", new 
                {
                    p_id = tch.Id,
                    p_Name = tch.Name, 
                    p_Salary = tch.Salary, 
                    p_PhoneNumber = tch.PhoneNumber
                }, 
                commandType: System.Data.CommandType.StoredProcedure);
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
                var teacher = await _Connection.QueryFirstOrDefaultAsync<Students>("GetTeacher", new 
                {
                    _Id = Id
                }, 
                commandType: System.Data.CommandType.StoredProcedure);

                if (teacher is null)
                    return NotFound("No teacher is found with this id.");
                
                await _Connection.QueryAsync("DeleteTeacher", new 
                {
                    p_id = Id
                },
                commandType: System.Data.CommandType.StoredProcedure);
                return Ok("Successfully deleted");
           }
           catch (System.Exception e)
           {
                return BadRequest(e.Message);
           }
        }

    }
}