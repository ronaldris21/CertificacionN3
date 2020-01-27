using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ApiTudo.Models;

namespace ApiTudo.Controllers
{
    public class busesController : ApiController
    {
        private DBEntityModels db = new DBEntityModels();
        private Repository.Rbuses repo = new Repository.Rbuses();

        // GET: api/buses
        [HttpGet]
        public HttpResponseMessage Getbuses()
        {
            return Request.CreateResponse(HttpStatusCode.OK, repo.GetAll());
        }

        // PUT: api/buses/5
        [HttpPut]
        public HttpResponseMessage Putbuses(int id, buses bus)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
            }

            if (id != bus.idBus)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
            }

            if (repo.Put(id,bus))
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, bus);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No es posible actualizar");

        }

        // POST: api/buses
        [HttpPost]
        public HttpResponseMessage Postbuses(buses bus)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
            }

            if (repo.Post(ref bus))
            {
                return Request.CreateResponse(HttpStatusCode.Created, bus);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No es posible agregar a la base de datos");
        }

        // DELETE: api/buses/5
        [HttpDelete]
        public HttpResponseMessage Deletebuses(int id)
        {
            buses buses = db.buses.Find(id);
            if (buses == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No se encontró el dato buscado");
            }

            if (repo.Delete(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Eliminado");
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotImplemented, "No es posible eliminar");
        }


    }
}