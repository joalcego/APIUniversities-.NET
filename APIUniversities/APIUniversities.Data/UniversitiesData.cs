using APIUniversities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIUniversities.Data
{
    public class UniversitiesData : BaseData<UniversityModel, University>
    {
        public UniversitiesData()
        {
            Set = DBContext.Universities;
        }

        #region Converters

        internal override University Converter(UniversityModel model)
        {
            University entity = null;

            if(model != null)
            {
                entity = new University()
                {
                    code = model.Code,
                    name = model.Name,
                    website = model.Website
                };
            }

            return entity;
        }

        internal override UniversityModel Converter(University entity)
        {
            UniversityModel model = null;

            if (entity != null)
            {
                model = new UniversityModel()
                {
                    Code = entity.code,
                    Name = entity.name,
                    Website = entity.website
                };
            }

            return model;
        }

        #endregion

        public List<UniversityModel> FindAll()
        {
            return Set.ToList().ConvertAll(Converter);
        }

        public UniversityModel GetByCode(string code)
        {
            return Converter(Set.Where(x => x.code == code).FirstOrDefault());
        }

        public override bool Insert(UniversityModel model)
        {
            try
            {
                if (GetByCode(model.Code) == null)
                {
                    University university = Converter(model);

                    if (university != null)
                    {
                        Set.Add(university);
                        DBContext.SaveChanges();
                    }
                }
                else
                {
                    //duplicate university
                    return false;
                }
            }
            catch(Exception ex)
            {
                return false;
            }

            return true;
        }

        public override bool Update(UniversityModel model)
        {
            try
            {
                University university = Set.Where(x => x.code == model.Code).FirstOrDefault();

                if (university != null)
                {
                    university.name = model.Name;
                    university.website = model.Website;
                    DBContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }


        public override bool Delete(UniversityModel model)
        {
            try
            {
                University university = Set.Where(x => x.code == model.Code).FirstOrDefault();

                if (university != null)
                {
                    Set.Remove(university);
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
