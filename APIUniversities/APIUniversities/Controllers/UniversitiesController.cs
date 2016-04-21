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
        private List<UniversityModel> universities = new List<UniversityModel>()
        {
            new UniversityModel { Code = "CFT", Name="Universidad Cenfotec", Website="www.ucenfotec.ac.cr"},
            new UniversityModel { Code = "LTN", Name="Universidad Latina", Website="www.ulatina.ac.cr"},
            new UniversityModel { Code="UND", Name="Universidad Estatal a Distancia", Website="www.uned.ac.cr"}
        };

        [HttpGet]
        public IEnumerable<UniversityModel> FindAll()
        {
            return universities;
        }

        [HttpGet]
        public IHttpActionResult GetByCode(string id)
        {
            UniversityModel university = universities.Where(x => x.Code == id).FirstOrDefault();

            if(university == null)
            {
                return NotFound();
            }

            return Ok(university);
        }

        [HttpPost()]
        public IHttpActionResult Create([FromBody] UniversityModel university)
        {
            if(university == null)
            {
                return BadRequest("No arguments were passed");
            }

            //no data is saved here, this is just an example
            universities.Add(university);

            return Ok("Created successfully!");
        }

        [HttpPut]
        public IHttpActionResult Update(string id, [FromBody] UniversityModel university)
        {
            if(university == null)
            {
                return BadRequest("No arguments were passed");
            }

            UniversityModel targetUniversity = universities.Where(x => x.Code == id).FirstOrDefault();

            if(targetUniversity == null)
            {
                return BadRequest("University not found");
            }

            //no data is updated here, this is just an example
            targetUniversity.Name = university.Name;
            targetUniversity.Website = university.Website;

            return Ok("Updated successfully!");
        }

        [HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            universities.Remove(universities.Where(x => x.Code == id).FirstOrDefault());

            return Ok("Deleted successfully!");
        }
    }
}
