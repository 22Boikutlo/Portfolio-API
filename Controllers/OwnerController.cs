using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Models;

namespace PortfolioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public OwnerController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                SELECT FirstName, MiddleName, LastName, AboutMe, OwnerRole,PhoneNumber,EmailURL,LinkedInURL,GitHub
                FROM dbo.PortfolioOwner";
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
        public JsonResult Post(PortfolioOwner _owner)
        {
            string query = @"
                INSERT INTO dbo.PortfolioOwner (FirstName, MiddleName, LastName, AboutMe, OwnerRole,PhoneNumber,EmailURL,LinkedInURL,GitHub)
                VALUES (@FirstName, @MiddleName, @LastName, @AboutMe,@OwnerRole,@PhoneNumber,@EmailURL,@LinkedInURL,@GitHub)";
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
                    _myCommand.Parameters.AddWithValue("@FirstName", _owner.FirstName);
                    _myCommand.Parameters.AddWithValue("@MiddleName", _owner.MiddleName);
                    _myCommand.Parameters.AddWithValue("@LastName", _owner.LastName);
                    _myCommand.Parameters.AddWithValue("@AboutMe", _owner.AboutMe);
                    _myCommand.Parameters.AddWithValue("@OwnerRole", _owner.OwnerRole);
                    _myCommand.Parameters.AddWithValue("@PhoneNumber", _owner.PhoneNumber);
                    _myCommand.Parameters.AddWithValue("@EmailURL", _owner.EmailURL);
                    _myCommand.Parameters.AddWithValue("@LinkedInURL", _owner.LinkedInURL);
                    _myCommand.Parameters.AddWithValue("@GitHub", _owner.GitHub);

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
        public JsonResult Put(PortfolioOwner _owner)
        {
            string query = @"
                UPDATE dbo.PortfiolioOwner
                SET FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, AboutMe = @AboutMe, OwnerRole = @OwnerRole,PhoneNumber = @PhoneNumber,EmailURL = @EmailURL,LinkedInURL = @LinkedInURL,GitHub = @GitHub
                WHERE OwnerId = @OwnerId";
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
                    _myCommand.Parameters.AddWithValue("@FirstName", _owner.FirstName);
                    _myCommand.Parameters.AddWithValue("@MiddleName", _owner.MiddleName);
                    _myCommand.Parameters.AddWithValue("@LastName", _owner.LastName);
                    _myCommand.Parameters.AddWithValue("@AboutMe", _owner.AboutMe);
                    _myCommand.Parameters.AddWithValue("@OwnerRole", _owner.OwnerRole);
                    _myCommand.Parameters.AddWithValue("@PhoneNumber", _owner.PhoneNumber);
                    _myCommand.Parameters.AddWithValue("@EmailURL", _owner.EmailURL);
                    _myCommand.Parameters.AddWithValue("@LinkedInURL", _owner.LinkedInURL);
                    _myCommand.Parameters.AddWithValue("@GitHub", _owner.GitHub);
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
                DELETE FROM dbo.PortfolioOwner
                WHERE OwnerId = @OwnerId";
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
                    _myCommand.Parameters.AddWithValue("@OwnerId", id);
                    // Execute the command
                    _myReader = _myCommand.ExecuteReader();
                    _table.Load(_myReader);
                    _myReader.Close();
                    _myCon.Close();
                }
            }
            return new JsonResult("Deleted Successfully");
        }
        [Route("Save File")]
        [HttpPost]
        public JsonResult SaveFile(IFormFile file)
        {
            try
            {
               var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = _webHostEnvironment.ContentRootPath + "/Images/" + fileName;
                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(fileName);
            }
            catch (Exception ex)
            {
                return new JsonResult($"Error: {ex.Message}");
            }
        }
    }
}