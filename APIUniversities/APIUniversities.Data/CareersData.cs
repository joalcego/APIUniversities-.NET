using APIUniversities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIUniversities.Data
{
    public class CareersData : BaseData<CareerModel, Career>
    {
        public CareersData()
        {
            Set = DBContext.Careers;
        }

        #region Converters

        internal override Career Converter(CareerModel model)
        {
            Career entity = null;

            if (model != null)
            {
                entity = new Career()
                {
                    id = model.Id,
                    name = model.Name,
                    description = model.Description,
                    universityCode = model.UniversityCode
                };
            }

            return entity;
        }

        internal override CareerModel Converter(Career entity)
        {
            CareerModel model = null;

            if (entity != null)
            {
                model = new CareerModel()
                {
                    Id = entity.id,
                    Name = entity.name,
                    Description = entity.description,
                    UniversityCode = entity.universityCode
                };
            }

            return model;
        }

        #endregion

        public List<CareerModel> FindAll()
        {
            return Set.ToList().ConvertAll(Converter);
        }

        public List<CareerModel> FindByUniversity(string universityCode)
        {
            return Set.Where(x => x.universityCode == universityCode).ToList().ConvertAll(Converter);
        }

        public CareerModel GetById(int id)
        {
            return Converter(Set.Find(id));
        }

        public override bool Insert(CareerModel model)
        {
            try
            {
                if (GetById(model.Id) == null)
                {
                    Career career = Converter(model);

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

        public override bool Update(CareerModel model)
        {
            try
            {
                Career career = Set.Find(model.Id);

                if (career != null)
                {
                    career.name = model.Name;
                    career.description = model.Description;
                    career.universityCode = model.UniversityCode;
                    DBContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
        
        public override bool Delete(CareerModel model)
        {
            try
            {
                Career career = Set.Find(model.Id);

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
