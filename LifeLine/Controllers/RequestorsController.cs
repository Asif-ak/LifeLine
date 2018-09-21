using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LifeLine_WebApi.DBConfiguration;
using LifeLine_WebApi.Models;
using System.Net.Http;
using System.Net;

namespace LifeLine_WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestorsController : ControllerBase
    {
        private readonly LifeLineContext _context;

        public RequestorsController(LifeLineContext context)
        {
            _context = context;
        }

        // GET: api/Requestors
        [HttpGet]
        public IEnumerable<Requestor> GetRequestor()
        {

            return _context.Requestor.ToList();
        }

        // GET: api/Requestors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequestor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requestor = await _context.Requestor.FindAsync(id);

            if (requestor == null)
            {
                return NotFound();
            }

            return Ok(requestor);
        }

        // POST:api/Requestors
        [HttpPost]
        public async Task<HttpResponseMessage> PostRequestor([FromForm] Requestor requestor, [FromForm] Requests requests)
        {
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            var value = _context.Requestor.FirstOrDefault(a => a.RequestorCellNumber == requestor.RequestorCellNumber);
            try
            {
                if (value == null)
                {
                    var add = _context.Requestor.Add(requestor);
                    await _context.SaveChangesAsync();
                    Requests rr = new Requests
                    {
                        RID = add.Entity.ID,
                        RequestedBloodtype = requests.RequestedBloodtype,
                        IsActive = true
                    };

                    _context.Requests.Add(rr);
                    await _context.SaveChangesAsync();
                    return new HttpResponseMessage(HttpStatusCode.Accepted);

                }
                else 
                {
                    Requests rr = new Requests
                    {
                        RID = value.ID,
                        RequestedBloodtype = requests.RequestedBloodtype,
                        IsActive = true
                    };
                    _context.Requests.Add(rr);
                    await _context.SaveChangesAsync();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        // DELETE: api/Requestors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequestor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requestor = await _context.Requestor.FindAsync(id);
            var request = _context.Requests.Where(a => a.Requestor.ID == requestor.ID).FirstOrDefault();
            if (requestor == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            _context.Requestor.Remove(requestor);
            await _context.SaveChangesAsync();

            return Ok(requestor);
        }


    }
}