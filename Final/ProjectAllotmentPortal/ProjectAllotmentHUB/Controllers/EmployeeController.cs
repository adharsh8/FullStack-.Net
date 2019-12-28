using ProjectAllotmentHUB.HelperClass;
using ProjectAllotmentHUB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ProjectAllotmentHUB.Controllers
{
    public class EmployeeController : ApiController
    {
        [HttpGet]
        [Route("api/GetEmployee")]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult Get()                 //Get All Employee Details
        {
            try
            {
                using (ProjectAllocationDBEntities entity = new ProjectAllocationDBEntities())
                {
                    var listofEmployees = entity.Employees.Select(e => new
                    {
                        employee_Id = e.Employee_Id,
                        employeename = e.EmployeeName,
                        employeeId = e.EmployeeId,
                        employeePhno = e.EmployeePhno,
                        employeemailId = e.EmployeeMailId,
                        doj = e.DOJ
                    });
                    return Ok(listofEmployees.ToList());
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteLog(ex);
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("api/PostEmployee")]
        public IHttpActionResult Post([FromBody] Employee employee)         //Add new Employee Detail
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                using (ProjectAllocationDBEntities entity = new ProjectAllocationDBEntities())
                {
                    entity.Employees.Add(employee);
                    entity.SaveChanges();
                    return Ok("Employee Added Successfully");
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteLog(ex);
                return BadRequest();
            }
        }
        [HttpPut]
        [Route("api/PutEmployee/{id=id}")]
        public IHttpActionResult Put(int id, [FromBody] Employee employee)    //Modify PhoneNo, MailId of Employee
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                using (ProjectAllocationDBEntities entity = new ProjectAllocationDBEntities())
                {
                    var emp = entity.Employees.FirstOrDefault(e => e.Employee_Id == id);

                    if (emp == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        emp.EmployeePhno = employee.EmployeePhno;
                        emp.EmployeeMailId = employee.EmployeeMailId;
                        entity.SaveChanges();

                        return Ok("Employee Details has Updated");
                    }
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
