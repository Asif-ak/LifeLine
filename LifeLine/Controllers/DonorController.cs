using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using LifeLine_WebApi.DBConfiguration;
using LifeLine_WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using LifeLine_WebAPi.WrapperCLasses;

namespace LifeLine_WebAPi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/donor")]
    [ApiController]
    public class DonorController : ControllerBase
    {
        private readonly LifeLineContext _context;

        public DonorController(LifeLineContext context)
        {
            this._context = context;
        }
        // GET: api/Donor
        /// <summary>
        /// Get all the registered donors.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Donor> Get()
        {
            return _context.Donors.ToList();
        }

        // GET: api/Donor/5
        /// <summary>
        /// Get the donor by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var donor= await _context.Donors.FindAsync(id);
            if(donor==null)
            {
                return NotFound();
            }
            return Ok(donor);

        }

        // POST: api/Donor
        // POST: will only accepts from form
        /// <summary>
        /// Register a donor.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromForm] Donor value)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                if (!HelperClass.IsValidEmail(value.Email))
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                if (_context.Donors.Any(a => a.DonorCellNumber == value.DonorCellNumber))
                    return new HttpResponseMessage(HttpStatusCode.AlreadyReported);
                //var _donor = new Donor
                //{
                //    DonorName = value.DonorName,
                //    City = value.City,
                //    DonorCellNumber = value.DonorCellNumber,
                //    DonorBloodtype = value.DonorBloodtype,
                //    Email=value.Email
                //};
                
                await _context.Donors.AddAsync(value);
                
                await _context.SaveChangesAsync();

                _context.ChangeTracker.Tracked += (s, e) =>
                  {
                      
                      
                  };
                return new HttpResponseMessage(HttpStatusCode.Created);
                //return message;
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // PUT: api/Donor/5

        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Delete the registered donor using id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}", Name = "Delete")]
        
        public async Task<HttpResponseMessage> Delete(int id)
        {
            var donor = _context.Donors.Where(a => a.DonorID == id).FirstOrDefault();
            if (donor != null)
            {
                try
                {
                    _context.Donors.Remove(donor);
                    //_context.Entry(donor).State=Microsoft.EntityFrameworkCore.EntityState.Deleted;
                    await _context.SaveChangesAsync();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                catch (Exception)
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);

                }
            }
            else { return new HttpResponseMessage(HttpStatusCode.NotFound); }

            


        }
    }
}
