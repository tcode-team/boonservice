using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        string client = System.Configuration.ConfigurationManager.AppSettings["client"];

        /// <summary>
        /// ค้นหา Shipment คำนวณค่าขนส่ง BLF
        /// </summary>
        /// <remarks>
        /// ค้นหา Shipment สำหรับหน้าจอ คำนวณค่าขนส่ง BLF 
        /// </remarks>
        /// <param name="searchvalue">criteria</param>
        /// <returns></returns>
        /// <response code="200"></response>
        [ResponseType(typeof(IEnumerable<ShipmentDetailModel>))]
        [Route("shipment/search")]
        public async Task<HttpResponseMessage> PostShipmentSearh(ShipmentSearchModel searchvalue)
        {
            searchvalue.fetchdata = searchvalue.fetchdata == null ? new fetchdata() : searchvalue.fetchdata;

            var Shipments = new List<ShipmentDetailModel>();
            var datefrom = DateTime.ParseExact(searchvalue.ShipmentDateFrom, "dd/MM/yyyy", null).Date.ToString("yyyyMMdd");
            var dateto = DateTime.ParseExact(searchvalue.ShipmentDateTo, "dd/MM/yyyy", null).Date.ToString("yyyyMMdd");            

            Expression<Func<VTTK, bool>> forwardingExpression;
            if (searchvalue.forwarding != null)
            {
                searchvalue.forwarding = searchvalue.forwarding.PadLeft(10, '0');
                forwardingExpression = gto => gto.TDLNR == searchvalue.forwarding;
            } else
                forwardingExpression = gto => 1 == 1;

            Expression<Func<VTTK, bool>> ShipmentNumberExpression;
            if (searchvalue.ShipmentNo != null)
            {
                searchvalue.ShipmentNo = searchvalue.ShipmentNo.PadLeft(10, '0');
                ShipmentNumberExpression = gto => gto.TKNUM == searchvalue.ShipmentNo;
            }
            else
                ShipmentNumberExpression = gto => 1 == 1;

            Expression<Func<VTTK, bool>> ShipmentTypeExpression;
            if (searchvalue.ShipmentType != "ALL" && searchvalue.ShipmentType != null)
            {
                ShipmentTypeExpression = gto => gto.VSART == searchvalue.ShipmentType;
            }
            else
                ShipmentTypeExpression = gto => 1 == 1;

            Expression<Func<VTTK, bool>> CarLicenseExpression;
            if (searchvalue.CarLicense != null)
            {
                CarLicenseExpression = gto => gto.ADD01 == searchvalue.CarLicense;
            }
            else
                CarLicenseExpression = gto => 1 == 1;

            using (var context = new SapContext())
            {
                var vttks = context.VTTK
                    .Where(ShipmentNumberExpression)
                    .Where(t =>
                        t.MANDT == client &&
                        t.ERDAT.CompareTo(datefrom) >= 0 &&
                        t.ERDAT.CompareTo(dateto) <= 0)
                    .Where(ShipmentTypeExpression)
                    .Where(forwardingExpression)
                    .Where(CarLicenseExpression)
                    .OrderBy(t => t.TKNUM)
                    .Skip(searchvalue.fetchdata.after == 0 ? 0 : searchvalue.fetchdata.after)
                    .Take(searchvalue.fetchdata.size == 0 ? 500 : searchvalue.fetchdata.size)
                    .ToList();
                Shipments = MappingShipmentDetail(vttks);

                return Shipments == null
                    ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "Shipment List not found")
                    : Request.CreateResponse(HttpStatusCode.OK, Shipments);
            }
        }

        /// <summary>
        /// Shipment ทั้งหมด 
        /// </summary>
        /// <remarks>
        /// ข้อมูล Shipment ทั้งหมด
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [ResponseType(typeof(IEnumerable<ShipmentDetailModel>))]
        [Route("shipment/getall")]
        public async Task<HttpResponseMessage> Get(fetchdata fetchdata)
        {
            fetchdata = fetchdata == null ? new fetchdata() : fetchdata;

            var Shipments = new List<ShipmentDetailModel>();
            using (var context = new SapContext())
            {
                var vttks = context.VTTK
                    .Where(w => w.MANDT == client)
                    .OrderBy(t => t.TKNUM)
                    .Skip(fetchdata.after == 0 ? 0 : fetchdata.after)
                    .Take(fetchdata.size == 0 ? 500 : fetchdata.size)
                    .ToList();
                Shipments = MappingShipmentDetail(vttks);
                return Shipments == null
                    ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "Shipment not found")
                    : Request.CreateResponse(HttpStatusCode.OK, Shipments);
            }
        }

        private List<ShipmentDetailModel> MappingShipmentDetail(List<VTTK> vttks)
        {
            var Shipments = new List<ShipmentDetailModel>();
            using (var context = new SapContext())
            {
                foreach (var r in vttks)
                {
                    var shipment = new ShipmentDetailModel();
                    shipment.client = r.MANDT;
                    shipment.shipment_number = r.TKNUM;
                    shipment.shipment_date = DateTime.ParseExact(r.ERDAT, "yyyyMMdd", null);
                    shipment.shipmenttype_code = r.VSART;
                    if (r.VSART != " ")
                    {
                        var shipmenttype = context.T173T.Where(t => t.MANDT == r.MANDT && t.SPRAS == "2" && t.VSART == r.VSART).FirstOrDefault();
                        shipment.shipmenttype_desc = shipmenttype.BEZEI;
                    }
                    shipment.route = r.ROUTE;
                    if (r.ROUTE != " ")
                    {
                        var route = context.TVROT.Where(t => t.MANDT == r.MANDT && t.SPRAS == "2" && t.ROUTE == r.ROUTE).FirstOrDefault();
                        shipment.route_desc = route.BEZEI;
                    }
                    shipment.container_id = r.SIGNI;
                    shipment.car_license = r.ADD01;
                    shipment.forwarding = r.TDLNR;
                    Shipments.Add(shipment);
                }
                return Shipments;
            }
        }
        
    }
}