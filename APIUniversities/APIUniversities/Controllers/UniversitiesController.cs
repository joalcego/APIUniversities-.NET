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
    public class UniversitiesController : ApiController
    {
        private static UniversitiesData uData;

        public UniversitiesController()
        {
            if (uData == null)
                uData = new UniversitiesData();
        }

        [HttpGet]
        public IEnumerable<UniversityModel> FindAll()
        {
            return uData.FindAll();
        }

        [HttpGet]
        public IHttpActionResult GetByCode(string universityId)
        {
            UniversityModel university = uData.GetByCode(universityId);

            if(university == null)
            {
                return NotFound();
            }

            return Ok(university);
        }

        [HttpPost]
        public IHttpActionResult Create([FromBody] UniversityModel university)
        {
            if(university == null)
                return BadRequest(Constants.MsgErrorArguments);

            bool inserted = uData.Insert(university);
            if (!inserted)
                return BadRequest(Constants.MsgError);

            return Ok(Constants.MsgSuccess);
        }

        [HttpPut]
        public IHttpActionResult Update(string universityId, [FromBody] UniversityModel university)
        {
            if(university == null)
                return BadRequest(Constants.MsgErrorArguments);

            university.Code = universityId;
            bool updated = uData.Update(university);
            if (!updated)
                return BadRequest(Constants.MsgError);

            return Ok(Constants.MsgSuccess);
        }

        [HttpDelete]
        public IHttpActionResult Delete(string universityId)
        {
            UniversityModel university = new UniversityModel()
            {
                Code = universityId
            };

            bool deleted = uData.Delete(university);

            if (!deleted)
                return BadRequest(Constants.MsgError);

            return Ok(Constants.MsgSuccess);
        }
    }
}
