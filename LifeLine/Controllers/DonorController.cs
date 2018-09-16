using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using LifeLine_WebApi.DBConfiguration;
using LifeLine_WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LifeLine_WebAPi.Controllers
{

    [Route("api/donor")]
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
        public Donor Get(int id)
        {
            return _context.Donors.FirstOrDefault(i => i.DonorID == id);

        }

        // POST: api/Donor
        // POST: will only accepts from form
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromForm] Donor value)
        {
            try
            {
                if (_context.Donors.Any(a => a.DonorCellNumber == value.DonorCellNumber))
                    return new HttpResponseMessage(HttpStatusCode.AlreadyReported);
                var _donor = new Donor
                {
                    DonorName = value.DonorName,
                    City = value.City,
                    DonorCellNumber = value.DonorCellNumber,
                    DonorBloodtype = value.DonorBloodtype
                };

                await _context.Donors.AddAsync(_donor);
                await _context.SaveChangesAsync();
                var message = new HttpResponseMessage(HttpStatusCode.Created);
                return message;
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
        [HttpDelete("{id}", Name = "Delete")]
        //[HttpDelete]//("{id}"]
        //[Route("api/donor/Delete/{id}")]
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
