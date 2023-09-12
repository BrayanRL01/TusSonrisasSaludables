using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersViewController : ControllerBase
    {
        private readonly TusSonrisasSaludablesContext _context;

        public UsersViewController(TusSonrisasSaludablesContext context)
        {
            _context = context;
        }

        // GET: api/<UsersViewController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VwUser>>> SPGetUsersView()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            return await _context.VwUsers.FromSqlRaw("EXEC SP_GetAllUsersView").ToListAsync();
        }

        // GET api/<UsersViewController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> SPGetUserView(int id)
        {
            var users = await _context.VwUsers.FromSqlInterpolated($"EXEC SP_GetUserView {id}").ToListAsync();

            var user = users.FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST api/<UsersViewController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<UsersViewController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<UsersViewController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
