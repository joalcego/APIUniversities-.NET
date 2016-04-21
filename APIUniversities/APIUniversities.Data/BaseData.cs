using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIUniversities.Data
{
    public abstract class BaseData<Model, Entity> where Entity : class
    {
        protected static UniversitiesEntities DBContext { get; set; }
        protected DbSet<Entity> Set { get; set; }

        public BaseData()
        {
            if (DBContext == null)
                DBContext = new UniversitiesEntities();
        }

        //Converters from Entity Framework objects to models and from models to Entity Framework objects
        internal abstract Model Converter(Entity entity);
        internal abstract Entity Converter(Model model);

        //CRUD methods
        public abstract bool Insert(Model model);
        public abstract bool Update(Model model);
        public abstract bool Delete(Model model);
    }
}
