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
    public class CoursesController : ApiController
    {
        private static CoursesData cData;

        public CoursesController()
        {
            if (cData == null)
                cData = new CoursesData();
        }

        [HttpGet]
        public IEnumerable<CourseModel> FindByCareer(int careerId)
        {
            return cData.FindByCareer(careerId);
        }

        [HttpGet]
        public IHttpActionResult GetById(string careerId, int courseId)
        {
            CourseModel course = cData.GetById(courseId);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        [HttpPost]
        public IHttpActionResult Create(int careerId, [FromBody] CourseModel course)
        {
            if (course == null)
                return BadRequest(Constants.MsgErrorArguments);

            course.CareerId = careerId;
            bool inserted = cData.Insert(course);
            if (!inserted)
                return BadRequest(Constants.MsgError);

            return Ok(Constants.MsgSuccess);
        }

        [HttpPut]
        public IHttpActionResult Update(int careerId, int courseId, [FromBody] CourseModel course)
        {
            if (course == null)
                return BadRequest(Constants.MsgErrorArguments);

            course.CareerId = careerId;
            course.Id = courseId;
            bool updated = cData.Update(course);
            if (!updated)
                return BadRequest(Constants.MsgError);

            return Ok(Constants.MsgSuccess);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int courseId)
        {
            CourseModel course = new CourseModel()
            {
                Id = courseId
            };

            bool deleted = cData.Delete(course);

            if (!deleted)
                return BadRequest(Constants.MsgError);

            return Ok(Constants.MsgSuccess);
        }
    }
}
