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
    public class ShipmentController : ApiController
    {

        /// <summary>
        /// ค้นหา Shipment คำนวณค่าขนส่ง BLF
        /// </summary>
        /// <remarks>
        /// Get a list of all sapsr3.t173t
        /// </remarks>
        /// <param name="searchvalue">criteria</param>
        /// <returns></returns>
        /// <response code="200"></response>
        [ResponseType(typeof(IEnumerable<VTTK>))]
        [Route("shipment/search")]
        public HttpResponseMessage PostShipmentSearh(ShipmentSearchModel searchvalue)
        {
            var datefrom = DateTime.ParseExact(searchvalue.ShipmentDateFrom, "dd/MM/yyyy", null).Date.ToString("yyyyMMdd");
            var dateto = DateTime.ParseExact(searchvalue.ShipmentDateTo, "dd/MM/yyyy", null).Date.ToString("yyyyMMdd");
            using (var context = new SapContext())
            {
                var vttk = context.VTTK.Where(t =>
                    t.MANDT == "900" &&
                    t.ERDAT.CompareTo(datefrom) >= 0 &&
                    t.ERDAT.CompareTo(dateto) <= 0).Take(100).ToList();

                return vttk == null
                    ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "T173T not found")
                    : Request.CreateResponse(HttpStatusCode.OK, vttk);
            }
        }

        /// <summary>
        /// Get Shipment list (max 25 records)
        /// </summary>
        /// <remarks>
        /// Get a list of all sapsr3.vttk
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [ResponseType(typeof(IEnumerable<VTTK>))]
        [Route("shipment/getall")]
        public HttpResponseMessage Get()
        {
            using (var context = new SapContext())
            {
                var vttks = context.VTTK.Take(25).ToList();

                //foreach(VTTK c in vttks)
                //{
                //    c.T173Ts = new List<T173T>();
                //    c.T173Ts.AddRange(context.T173T.Where(t => t.MANDT == c.MANDT && t.SPRAS == "2" && t.VSART == c.VSART));
                //};
                  
                return vttks == null
                    ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "VTTK not found")
                    : Request.CreateResponse(HttpStatusCode.OK, vttks);
            }
        }
        
    }
}