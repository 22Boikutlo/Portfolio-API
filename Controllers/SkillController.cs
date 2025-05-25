using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Models;

namespace PortfolioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public SkillController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                SELECT  SkillName, SkillDescription, WebAddress, SelfRating
                FROM dbo.Skills";
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
        public JsonResult Post(Skills _skills)
        {
            string query = @"
                INSERT INTO dbo.Skills (SkillName, SkillDescription, WebAddress, SelfRating)
                VALUES (@SkillName, @SkillDescription, @WebAddress)";
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
                    _myCommand.Parameters.AddWithValue("@SkillName", _skills.SkillName);
                    _myCommand.Parameters.AddWithValue("@SkillDescription", _skills.SkillDescription);
                    _myCommand.Parameters.AddWithValue("@WebAddress", _skills.WebAddress);
                    _myCommand.Parameters.AddWithValue("@SelfRating", _skills.SelfRating);

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
        public JsonResult Put(Skills _skills)
        {
            string query = @"
                UPDATE dbo.Skills
                SET SkillName = @SkillName, SkillDescription = @SkillDescription, WebAddress = @WebAddress, SelfRating = @SelfRating
                WHERE SkillId = @SkillId";
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
                    _myCommand.Parameters.AddWithValue("@SkillName", _skills.SkillName);
                    _myCommand.Parameters.AddWithValue("@SkillDescription", _skills.SkillDescription);
                    _myCommand.Parameters.AddWithValue("@WebAddress", _skills.WebAddress);
                    _myCommand.Parameters.AddWithValue("@SelfRating", _skills.SelfRating);
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
                DELETE FROM dbo.Skills
                WHERE SkillId = @SkillId";
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
                    _myCommand.Parameters.AddWithValue("@SkillId", id);
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