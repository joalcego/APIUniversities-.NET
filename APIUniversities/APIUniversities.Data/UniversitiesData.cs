using APIUniversities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIUniversities.Data
{
    public class UniversitiesData : BaseData<UniversityModel, Universities>
    {
        public UniversitiesData()
        {
            Set = DBContext.Universities;
        }

        #region Converters

        internal override Universities Converter(UniversityModel model)
        {
            Universities entity = null;

            if(model != null)
            {
                entity = new Universities()
                {
                    code = model.Code,
                    name = model.Name,
                    website = model.Website
                };
            }

            return entity;
        }

        internal override UniversityModel Converter(Universities entity)
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
                    Universities university = Converter(model);

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
                Universities university = Set.Where(x => x.code == model.Code).FirstOrDefault();

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
                Universities university = Set.Where(x => x.code == model.Code).FirstOrDefault();

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
