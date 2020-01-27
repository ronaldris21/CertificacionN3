using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiTudo.Repository
{
    using Models;
    public class Rbuses
    {
        /// <summary>
        /// Metood para obtener todos los datos
        /// </summary>
        /// <returns></returns>
        public List<buses> GetAll()
        {
            using (var db = new Models.DBEntityModels())
            {
                db.Configuration.ProxyCreationEnabled = false;
                return db.buses.ToList();
            }
        }
        /// <summary>
        /// Metodo pare eliminar un registro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            using (var db = new Models.DBEntityModels())
            {
                try
                {
                    var bus = db.buses.Find(id);
                    if (bus == null)
                    {
                        return false;
                    }
                    db.buses.Remove(bus);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Metodo para actualizar un registro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bus"></param>
        /// <returns></returns>
        public bool Put(int id, buses bus)
        {
            using (var db = new Models.DBEntityModels())
            {
                try
                {
                    var busOld = db.buses.Find(id);
                    if (bus == null)
                    {
                        return false;
                    }
                    busOld.capacidad = bus.capacidad;
                    busOld.color = bus.color;
                    busOld.estado = bus.estado;
                    busOld.year = bus.year;
                    db.Entry(busOld).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Metodo para agregar un registro
        /// </summary>
        /// <param name="bus"></param>
        /// <returns></returns>
        public bool Post(ref buses  bus)
        {
            using (var db = new Models.DBEntityModels())
            {
                try
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.buses.Add(bus);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }



    }
}