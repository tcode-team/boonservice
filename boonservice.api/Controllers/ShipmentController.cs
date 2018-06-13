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
    /// <summary>
    /// Shipment
    /// </summary>
    /// <remarks></remarks>
    [RoutePrefix("shipment")]
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
        [Authorize]
        [Route("search")]
        public HttpResponseMessage PostShipmentSearh(ShipmentSearchModel searchvalue)
        {
            searchvalue.fetchdata = searchvalue.fetchdata == null ? new fetchdata() : searchvalue.fetchdata;

            var result = new List<ShipmentDetailModel>();
            var Shipments = new List<ShipmentDetailModel>();

            Expression<Func<VTTK, bool>> ShipmentDateFromExpression;
            if (searchvalue.ShipmentDateFrom != null)
            {
                var datefrom = DateTime.ParseExact(searchvalue.ShipmentDateFrom, "dd/MM/yyyy", null).Date.ToString("yyyyMMdd");
                ShipmentDateFromExpression = gto => gto.ERDAT.CompareTo(datefrom) >= 0;
            }
            else
                ShipmentDateFromExpression = gto => 1 == 1;

            Expression<Func<VTTK, bool>> ShipmentDateToExpression;
            if (searchvalue.ShipmentDateFrom != null)
            {
                var dateto = DateTime.ParseExact(searchvalue.ShipmentDateTo, "dd/MM/yyyy", null).Date.ToString("yyyyMMdd");
                ShipmentDateToExpression = gto => gto.ERDAT.CompareTo(dateto) <= 0;
            }
            else
                ShipmentDateToExpression = gto => 1 == 1;

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

            Expression<Func<afs_shipment_h, bool>> ShipmentStatusExpression;
            if (searchvalue.ShipmentStatus != "ALL" && searchvalue.ShipmentStatus != null)
            {
                ShipmentStatusExpression = gto => gto.STATUS == searchvalue.ShipmentStatus;
            }
            else
                ShipmentStatusExpression = gto => 1 == 1;

            using (var context = new SAPSR3Context())
            {
                var vttks = context.VTTK
                    .Where(t => t.MANDT == client)
                    .Where(ShipmentNumberExpression)
                    .Where(ShipmentDateFromExpression)
                    .Where(ShipmentDateToExpression)
                    .Where(ShipmentTypeExpression)
                    .Where(forwardingExpression)
                    .Where(CarLicenseExpression)
                    .OrderBy(t => t.TKNUM)
                    .Skip(searchvalue.fetchdata.after == 0 ? 0 : searchvalue.fetchdata.after)
                    .Take(searchvalue.fetchdata.size == 0 ? 500 : searchvalue.fetchdata.size)
                    .ToList();
                Shipments = MappingShipmentDetail(vttks);

                foreach (ShipmentDetailModel r in Shipments)
                {
                    using (var sapcontext = new SAPContext())
                    {
                        var shipment_h = sapcontext.afs_shipment_h.Where(t => 
                                            t.CLIENT == r.client && 
                                            t.SHIPMENT_NUMBER == r.shipment_number)
                                         .FirstOrDefault();
                        if (shipment_h == null && (searchvalue.ShipmentStatus != "ALL" && searchvalue.ShipmentStatus != "01" && searchvalue.ShipmentStatus != null))
                        {
                            continue;
                        } else
                        {
                            if (shipment_h == null && (searchvalue.ShipmentStatus == "ALL" || searchvalue.ShipmentStatus == "01" || searchvalue.ShipmentStatus == null))
                            {
                                var status = sapcontext.afs_shipment_status.Where(t =>
                                                    t.STATUS_CODE == "01").FirstOrDefault();
                                r.status_code = "01";
                                r.status_desc = status.STATUS_DESC;
                                result.Add(r);
                            } else
                            {
                                if (shipment_h.STATUS == searchvalue.ShipmentStatus)
                                {
                                    var status = sapcontext.afs_shipment_status.Where(t =>
                                                    t.STATUS_CODE == shipment_h.STATUS).FirstOrDefault();
                                    r.status_code = shipment_h.STATUS;
                                    r.status_desc = status.STATUS_DESC;
                                    r.cargroup_code = shipment_h.CARGROUP_CODE;
                                    if (r.cargroup_code != null)
                                    {
                                        var cargroup = sapcontext.afs_car_group.Where(t => t.CARGROUP_CODE == r.cargroup_code).FirstOrDefault();
                                        r.cargroup_desc = cargroup.CARGROUP_DESC;
                                    }
                                    result.Add(r);
                                } else
                                {
                                    continue;
                                }
                            }
                            
                        }
                    }
                }  

                return result == null || result.Count == 0
                    ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "Shipment List not found")
                    : Request.CreateResponse(HttpStatusCode.OK, result);
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
        [Authorize]
        [Route("getall")]
        public HttpResponseMessage Get(fetchdata fetchdata)
        {
            fetchdata = fetchdata == null ? new fetchdata() : fetchdata;

            var Shipments = new List<ShipmentDetailModel>();
            using (var context = new SAPSR3Context())
            {
                var vttks = context.VTTK
                    .Where(w => w.MANDT == client)
                    .OrderBy(t => t.TKNUM)
                    .Skip(fetchdata.after == 0 ? 0 : fetchdata.after)
                    .Take(fetchdata.size == 0 ? 100 : fetchdata.size)
                    .ToList();
                Shipments = MappingShipmentDetail(vttks);

                foreach (ShipmentDetailModel r in Shipments)
                {
                    using (var sapcontext = new SAPContext())
                    {
                        var shipment_h = sapcontext.afs_shipment_h.Where(t =>
                                            t.CLIENT == r.client &&
                                            t.SHIPMENT_NUMBER == r.shipment_number)
                                         .FirstOrDefault();
                        if (shipment_h == null)
                        {
                            var status = sapcontext.afs_shipment_status.Where(t =>
                                                t.STATUS_CODE == "01").FirstOrDefault();
                            r.status_code = "01";
                            r.status_desc = status.STATUS_DESC;
                        }
                        else
                        {
                            var status = sapcontext.afs_shipment_status.Where(t =>
                                            t.STATUS_CODE == shipment_h.STATUS).FirstOrDefault();
                            r.status_code = shipment_h.STATUS;
                            r.status_desc = status.STATUS_DESC;
                        }
                    }
                }

                return Shipments == null
                    ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "Shipment not found")
                    : Request.CreateResponse(HttpStatusCode.OK, Shipments);
            }
        }

        /// <summary>
        /// บันทึกคำนวณค่าขนส่ง
        /// </summary>
        /// <remarks>
        /// บันทึกคำนวณค่าขนส่ง
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [ResponseType(typeof(IEnumerable<ShipmentDetailModel>))]
        [Authorize]
        [Route("save")]
        public HttpResponseMessage Save(ShipmentDetailModel postdata)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    afs_shipment_h data = new afs_shipment_h();
                    data.CLIENT = postdata.client;
                    data.SHIPMENT_NUMBER = postdata.shipment_number;
                    data.TRANSPORT_DATE = DateTime.ParseExact(postdata.transport_date, "dd/MM/yyyy", null);
                    data.CARGROUP_CODE = postdata.cargroup_code;
                    data.DRIVER_ID = postdata.driver_id;
                    data.STAFF1_ID = postdata.staff1_id;
                    data.STAFF2_ID = postdata.staff2_id;
                    data.STATUS = postdata.status_code;
                    data.REMARK = postdata.remark;
                    data.POINT_ID = postdata.point_id;
                    var shipment_h = context.afs_shipment_h.Where(t => t.CLIENT == postdata.client && t.SHIPMENT_NUMBER == postdata.shipment_number).FirstOrDefault();
                    if (shipment_h == null)
                    {
                        data.CREATED_BY = postdata.created_by;
                        data.CREATED_DATE = DateTime.Now;
                        data.UPDATE_BY = postdata.update_by;
                        data.UPDATE_DATE = DateTime.Now;
                        context.afs_shipment_h.Add(data);
                        context.SaveChanges();
                    }
                    else
                    {
                        context.afs_shipment_h.Remove(shipment_h);
                        data.CREATED_BY = shipment_h.CREATED_BY;
                        data.CREATED_DATE = shipment_h.CREATED_DATE;
                        data.UPDATE_BY = postdata.update_by;
                        data.UPDATE_DATE = DateTime.Now;
                        context.afs_shipment_h.Add(data);
                        context.SaveChanges();
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
           
        }


        private List<ShipmentDetailModel> MappingShipmentDetail(List<VTTK> vttks)
        {
            var Shipments = new List<ShipmentDetailModel>();
            using (var context = new SAPSR3Context())
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