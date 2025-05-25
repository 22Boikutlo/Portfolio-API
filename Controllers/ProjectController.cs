using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Models;

namespace PortfolioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ProjectController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                SELECT ProjectId, ProjectName, ProjectDescription, ProjectURL, Languages
                FROM dbo.Projects";
            DataTable _table = new DataTable();
            // Retrieve the connection string from the configuration
            string _sqlDataSource = _configuration.GetConnectionString("PortfolioCon");
            SqlDataReader _myReader;
            using (SqlConnection _myCon = new SqlConnection(_sqlDataSource))
            {
                // Open the connection
                _myCon.Open();
                // Create a SqlCommand to execute the query
                using (SqlCommand _myCommand = new SqlCommand(query, _myCon))
                {
                    _myReader = _myCommand.ExecuteReader();
                    _table.Load(_myReader);
                    _myReader.Close();
                    _myCon.Close();
                }

            }
            return new JsonResult(_table);
        }

        [HttpPost]//
        public JsonResult Post(Projects _projects)
        {
            string query = @"
                INSERT INTO dbo.Projects (ProjectName, ProjectDescription, ProjectURL, Languages)
                VALUES (@ProjectName, @ProjectDescription, @ProjectURL, @Languages)";
            DataTable _table = new DataTable();
            // Retrieve the connection string from the configuration
            string _sqlDataSource = _configuration.GetConnectionString("PortfolioCon");
            SqlDataReader _myReader;
            using (SqlConnection _myCon = new SqlConnection(_sqlDataSource))
            {
                // Open the connection
                _myCon.Open();
                // Create a SqlCommand to execute the query
                using (SqlCommand _myCommand = new SqlCommand(query, _myCon))
                {
                    // Add parameters to the comman
                    _myCommand.Parameters.AddWithValue("@ProjectName", _projects.ProjectName);
                    _myCommand.Parameters.AddWithValue("@ProjectDescription", _projects.ProjectDescription);
                    _myCommand.Parameters.AddWithValue("@ProjectURL", _projects.ProjectURL);
                    _myCommand.Parameters.AddWithValue("@Languages", _projects.Languages);

                    // Execute the command
                    _myReader = _myCommand.ExecuteReader();
                    _table.Load(_myReader);
                    _myReader.Close();
                    _myCon.Close();
                }

            }
            return new JsonResult("Added Successfully");
        }
        [HttpPut]
        public JsonResult Put(Projects _projects)
        {
            string query = @"
                UPDATE dbo.Projects
                SET ProjectName = @ProjectName, ProjectDescription = @ProjectDescription, ProjectURL = @ProjectURL, Languages = @Languages
                WHERE ProjectId = @ProjectId";
            DataTable _table = new DataTable();
            // Retrieve the connection string from the configuration
            string _sqlDataSource = _configuration.GetConnectionString("PortfolioCon");
            SqlDataReader _myReader;
            using (SqlConnection _myCon = new SqlConnection(_sqlDataSource))
            {
                // Open the connection
                _myCon.Open();
                // Create a SqlCommand to execute the query
                using (SqlCommand _myCommand = new SqlCommand(query, _myCon))
                {
                    // Add parameters to the command
                    _myCommand.Parameters.AddWithValue("@ProjectName", _projects.ProjectName);
                    _myCommand.Parameters.AddWithValue("@ProjectDescription", _projects.ProjectDescription);
                    _myCommand.Parameters.AddWithValue("@ProjectURL", _projects.ProjectURL);
                    _myCommand.Parameters.AddWithValue("@Languages", _projects.Languages);
                    // Execute the command
                    _myReader = _myCommand.ExecuteReader();
                    _table.Load(_myReader);
                    _myReader.Close();
                    _myCon.Close();
                }
            }
            return new JsonResult("Updated Successfully");
        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                DELETE FROM dbo.Projects
                WHERE ProjectId = @ProjectId";
            DataTable _table = new DataTable();
            // Retrieve the connection string from the configuration
            string _sqlDataSource = _configuration.GetConnectionString("PortfolioCon");
            SqlDataReader _myReader;
            using (SqlConnection _myCon = new SqlConnection(_sqlDataSource))
            {
                // Open the connection
                _myCon.Open();
                // Create a SqlCommand to execute the query
                using (SqlCommand _myCommand = new SqlCommand(query, _myCon))
                {
                    // Add parameters to the command
                    _myCommand.Parameters.AddWithValue("@ProjectId", id);
                    // Execute the command
                    _myReader = _myCommand.ExecuteReader();
                    _table.Load(_myReader);
                    _myReader.Close();
                    _myCon.Close();
                }
            }
            return new JsonResult("Deleted Successfully");
        }
    }
}