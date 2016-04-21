using APIUniversities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIUniversities.Data
{
    public class CoursesData : BaseData<CourseModel, Courses>
    {
        public CoursesData()
        {
            Set = DBContext.Courses;
        }

        #region Converters

        internal override Courses Converter(CourseModel model)
        {
            Courses entity = null;

            if (model != null)
            {
                entity = new Courses()
                {
                    id = model.Id,
                    name = model.Name,
                    cost = model.Cost,
                    careerId = model.CareerId
                };
            }

            return entity;
        }

        internal override CourseModel Converter(Courses entity)
        {
            CourseModel model = null;

            if (entity != null)
            {
                model = new CourseModel()
                {
                    Id = entity.id,
                    Name = entity.name,
                    Cost = entity.cost,
                    CareerId = entity.careerId
                };
            }

            return model;
        }

        #endregion

        public List<CourseModel> FindAll()
        {
            return Set.ToList().ConvertAll(Converter);
        }

        public List<CourseModel> FindByCareer(int careerId)
        {
            return Set.Where(x => x.careerId == careerId).ToList().ConvertAll(Converter);
        }

        public CourseModel GetById(int id)
        {
            return Converter(Set.Find(id));
        }

        public override bool Insert(CourseModel model)
        {
            try
            {
                if (GetById(model.Id) == null)
                {
                    Courses career = Converter(model);

                    if (career != null)
                    {
                        Set.Add(career);
                        DBContext.SaveChanges();
                    }
                }
                else
                {
                    //duplicate university
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public override bool Update(CourseModel model)
        {
            try
            {
                Courses career = Set.Find(model.Id);

                if (career != null)
                {
                    career.name = model.Name;
                    career.cost = model.Cost;
                    career.careerId = model.CareerId;
                    DBContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public override bool Delete(CourseModel model)
        {
            try
            {
                Courses career = Set.Find(model.Id);

                if (career != null)
                {
                    Set.Remove(career);
                    DBContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

    }
}
