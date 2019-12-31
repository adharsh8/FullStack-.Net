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
    public class EmployeeStreamController : ApiController
    {
        [HttpGet]
        [Route("api/GetEmployeeStream")]
        public IHttpActionResult Get()                 //Get All Stream Details
        {
            try
            {

                using (ProjectAllocationDBEntities entity = new ProjectAllocationDBEntities())
                {
                    var details = (from es in entity.EmployeeStreams
                                   join emp in entity.Employees on es.Employee_Id equals emp.Employee_Id
                                   join str in entity.Streams on es.Stream_Id equals str.Stream_Id
                                   select new
                                   {
                                       es.EmployeeStream_Id,
                                       emp.EmployeeName,
                                       emp.EmployeeId,
                                       str.StreamName
                                   }).ToList();
                    return Ok(details.ToList());
 
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteLog(ex);
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("api/PostEmployeeStream")]
        public IHttpActionResult Post([FromBody] EmployeeStream empstream)         //Add new Stream Detail
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                using (ProjectAllocationDBEntities entity = new ProjectAllocationDBEntities())
                {
                    entity.EmployeeStreams.Add(empstream);
                    entity.SaveChanges();
                    return Ok("Employee Stream Added Successfully");
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
