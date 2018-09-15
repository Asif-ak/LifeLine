using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LifeLine_WebApi.DBConfiguration;
using LifeLine_WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LifeLine_WebAPi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class DonorController : ControllerBase
    {
        private readonly LifeLineContext _context;

        public DonorController(LifeLineContext context)
        {
            this._context = context;
        }
        // GET: api/Donor
        [HttpGet]
        public IEnumerable<Donor> Get()
        {
            return _context.Donors.ToList();
        }

        // GET: api/Donor/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Donor
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Donor/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
