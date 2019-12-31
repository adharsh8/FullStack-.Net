using NUnit.Framework;
using ProjectAllotmentHUB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectAllotmentHUB.HelperClass
{
    public class DbOperations
    {
        public void EmployeeList()
        {
            try
            {
                using(ProjectAllocationDBEntities entity = new ProjectAllocationDBEntities())
                {
                    var list1 = entity.Employees.Select(e => e.Employee_Id).ToList();
                    var list2 = entity.EmployeeStreams.Select(f => f.Employee_Id).ToList();

                    IEnumerable<int> list3;
                    list3 = list1.Except(list2);
                }
            }
            catch
            {

            }
        }
        public void EmployeeList1()
        {
            try
            {
                using (ProjectAllocationDBEntities entity = new ProjectAllocationDBEntities())
                {
                  
                }
            }
            catch
            {

            }
        }
    }
}