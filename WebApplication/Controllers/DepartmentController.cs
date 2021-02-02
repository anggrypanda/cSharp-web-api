using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class DepartmentController : ApiController
    {
        public string Post(Department dep)
        {
            try
            {
                string query = @"
                                insert into dbo.Department 
                                values('" + dep.DepartmentName + @"')
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
                            select DepartmentId, DepartmentName 
                            from dbo.Department
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

        public string Put(Department dep)
        {
            try
            {
                string query = @"
                                update dbo.Department 
                                set DepartmentName = '" + dep.DepartmentName + @"' 
                                where DepartmentId = " + dep.DepartmentId + @"
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
                                delete dbo.Department 
                                where DepartmentId = " + id + @"
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
    }
}
