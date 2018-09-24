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

namespace LifeLine_WebAPi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly LifeLineContext _context;

        public RequestsController(LifeLineContext context)
        {
            _context = context;
        }

        // GET: api/Requests
        /// <summary>
        /// To get requests and their corresponding requestors.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetRequests()
        {
            //var result= _context.Requests.ToList();
            var result = await (from request in _context.Requests
                                join requestor in _context.Requestor on request.RID equals requestor.ID
                                select new
                                {
                                    id = request.RequestID,
                                    requestedbloodtype = Enum.GetName(typeof(BloodType), request.RequestedBloodtype),
                                    Active = (bool)request.IsActive,
                                    requestornumber = requestor.RequestorCellNumber, // or requestor.id
                                    requestor = requestor.RequestorName
                                }
                         ).ToListAsync();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
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
        /// <summary>
        /// to get the requests by requestor's cell/mobile number.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [HttpGet("{number}", Name = "GetByNumber")] //// GET: api/Requests/923*********S
        public async Task<IActionResult> GetRequestsByNumber([FromRoute] string number)
        {
            var result = await (from requestor in _context.Requestor
                                join request in _context.Requests on requestor.ID equals request.RID
                                where requestor.RequestorCellNumber == number
                                select new
                                {
                                    id = requestor.ID,
                                    name = requestor.RequestorName,
                                    number = requestor.RequestorCellNumber,
                                    date = requestor.RequestedOn,
                                    city = requestor.City,
                                    address = requestor.DonationAddress,
                                    email = requestor.Email,
                                    requestedbloodtype = Enum.GetName(typeof(BloodType), request.RequestedBloodtype),
                                    Active = (bool)request.IsActive
                                }).ToListAsync();



            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // isko bhi sai krna hai

        // PUT: api/Requests/5
        /// <summary>
        /// to update the requests status.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requests"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<HttpResponseMessage> PutRequests([FromRoute] int id, [FromForm] Requests requests)
        {
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }

            if (id != requests.RequestID)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);

            }
            var request = _context.Requests.FirstOrDefault(a => a.RequestID == id);
            Requests _requests = new Requests
            {
                IsActive = requests.IsActive,
                RequestedBloodtype = requests.RequestedBloodtype,
                RequestID = id,
                Requestor = request.Requestor

            };

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestsExists(id))
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
                }
                else
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                }
            }

        }



        // DELETE: api/Requests/5
        /// <summary>
        /// to delete individual request by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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