using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Text.Json.Serialization;
using WebApi2.Models;

namespace WebApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EMPController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public EMPController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetAllEMP")]
        public String GetEmployee()
        {
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("EmployeeAPPCon").ToString());
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM EMP", conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            List<Employee> empList = new List<Employee>();
            Response response = new Response();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Employee emp = new Employee();
                    emp.id = Convert.ToInt32(dt.Rows[i]["EmpId"]);
                    emp.Name = Convert.ToString(dt.Rows[i]["Name"]);
                    emp.Password = Convert.ToString(dt.Rows[i]["Password"]);
                    empList.Add(emp);
                }
            }
            if (empList.Count > 0)
            {
                return JsonConvert.SerializeObject(empList);
            }
            else
            {
              response.StatusCode = 100;
                response.ErrorMsg = "No data found";
                return JsonConvert.SerializeObject(response);

            }
        }
    }
}
