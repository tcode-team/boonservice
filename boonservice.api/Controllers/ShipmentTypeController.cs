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
    /// <summary>
    /// Shipment Type class
    /// </summary>
    public class ShipmentTypeController : ApiController
    {

        string client = System.Configuration.ConfigurationManager.AppSettings["client"];

        /// <summary>
        /// ประเภทรถทั้งหมด (Language TH only)
        /// </summary>
        /// <remarks>
        /// Get a list of all sapsr3.t173t
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="404">Data not found</response>
        [ResponseType(typeof(IEnumerable<ShipmentTypeModel>))]
        [Route("shipmenttype/getall")]
        public HttpResponseMessage GetShipmentType()
        {
            var ShipmentTypes = new List<ShipmentTypeModel>();
            try
            {
                using (var context = new SAPContext())
                {
                    var t173ts = context.T173T.Where(t => t.MANDT == client && t.SPRAS == "2").ToList();
                    foreach (var r in t173ts)
                    {
                        var ShipmentType = new ShipmentTypeModel();
                        ShipmentType.client = r.MANDT;
                        ShipmentType.lang_code = r.SPRAS;
                        ShipmentType.shipmenttype_code = r.VSART;
                        ShipmentType.shipmenttype_desc = r.BEZEI;
                        ShipmentTypes.Add(ShipmentType);
                    }
                    return ShipmentTypes == null
                        ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "Shipment Type not found")
                        : Request.CreateResponse(HttpStatusCode.OK, ShipmentTypes);
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
        [ResponseType(typeof(ShipmentTypeModel))]
        [Route("shipmenttype/detail")]
        public HttpResponseMessage Get(string id)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var t173t = context.T173T.Where(t => t.MANDT == client && t.SPRAS == "2" && t.VSART == id).FirstOrDefault();
                    ShipmentTypeModel ShipmentType = new ShipmentTypeModel();
                    if (t173t != null)
                    {
                        ShipmentType.client = t173t.MANDT;
                        ShipmentType.lang_code = t173t.SPRAS;
                        ShipmentType.shipmenttype_code = t173t.VSART;
                        ShipmentType.shipmenttype_desc = t173t.BEZEI;
                    };
                    return ShipmentType.shipmenttype_code == null
                        ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "Shipment Type not found")
                        : Request.CreateResponse(HttpStatusCode.OK, ShipmentType);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            
        }

    }
}