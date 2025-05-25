using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Models;

namespace PortfolioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public EducationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                SELECT EducationId, SchoolName, CourseName, Subjects, Year
                FROM dbo.Education";
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
        public JsonResult Post(Education _education)
        {
            string query = @"
                INSERT INTO dbo.Education (SchoolName, Course, Subjects, Year)
                VALUES (@SchoolName, @Course, @Subjects, @Year)";
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
                    _myCommand.Parameters.AddWithValue("@SchoolName", _education.SchoolName);
                    _myCommand.Parameters.AddWithValue("@Course", _education.Course);
                    _myCommand.Parameters.AddWithValue("@Subjects", _education.Subjects);
                    _myCommand.Parameters.AddWithValue("@Year", _education.Year);

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
        public JsonResult Put(Education _education)
        {
            string query = @"
                UPDATE dbo.Education
                SET SchoolName = @SchoolName, Course = @Course, Subjects = @Subjects, Year = @Year
                WHERE EducationId = @EducationId";
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
                    _myCommand.Parameters.AddWithValue("@SchoolName", _education.SchoolName);
                    _myCommand.Parameters.AddWithValue("@Course", _education.Course);
                    _myCommand.Parameters.AddWithValue("@Subjects", _education.Subjects);
                    _myCommand.Parameters.AddWithValue("@Year", _education.Year);
                    _myCommand.Parameters.AddWithValue("@EducationId", _education.EducationId);
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
                DELETE FROM dbo.Education
                WHERE EducationId = @EducationId";
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
                    _myCommand.Parameters.AddWithValue("@EducationId", id);
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