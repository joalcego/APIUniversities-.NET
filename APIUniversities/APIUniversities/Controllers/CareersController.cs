using APIUniversities.Data;
using APIUniversities.Infrastructure;
using APIUniversities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIUniversities.Controllers
{
    public class CareersController : ApiController
    {
        private static CareersData cData;

        public CareersController()
        {
            if (cData == null)
                cData = new CareersData();
        }

        [HttpGet]
        public IEnumerable<CareerModel> FindByUniversity(string universityId)
        {
            return cData.FindByUniversity(universityId);
        }

        [HttpGet]
        public IHttpActionResult GetById(string universityId, int careerId)
        {
            CareerModel career = cData.GetById(careerId);

            if (career == null)
            {
                return NotFound();
            }

            return Ok(career);
        }

        [HttpPost]
        public IHttpActionResult Create(string universityId, [FromBody] CareerModel career)
        {
            if (career == null)
                return BadRequest(Constants.MsgErrorArguments);

            career.UniversityCode = universityId;
            bool inserted = cData.Insert(career);
            if (!inserted)
                return BadRequest(Constants.MsgError);

            return Ok(Constants.MsgSuccess);
        }

        [HttpPut]
        public IHttpActionResult Update(string universityId, int careerId, [FromBody] CareerModel career)
        {
            if (career == null)
                return BadRequest(Constants.MsgErrorArguments);

            career.UniversityCode = universityId;
            career.Id = careerId;
            bool updated = cData.Update(career);
            if (!updated)
                return BadRequest(Constants.MsgError);

            return Ok(Constants.MsgSuccess);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int careerId)
        {
            CareerModel career = new CareerModel()
            {
                Id = careerId
            };

            bool deleted = cData.Delete(career);

            if (!deleted)
                return BadRequest(Constants.MsgError);

            return Ok(Constants.MsgSuccess);
        }
    }
}
