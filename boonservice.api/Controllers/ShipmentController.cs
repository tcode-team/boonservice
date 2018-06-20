using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                                //ค้นหาทะเบียนรถ กับกลุ่มรถ
                                var car = sapcontext.afs_car_license.Where(t => t.CAR_SAP == r.car_license).FirstOrDefault();
                                afs_car_group cargroup;
                                if (car == null)
                                {
                                    //Default cargroup 
                                    cargroup = sapcontext.afs_car_group.Where(t => t.CARGROUP_CODE == "01").FirstOrDefault();
                                } else
                                {
                                    cargroup = sapcontext.afs_car_group.Where(t => t.CARGROUP_CODE == car.CARGROUP_CODE).FirstOrDefault();
                                }
                                r.cargroup_code = cargroup.CARGROUP_CODE;
                                r.cargroup_desc = cargroup.CARGROUP_DESC;
                                result.Add(r);
                            } else
                            {
                                if ((searchvalue.ShipmentStatus == null) || (shipment_h.STATUS == searchvalue.ShipmentStatus))
                                {
                                    var status = sapcontext.afs_shipment_status.Where(t =>
                                                    t.STATUS_CODE == shipment_h.STATUS).FirstOrDefault();
                                    r.transport_date = shipment_h.TRANSPORT_DATE.HasValue ? shipment_h.TRANSPORT_DATE.Value.ToString("dd/MM/yyyy") : string.Empty;
                                    r.driver_id = shipment_h.DRIVER_ID;
                                    r.staff1_id = shipment_h.STAFF1_ID;
                                    r.staff2_id = shipment_h.STAFF2_ID;
                                    r.remark = shipment_h.REMARK;
                                    r.status_code = shipment_h.STATUS;
                                    r.status_desc = status.STATUS_DESC;
                                    r.cargroup_code = shipment_h.CARGROUP_CODE;
                                    if (r.cargroup_code != null)
                                    {
                                        var cargroup = sapcontext.afs_car_group.Where(t => t.CARGROUP_CODE == r.cargroup_code).FirstOrDefault();
                                        r.cargroup_desc = cargroup.CARGROUP_DESC;
                                    }
                                    r.point_id = shipment_h.POINT_ID;
                                    r.confirm_by = shipment_h.CONFIRM_BY;
                                    r.confirm_date = shipment_h.CONFIRM_DATE;
                                    r.created_by = shipment_h.CREATED_BY;
                                    r.created_date = shipment_h.CREATED_DATE;
                                    r.update_by = shipment_h.UPDATE_BY;
                                    r.update_date = shipment_h.UPDATE_DATE;
                                    //result.Add(r);
                                } else
                                {
                                    continue;
                                }

                                //Get Shipment carries
                                r.shipment_carries = new List<ShipmentCarries>();
                                var carries = sapcontext.afs_shipment_carries.Where(t => t.CLIENT == r.client && t.SHIPMENT_NUMBER == r.shipment_number).OrderBy(o => o.ITEM_NO).ToList();
                                foreach (var item in carries)
                                {
                                    r.shipment_carries.Add(new ShipmentCarries
                                    {
                                        client = item.CLIENT,
                                        shipment_number = item.SHIPMENT_NUMBER,
                                        itemno = item.ITEM_NO,
                                        point_desc = item.POINT_DESC,
                                        time_range = item.TIME_RANGE,
                                        so_number = item.SALEORDER_NUMBER,
                                        remark = item.REMARK,
                                        driver_amount = item.DRIVER_AMOUNT,
                                        staff_amount = item.STAFF_AMOUNT,
                                        created_by = item.CREATED_BY,
                                        created_date = item.CREATED_DATE,
                                        update_by = item.UPDATE_BY,
                                        update_date = item.UPDATE_DATE,
                                    });
                                }

                                //Get Shipment expense
                                r.shipment_expense = new List<ShipmentExpense>();
                                var exp = sapcontext.afs_shipment_expense.Where(t => t.CLIENT == r.client && t.SHIPMENT_NUMBER == r.shipment_number).OrderBy(o => o.ITEM_NO).ToList();
                                foreach (var item in exp)
                                {
                                    var expd = sapcontext.afs_expense.Where(t => t.EXPENSE_ID == item.EXPENSE_ID).FirstOrDefault();
                                    r.shipment_expense.Add(new ShipmentExpense
                                    {
                                        client = item.CLIENT,
                                        shipment_number = item.SHIPMENT_NUMBER,
                                        itemno = item.ITEM_NO,
                                        expense_id = item.EXPENSE_ID,
                                        expense_desc = expd != null ? expd.EXPENSE_DESC : string.Empty,
                                        expense_amount = item.EXPENSE_AMOUNT,
                                        remark = item.REMARK,
                                        created_by = item.CREATED_BY,
                                        created_date = item.CREATED_DATE,
                                        update_by = item.UPDATE_BY,
                                        update_date = item.UPDATE_DATE,
                                    });
                                }
                                result.Add(r);
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
                    data.TRANSPORT_DATE = DateTime.ParseExact(postdata.transport_date,"dd/MM/yyyy", null);
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
                    }
                    else
                    {
                        context.afs_shipment_h.Remove(shipment_h);
                        data.CREATED_BY = shipment_h.CREATED_BY;
                        data.CREATED_DATE = shipment_h.CREATED_DATE;
                        data.UPDATE_BY = postdata.update_by;
                        data.UPDATE_DATE = DateTime.Now;
                        context.afs_shipment_h.Add(data);
                    }
                    

                    // Save Shipment carries
                    if (postdata.shipment_carries != null)
                    {
                        foreach (var item in postdata.shipment_carries)
                        {
                            afs_shipment_carries carries = new afs_shipment_carries();
                            carries.CLIENT = postdata.client;
                            carries.SHIPMENT_NUMBER = postdata.shipment_number;
                            carries.ITEM_NO = item.itemno;
                            carries.POINT_DESC = item.point_desc;
                            carries.TIME_RANGE = item.time_range;
                            carries.SALEORDER_NUMBER = item.so_number;
                            carries.REMARK = item.remark;
                            carries.DRIVER_AMOUNT = item.driver_amount;
                            carries.STAFF_AMOUNT = item.staff_amount;
                            var exists = context.afs_shipment_carries.Where(w => 
                                w.CLIENT == carries.CLIENT && 
                                w.SHIPMENT_NUMBER == carries.SHIPMENT_NUMBER && 
                                w.ITEM_NO == carries.ITEM_NO).FirstOrDefault();
                            if (exists == null)
                            {
                                carries.CREATED_BY = postdata.created_by;
                                carries.CREATED_DATE = DateTime.Now;
                                carries.UPDATE_BY = postdata.update_by;
                                carries.UPDATE_DATE = DateTime.Now;
                                context.afs_shipment_carries.Add(carries);
                            } else
                            {
                                context.afs_shipment_carries.Remove(exists);
                                carries.CREATED_BY = shipment_h.CREATED_BY;
                                carries.CREATED_DATE = shipment_h.CREATED_DATE;
                                carries.UPDATE_BY = postdata.update_by;
                                carries.UPDATE_DATE = DateTime.Now;
                                context.afs_shipment_carries.Add(carries);
                            }
                        }
                    }

                    // Save Shipment expense
                    if (postdata.shipment_expense != null)
                    {
                        foreach (var item in postdata.shipment_expense) 
                        {
                            afs_shipment_expense exp = new afs_shipment_expense();
                            exp.CLIENT = postdata.client;
                            exp.SHIPMENT_NUMBER = postdata.shipment_number;
                            exp.ITEM_NO = item.itemno;
                            exp.EXPENSE_ID = item.expense_id;
                            exp.REMARK = item.remark;
                            exp.EXPENSE_AMOUNT = item.expense_amount;
                            var exists = context.afs_shipment_expense.Where(w =>
                                w.CLIENT == exp.CLIENT &&
                                w.SHIPMENT_NUMBER == exp.SHIPMENT_NUMBER &&
                                w.ITEM_NO == exp.ITEM_NO
                            ).FirstOrDefault();
                            if (exists==null)
                            {
                                exp.CREATED_BY = postdata.created_by;
                                exp.CREATED_DATE = DateTime.Now;
                                exp.UPDATE_BY = postdata.update_by;
                                exp.UPDATE_DATE = DateTime.Now;
                                context.afs_shipment_expense.Add(exp);
                            } else
                            {
                                context.afs_shipment_expense.Remove(exists);
                                exp.CREATED_BY = shipment_h.CREATED_BY;
                                exp.CREATED_DATE = shipment_h.CREATED_DATE;
                                exp.UPDATE_BY = postdata.update_by;
                                exp.UPDATE_DATE = DateTime.Now;
                                context.afs_shipment_expense.Add(exp);
                            }
                        }
                    }

                    // Commit data
                    context.SaveChanges();

                    //Search shipment
                    ShipmentSearchModel searh = new ShipmentSearchModel();
                    HttpResponseMessage result = new HttpResponseMessage();
                    searh.ShipmentNo = postdata.shipment_number;
                    result = PostShipmentSearh(searh);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
           
        }

        /// <summary>
        /// Shipment Confirm
        /// </summary>
        /// <param name="postdata">List of Shipment Number</param>
        /// <returns></returns>
        [ResponseType(typeof(IEnumerable<ShipmentDetailModel>))]
        //[Authorize]
        [Route("confirm")]
        public HttpResponseMessage Confirm(List<ShipmentConfirmModeal> postdata)
        {
            try
            {
                foreach (var item in postdata)
                {
                    using (var context = new SAPContext())
                    {
                        var shiph = context.afs_shipment_h.Where(t => t.CLIENT == item.client && t.SHIPMENT_NUMBER == item.shipment_number).FirstOrDefault();
                        if (shiph != null)
                        {
                            context.Entry(shiph).State = EntityState.Modified;
                            shiph.STATUS = "03";
                            shiph.CONFIRM_BY = item.confirm_by;
                            shiph.CONFIRM_DATE = DateTime.Now;
                        }
                        context.SaveChanges();
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK);
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