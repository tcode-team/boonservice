using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using boonservice.api.Context;
using boonservice.api.Models;

namespace boonservice.api.Controllers
{
    public class ShipmentTypeController : ApiController
    {
        /// <summary>
        /// ประเภทรถทั้งหมด (Language TH only)
        /// </summary>
        /// <remarks>
        /// Get a list of all sapsr3.t173t
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="404">Data not found</response>
        [ResponseType(typeof(IEnumerable<T173T>))]
        [Route("shipmenttype/getall")]
        public HttpResponseMessage GetShipmentType()
        {
            try
            {
                using (var context = new SapContext())
                {
                    var t173ts = context.T173T.Where(t => t.SPRAS == "2").ToList();
                    return t173ts == null
                        ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "T173T not found")
                        : Request.CreateResponse(HttpStatusCode.OK, t173ts);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            
        }

        /// <summary>
        /// ประเภทรถ
        /// </summary>
        /// <remarks>
        /// Get a detail of sapsr3.t173t
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="404">Data not found</response>
        [ResponseType(typeof(T173T))]
        [Route("shipmenttype/detail")]
        public HttpResponseMessage Get(string id)
        {
            try
            {
                using (var context = new SapContext())
                {
                    var t173t = context.T173T.Where(t => t.MANDT == "900" && t.SPRAS == "2" && t.VSART == id).FirstOrDefault();
                    return t173t == null
                        ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "T173T not found")
                        : Request.CreateResponse(HttpStatusCode.OK, t173t);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            
        }

    }
}