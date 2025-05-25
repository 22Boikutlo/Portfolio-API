using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        ///GENERIC GET THAT GET ALL THE DATA
        // GET: api/Users
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        
        /// GET USER BY ID       
        // GET api/Users/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"value {id}";
        }

        //GENERIC POST THAT CREATE A NEW USER
        // POST api/Users
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        //GENERIC PUT THAT UPDATE A USER
        // PUT api/Users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        //GENERIC DELETE THAT DELETE A USER
        // DELETE api/Users/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
