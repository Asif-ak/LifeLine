using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LifeLine_WebApi.DBConfiguration;
using LifeLine_WebApi.Models;

namespace LifeLine_WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly LifeLineContext _context;

        public RequestsController(LifeLineContext context)
        {
            _context = context;
        }

        // GET: api/Requests
        [HttpGet]
        public IEnumerable<Requests> GetRequests()
        {
            return _context.Requests.ToList();
        }

        // GET: api/Requests/5
        //[HttpGet("{id}", Name = "GetByID")]

        //public async Task<IActionResult> GetRequests([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var requests = await _context.Requests.FindAsync(id);

        //    if (requests == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(requests);
        //}
        //

        //GET: api/Requests/92**********
        [HttpGet("{number}", Name = "GetByNumber")] //// GET: api/Requests/923*********S
        public IActionResult GetRequestsByNumber([FromRoute] string number)
        {
            var result = (from requestor in _context.Requestor
                          join request in _context.Requests on requestor.ID equals request.RID
                          where requestor.RequestorCellNumber == number
                          select new
                          {
                              id = requestor.ID,
                              name = requestor.RequestorName,
                              number = requestor.RequestorCellNumber,
                              date = requestor.RequestedOn,
                              bloodtype = Enum.GetName(typeof(BloodType), request.RequestedBloodtype),
                              Active = (bool)request.IsActive
                          }).ToList();



            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // isko bhi sai krna hai

        // PUT: api/Requests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequests([FromRoute] int id, [FromForm] Requests requests)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != requests.RequestID)
            {
                return BadRequest();
            }
            var request = _context.Requests.FirstOrDefault(a => a.RequestID == id);
            Requests _requests = new Requests
            {
                IsActive = requests.IsActive,
                RequestedBloodtype = requests.RequestedBloodtype,
                RequestID = id,
                Requestor = request.Requestor

            };
            //request.IsActive = requests.IsActive;
            //request.RequestID = id;
            //request.RequestedBloodtype = requests.RequestedBloodtype;
            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!RequestsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequests([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requests = await _context.Requests.FindAsync(id);
            if (requests == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(requests);
            await _context.SaveChangesAsync();

            return Ok(requests);
        }

        private bool RequestsExists(int id)
        {
            return _context.Requests.Any(e => e.RequestID == id);
        }
    }
}