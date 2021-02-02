using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class EmployeeController : ApiController
    {
        public string Post(Employee emp)
        {
            try
            {
                string query = @"
                                insert into dbo.Employee 
                                values('" + emp.EmployeeName + @"', '" + emp.Department + @"', '" + emp.DateOfJoining + @"', '" + emp.PhotoFileName + @"')
                                ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var dAdpt = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    dAdpt.Fill(table);
                }

                return "Successfully added.";
            }
            catch (Exception)
            {
                return "Failed to add.";
            }
        }

        public HttpResponseMessage Get()
        {
            string query = @"
                            select EmployeeId, EmployeeName, Department, 
                            convert(varchar(10), DateOfJoining, 120) as DateOfJoining,
                            PhotoFileName
                            from dbo.Employee
                            ";

            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var dAdpt = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                dAdpt.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Put(Employee emp)
        {
            try
            {
                string query = @"
                                update dbo.Employee 
                                set EmployeeName = '" + emp.EmployeeName + @"', Department = '" + emp.Department + @"', DateOfJoining = '" + emp.DateOfJoining + @"', PhotoFileName = '" + emp.PhotoFileName + @"'
                                where EmployeeId = " + emp.EmployeeId + @"
                                ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var dAdpt = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    dAdpt.Fill(table);
                }

                return "Successfully updated.";
            }
            catch (Exception)
            {
                return "Failed to update.";
            }
        }

        public string Delete(int id)
        {
            try
            {
                string query = @"
                                delete dbo.Employee 
                                where EmployeeId = " + id + @"
                                ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var dAdpt = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    dAdpt.Fill(table);
                }

                return "Successfully deleted.";
            }
            catch (Exception)
            {
                return "Failed to delete.";
            }
        }

        [Route("api/Employee/GetAllDepartmentNames")]
        [HttpGet]
        public HttpResponseMessage GetAllDepartmentNames()
        {
            string query = @"
                            select DepartmentName 
                            from dbo.Department";

            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("api/Employee/SaveFile")]
        [HttpPost]
        public string SaveFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = HttpContext.Current.Server.MapPath("~/Photos/" + fileName);

                postedFile.SaveAs(physicalPath);

                return fileName;
            }

            catch (Exception)
            {
                return "anonymous.png";
            }
        }
    }
}