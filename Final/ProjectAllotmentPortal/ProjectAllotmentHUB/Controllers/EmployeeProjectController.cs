using ProjectAllotmentHUB.HelperClass;
using ProjectAllotmentHUB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectAllotmentHUB.Controllers
{
    public class EmployeeProjectController : ApiController
    {
        [HttpGet]
        [Route("api/GetEmployeeProject")]
        public IHttpActionResult Get()                 //Get All Project Details
        {
            try
            {
                using (ProjectAllocationDBEntities entity = new ProjectAllocationDBEntities())
                {
                    var listofProjects = entity.EmployeeProjects.Select(e => new
                    {
                       employeeproject_id = e.EmployeeProject_Id,
                       status = e.Status,
                       startdate = e.StartDate,
                       enddate = e.EndDate,
                       roles_id = e.Roles_Id,
                       employeestream_id = e.EmployeeStream_Id,
                       project_id = e.Project_Id
                    });
                    return Ok(listofProjects.ToList());
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteLog(ex);
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("api/PostEmployeeProject")]
        public IHttpActionResult Post([FromBody] EmployeeProject proj)         //Add new EmployeeProject Detail
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                using (ProjectAllocationDBEntities entity = new ProjectAllocationDBEntities())
                {
                    entity.EmployeeProjects.Add(proj);
                    entity.SaveChanges();
                    return Ok("Employee-Project Added Successfully");
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteLog(ex);
                return BadRequest();
            }
        }
    }
}
