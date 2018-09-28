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
using boonservice.api.Services;
using Oracle.ManagedDataAccess.Client;

namespace boonservice.api.Controllers
{
    /// <summary>
    /// Repair
    /// </summary>
    /// <remarks></remarks>
    [RoutePrefix("repair")]
    public class RepairController : ApiController
    {
        string client = System.Configuration.ConfigurationManager.AppSettings["client"];

        /// <summary>
        /// รายงานแจ้งซ่อม
        /// </summary>
        /// <remarks>
        /// ค้นหา สำหรับรายงานแจ้งซ่อม
        /// </remarks>
        /// <param name="search">criteria</param>
        /// <returns name="RepairHeader">Repair data</returns>
        /// <response code="200"></response>
        [ResponseType(typeof(IEnumerable<RepairHeader>))]
        [Route("search_list")]
        public HttpResponseMessage search_list(RepairSearchModel search)
        {

            var loadUser = new loadUser();
            var sapSaleOrder = new sapSaleOrder();
            var sapCustomer = new sapCustomer();
            var sapBilling = new sapBilling();
            var BranchService = new sapBranch();

            search.fetchdata = search.fetchdata == null ? new fetchdata() : search.fetchdata;
            var result = new List<RepairHeader>();
            var rh = new RepairHeader();
            ICollection<RepairItems> ri = new List<RepairItems>();
            try
            {

                Expression<Func<afs_repair_header, bool>> RepairDateFromExpression;
                if (!string.IsNullOrEmpty(search.repair_date_from))
                {
                    var datefrom = DateTime.ParseExact(search.repair_date_from, "dd/MM/yyyy", null);
                    //RepairDateFromExpression = gto => gto.REPAIR_DATE.Value.ToString("dd/MM/yyyy").CompareTo(search.repair_date_from) >= 0;
                    RepairDateFromExpression = gto => gto.REPAIR_DATE >= datefrom;
                }
                else
                    RepairDateFromExpression = gto => 1 == 1;

                Expression<Func<afs_repair_header, bool>> RepairDateToExpression;
                if (!string.IsNullOrEmpty(search.repair_date_to))
                {
                    var dateto = DateTime.ParseExact(search.repair_date_to + " 23:59:59", "dd/MM/yyyy HH:mm:ss", null);
                    //RepairDateToExpression = gto => gto.REPAIR_DATE.Value.ToString("dd/MM/yyyy").CompareTo(search.repair_date_to) <= 0;
                    RepairDateToExpression = gto => gto.REPAIR_DATE <= dateto;
                }
                else
                    RepairDateToExpression = gto => 1 == 1;

                Expression<Func<afs_repair_header, bool>> SoldToNameExpression;
                if (!string.IsNullOrEmpty(search.soldto_name))
                {
                    SoldToNameExpression = gto => gto.SOLDTO_NAME.Contains(search.soldto_name);
                }
                else
                    SoldToNameExpression = gto => 1 == 1;

                Expression<Func<afs_repair_header, bool>> SoNumberExpression;
                if (!string.IsNullOrEmpty(search.sonumber))
                {
                    SoNumberExpression = gto => gto.SO_NUMBER == search.sonumber;
                }
                else
                    SoNumberExpression = gto => 1 == 1;

                Expression<Func<afs_repair_header, bool>> RepairStatusExpression;
                if (search.status != "ALL" && !string.IsNullOrEmpty(search.status))
                {
                    RepairStatusExpression = gto => gto.STATUS == search.status;
                }
                else
                    RepairStatusExpression = gto => 1 == 1;

                Expression<Func<afs_repair_header, bool>> RepairCodeExpression;
                if (!string.IsNullOrEmpty(search.repair_code))
                {
                    RepairCodeExpression = gto => gto.REPAIR_CODE == search.repair_code;
                }
                else
                    RepairCodeExpression = gto => 1 == 1;

                using (var context = new SAPContext())
                {
                    var repair_headers = context.afs_repair_header
                        .Where(RepairCodeExpression)
                        .Where(RepairDateFromExpression)
                        .Where(RepairDateToExpression)
                        .Where(SoNumberExpression)
                        .Where(SoldToNameExpression)
                        .Where(RepairStatusExpression)
                        .ToList();

                    foreach (var item in repair_headers)
                    {
                        var vbak = sapSaleOrder.GetVBAK(client, item.SO_NUMBER);

                        rh = new RepairHeader();
                        rh.repair_code = item.REPAIR_CODE;
                        rh.repair_date = item.REPAIR_DATE;
                        rh.so_number = item.SO_NUMBER;
                        rh.soldto_code = item.SOLDTO_CODE;
                        rh.soldto_name = item.SOLDTO_NAME;
                        rh.saleorg = vbak.VKORG;
                        rh.distchannel = vbak.VTWEG;
                        rh.division = vbak.SPART;
                        rh.salegroup = vbak.VKGRP;
                        rh.saleoffice = vbak.VKBUR;

                        var branch = BranchService.GetZBRANCH(client, rh.saleoffice);
                        if (branch != null)
                        {
                            rh.saleoffice_name = branch.HOST_DESC;
                        }

                        //Get Partner Customer
                        var vbpa = sapSaleOrder.GetVBPA(vbak.MANDT, vbak.VBELN, "000000");

                        //Get Sale Rep name
                        var partner = vbpa.Where(w => w.PARVW == "ZR").FirstOrDefault();
                        rh.salerep_code = partner.KUNNR;
                        var salerep = sapCustomer.GetKNA1(partner.MANDT, partner.KUNNR);
                        rh.salerep_name = salerep.NAME1 + salerep.NAME2;

                        rh.status = item.STATUS;
                        rh.transport_amount = item.TRANSPORT_AMOUNT;
                        rh.contact_tel = item.CONTACT_TEL;
                        rh.remark = item.REMARK;
                        rh.created_by = item.CREATED_BY;

                        var ud = loadUser.GetUser(rh.created_by);
                        if (ud != null)
                        {
                            rh.created_name = ud.firstname + ' ' + ud.lastname;
                        };

                        rh.created_date = item.CREATED_DATE;
                        rh.update_by = item.UPDATE_BY;

                        ud = loadUser.GetUser(rh.update_by);
                        if (ud != null)
                        {
                            rh.update_name = ud.firstname + ' ' + ud.lastname;
                        };

                        rh.update_date = item.UPDATE_DATE;
                        result.Add(rh);
                    }

                }
            
                return result.Count() == 0 
                         ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "Repair data not found")
                         : Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.InnerException.Message);
            }

        }

        /// <summary>
        /// ค้นหา เพิ่มงานซ่อม SO
        /// </summary>
        /// <remarks>
        /// ค้นหา SO สำหรับหน้าจอ เพิ่มงานซ่อม SO
        /// </remarks>
        /// <param name="search">criteria</param>
        /// <returns name="RepairHeader">Repair data</returns>
        /// <response code="200"></response>
        [ResponseType(typeof(RepairForm))]
        [Route("searchso")]
        public HttpResponseMessage PostSearh(RepairSearchModel search)
        {
            var sapSaleOrder = new sapSaleOrder();
            var sapCustomer = new sapCustomer();
            var sapBilling = new sapBilling();

            search.fetchdata = search.fetchdata == null ? new fetchdata() : search.fetchdata;
            var result = new RepairForm();
            var rh = new RepairHeader();
            ICollection<RepairItems> ri = new List<RepairItems>();
            try
            {
                var vbak = sapSaleOrder.GetVBAK(client, search.sonumber);
                if (vbak == null || vbak.VBTYP != "C")
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "VBAK not found");
                };
                rh.so_number = vbak.VBELN;
                rh.so_date = DateTime.ParseExact(vbak.ERDAT, "yyyyMMdd", null);
                rh.soldto_code = vbak.KUNNR;

                //Get Customer Detail
                var kna1 = sapCustomer.GetKNA1(vbak.MANDT, vbak.KUNNR);
                rh.soldto_name = kna1.NAME1 + kna1.NAME2;

                //Get Customer Address 
                var adrc = sapCustomer.GetADRC(kna1.MANDT, kna1.ADRNR);
                rh.soldto_address = sapCustomer.ConcateAddress(adrc.Where(w => w.NATION == " ").FirstOrDefault());

                //Get Partner Customer
                var vbpa = sapSaleOrder.GetVBPA(vbak.MANDT, vbak.VBELN, "000000");

                //Ship-To Address
                var partner = vbpa.Where(w => w.PARVW == "WE").FirstOrDefault();
                rh.shipto_code = partner.KUNNR;
                kna1 = sapCustomer.GetKNA1(vbak.MANDT, partner.KUNNR);
                adrc = sapCustomer.GetADRC(kna1.MANDT, kna1.ADRNR);
                rh.shipto_address = sapCustomer.ConcateAddress(adrc.Where(w => w.NATION == " ").FirstOrDefault());

                //Get Sale Rep name
                partner = vbpa.Where(w => w.PARVW == "ZR").FirstOrDefault();
                rh.salerep_code = partner.KUNNR;
                var salerep = sapCustomer.GetKNA1(partner.MANDT, partner.KUNNR);
                rh.salerep_name = salerep.NAME1 + salerep.NAME2;

                //Get Sale Order item
                var vbap = sapSaleOrder.GetVBAP(vbak.MANDT, vbak.VBELN);
                foreach (var item in vbap)
                {
                    ri.Add(new RepairItems
                    {
                        select = false,
                        so_number = item.VBELN,
                        so_item = item.POSNR,
                        article_number = item.MATNR,
                        article_name = item.ARKTX,
                        qty = item.KWMENG
                    });
                }

                //Get Billing
                var vbrks = sapBilling.GetVBRKbySONumber(vbak.MANDT, vbak.VBELN);
                if (vbrks == null || vbrks.Count == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Billing not found");
                }
                var vbrk = vbrks.OrderBy(o => o.FKDAT).FirstOrDefault();
                rh.billing_date = DateTime.ParseExact(vbrk.FKDAT, "yyyyMMdd", null);

                result.header = rh;
                result.items = ri;

                return result.header == null || result.items == null
                         ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "Sale Order not found")
                         : Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
            
        }

        /// <summary>
        /// ค้นหา งานซ่อมด้วยเลขที่แจ้งซ่อม
        /// </summary>
        /// <remarks>
        /// ค้นหา งานซ่อมด้วยเลขที่แจ้งซ่อม
        /// </remarks>
        /// <param name="repair_code">เลขที่แจ้งซ่อม</param>
        /// <returns name="RepairForm">Repair data</returns>
        /// <response code="200"></response>
        [ResponseType(typeof(RepairForm))]
        [Route("detail")]
        public HttpResponseMessage detail(string repair_code)
        {
            //Service
            var b3gRepair = new b3gRepair();
            var sapSaleOrder = new sapSaleOrder();
            var sapCustomer = new sapCustomer();
            var sapBilling = new sapBilling();

            var result = new RepairForm();
            var rh = new RepairHeader();
            var rp = new RepairAppointment();
            ICollection<RepairItems> ri = new List<RepairItems>();
            ICollection<RepairRaw> rr = new List<RepairRaw>();
            try
            {
                afs_repair_header h = b3gRepair.GetRepairHeader(repair_code);
                if (h == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Repair data not found");
                };

                rh.repair_code = h.REPAIR_CODE;
                rh.repair_date = h.REPAIR_DATE;
                rh.so_number = h.SO_NUMBER;
                rh.soldto_code = h.SOLDTO_CODE;
                rh.soldto_name = h.SOLDTO_NAME;
                rh.status = h.STATUS;
                rh.transport_amount = h.TRANSPORT_AMOUNT;
                rh.contact_tel = h.CONTACT_TEL;
                rh.remark = h.REMARK;
                rh.created_by = h.CREATED_BY;
                rh.created_date = h.CREATED_DATE;
                rh.update_by = h.UPDATE_BY;
                rh.update_date = h.UPDATE_DATE;

                var vbak = sapSaleOrder.GetVBAK(client, h.SO_NUMBER);
                rh.so_date = DateTime.ParseExact(vbak.ERDAT, "yyyyMMdd", null);
                //rh.soldto_code = vbak.KUNNR;

                ////Get Customer Detail
                var kna1 = sapCustomer.GetKNA1(vbak.MANDT, rh.soldto_code);
                //rh.soldto_name = kna1.NAME1 + kna1.NAME2;

                //Get Customer Address 
                var adrc = sapCustomer.GetADRC(kna1.MANDT, kna1.ADRNR);
                rh.soldto_address = sapCustomer.ConcateAddress(adrc.Where(w => w.NATION == " ").FirstOrDefault());

                //Get Partner Customer
                var vbpa = sapSaleOrder.GetVBPA(vbak.MANDT, vbak.VBELN, "000000");

                //Ship-To Address
                var partner = vbpa.Where(w => w.PARVW == "WE").FirstOrDefault();
                rh.shipto_code = partner.KUNNR;
                kna1 = sapCustomer.GetKNA1(vbak.MANDT, partner.KUNNR);
                adrc = sapCustomer.GetADRC(kna1.MANDT, kna1.ADRNR);
                rh.shipto_address = sapCustomer.ConcateAddress(adrc.Where(w => w.NATION == " ").FirstOrDefault());

                //Get Sale Rep name
                partner = vbpa.Where(w => w.PARVW == "ZR").FirstOrDefault();
                rh.salerep_code = partner.KUNNR;
                var salerep = sapCustomer.GetKNA1(partner.MANDT, partner.KUNNR);
                rh.salerep_name = salerep.NAME1 + salerep.NAME2;

                //Get Billing
                var vbrks = sapBilling.GetVBRKbySONumber(vbak.MANDT, vbak.VBELN);
                var vbrk = vbrks.OrderBy(o => o.FKDAT).FirstOrDefault();
                rh.billing_date = DateTime.ParseExact(vbrk.FKDAT, "yyyyMMdd", null);

                ICollection<afs_repair_items> items = b3gRepair.GetRepairItem(repair_code);
                if (items == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Repair item data not found");
                };
               
                //Items
                foreach (var item in items)
                {
                    //Get Sale Order item
                    var vbap = sapSaleOrder.GetVBAP(vbak.MANDT, item.SO_NUMBER, item.SO_ITEM );
                    if (vbap == null) vbap = new VBAP();

                    //Get Images
                    List<RepairImages> ims = new List<RepairImages>();
                    var images = b3gRepair.GetRepairImage(item.REPAIR_ITEM_ID);
                    if (images == null) images = new List<afs_repair_images>();
                    foreach (var img in images)
                    {
                        var im = new RepairImages();
                        im.repair_image_id = img.REPAIR_IMAGE_ID;
                        im.repair_code = img.REPAIR_CODE;
                        im.repair_item_id = img.REPAIR_ITEM_ID;
                        im.so_number = img.SO_NUMBER;
                        im.so_item = img.SO_ITEM;
                        im.filename = img.FILENAME;
                        im.base64 = img.BASE64;
                        ims.Add(im);
                    }

                    ri.Add(new RepairItems
                    {
                        select = true,
                        repair_item_id = item.REPAIR_ITEM_ID,
                        repair_code = item.REPAIR_CODE,
                        so_number = item.SO_NUMBER,
                        so_item = item.SO_ITEM,
                        article_number = vbap.MATNR,
                        article_name = vbap.ARKTX,
                        repair_item_type = item.REPAIR_ITEM_TYPE,
                        qty = item.REPAIR_QTY,
                        repair_remark = item.REPAIR_REMARK,
                        repair_desc = item.REPAIR_DESC,
                        repair_type = item.REPAIR_TYPE,
                        waranty = item.WARANTY,
                        images = ims
                    });
                }

                //Appointment
                afs_repair_appointment p = b3gRepair.GetRepairAppointment(repair_code);
                if (p != null)
                {
                    rp.repair_code = p.REPAIR_CODE;
                    rp.repair_appoint_id = p.REPAIR_APPOINT_ID;
                    rp.appointment_date = p.APPOINTMENT_DATE;
                    rp.appointment_time = p.APPOINTMENT_TIME;
                    rp.target_date = p.TARGET_DATE;
                    rp.technician_team = p.TECHNICIAN_TEAM;
                    rp.price_amount = p.PRICE_AMOUNT;
                    rp.price_extra = p.PRICE_EXTRA;
                    rp.remark_customer = p.REMARK_CUSTOMER;
                    rp.created_by = p.CREATED_BY;
                    rp.created_date = p.CREATED_DATE;
                    rp.update_by = p.UPDATE_BY;
                    rp.update_date = p.UPDATE_DATE;
                };

                //Raw
                ICollection<afs_repair_raw> raws = b3gRepair.GetRepairRaw(repair_code);
                if (raws != null)
                {
                    foreach (var raw in raws)
                    {
                        rr.Add(new RepairRaw
                        {
                            repair_raw_id = raw.REPAIR_RAW_ID,
                            repair_code = raw.REPAIR_CODE,
                            raw_name = raw.RAW_NAME,
                            raw_qty = raw.RAW_QTY,
                            status = raw.STATUS,
                            raw_date = raw.RAW_DATE,
                            remark = raw.REMARK,
                            created_by = raw.CREATED_BY,
                            created_date = raw.CREATED_DATE,
                            update_by = raw.UPDATE_BY,
                            update_date = raw.UPDATE_DATE
                        });
                    }
                }

                result.header = rh;
                result.items = ri;
                result.appoint = rp;
                result.raws = rr;

                return result.header == null || result.items == null
                         ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "Repair data not found")
                         : Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

        }

        /// <summary>
        /// บันทึก เพิ่มงานซ่อม SO
        /// </summary>
        /// <remarks>
        /// บันทึก เพิ่มงานซ่อม SO
        /// </remarks>
        /// <param name="postdata">Repair Form data</param>
        /// <returns name="RepairHeader">Repair data</returns>
        /// <response code="200"></response>
        [ResponseType(typeof(RepairForm))]
        [Authorize]
        [Route("save")]
        public HttpResponseMessage Save(RepairForm postdata)
        {
            try
            {
                var b3gRepair = new b3gRepair();

                using (var context = new SAPContext())
                {
                    afs_repair_header h = new afs_repair_header();
                    h = context.afs_repair_header.Where(t => t.REPAIR_CODE == postdata.header.repair_code).FirstOrDefault();
                    if (h != null)
                    {
                        context.Entry(h).State = EntityState.Modified;
                    } else h = new afs_repair_header();
                    h.TRANSPORT_AMOUNT = postdata.header.transport_amount;
                    h.CONTACT_TEL = postdata.header.contact_tel;
                    h.REMARK = postdata.header.remark;
                    h.STATUS = postdata.header.status;
                    h.UPDATE_BY = postdata.header.update_by;
                    h.UPDATE_DATE = DateTime.Now;
                    if (string.IsNullOrEmpty(h.REPAIR_CODE))
                    {
                        h.REPAIR_CODE = b3gRepair.GetNextDocumentNumber();
                        h.REPAIR_DATE = DateTime.Now;
                        h.SO_NUMBER = postdata.header.so_number;
                        h.SOLDTO_CODE = postdata.header.soldto_code;
                        h.SOLDTO_NAME = postdata.header.soldto_name;
                        h.CREATED_BY = postdata.header.created_by;
                        h.CREATED_DATE = DateTime.Now;
                        context.afs_repair_header.Add(h);
                    }
                    context.SaveChanges();

                    //Set return value
                    postdata.header.repair_code = h.REPAIR_CODE;
                    postdata.header.repair_date = h.REPAIR_DATE;
                    postdata.header.created_by = h.CREATED_BY;
                    postdata.header.created_date = h.CREATED_DATE;
                    postdata.header.update_by = h.UPDATE_BY;
                    postdata.header.update_date = h.UPDATE_DATE;

                    List<afs_repair_items> i = new List<afs_repair_items>();
                    foreach (var item in postdata.items)
                    {
                        afs_repair_items v = new afs_repair_items();
                        v = context.afs_repair_items.Where(t => t.REPAIR_ITEM_ID == item.repair_item_id).FirstOrDefault();
                        if (v != null)
                        {
                            context.Entry(v).State = EntityState.Modified;
                        }
                        else v = new afs_repair_items();
                        v.REPAIR_ITEM_TYPE = item.repair_item_type;
                        v.REPAIR_QTY = item.qty;
                        v.REPAIR_REMARK = item.repair_remark;
                        v.REPAIR_DESC = item.repair_desc;
                        v.REPAIR_TYPE = item.repair_type;
                        v.WARANTY = item.waranty;
                        v.UPDATE_BY = postdata.header.update_by;
                        v.UPDATE_DATE = DateTime.Now;

                        if (item.repair_item_id == 0)
                        {
                            v.REPAIR_CODE = h.REPAIR_CODE;
                            v.SO_NUMBER = item.so_number;
                            v.SO_ITEM = item.so_item;
                            v.CREATED_BY = postdata.header.created_by;
                            v.CREATED_DATE = DateTime.Now;
                            context.afs_repair_items.Add(v);
                        }
                        context.SaveChanges();

                        //Set return value
                        item.select = true;
                        item.repair_item_id = v.REPAIR_ITEM_ID;
                        item.repair_code = v.REPAIR_CODE;

                        if (item.images != null)
                        {
                            List<afs_repair_images> m = new List<afs_repair_images>();
                            foreach (var img in item.images)
                            {
                                afs_repair_images im = new afs_repair_images();
                                im = context.afs_repair_images.Where(t => t.REPAIR_IMAGE_ID == img.repair_image_id).FirstOrDefault();
                                if (im != null)
                                {
                                    context.Entry(im).State = EntityState.Modified;
                                }
                                else im = new afs_repair_images();
                                im.FILENAME = img.filename;
                                im.BASE64 = img.base64;
                                if (img.repair_image_id==0)
                                {
                                    im.REPAIR_CODE = h.REPAIR_CODE;
                                    im.REPAIR_ITEM_ID = v.REPAIR_ITEM_ID;
                                    im.SO_NUMBER = item.so_number;
                                    im.SO_ITEM = item.so_item;
                                    context.afs_repair_images.Add(im);
                                }
                                context.SaveChanges();

                                //Set return value
                                img.repair_image_id = im.REPAIR_IMAGE_ID;
                                img.repair_code = im.REPAIR_CODE;
                                img.repair_item_id = im.REPAIR_ITEM_ID;
                            }
                        }
                    };

                }
                
                return Request.CreateResponse(HttpStatusCode.OK, postdata);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// บันทึก นัดหมายลูกค้า
        /// </summary>
        /// <remarks>
        /// บันทึก เพิ่มงานซ่อม SO
        /// </remarks>
        /// <param name="postdata">Repair Form data</param>
        /// <returns name="RepairHeader">Repair data</returns>
        /// <response code="200"></response>
        [ResponseType(typeof(RepairForm))]
        [Authorize]
        [Route("save_appointment")]
        public HttpResponseMessage SaveAppointment(RepairForm postdata)
        {
            try
            {
                var b3gRepair = new b3gRepair();

                using (var context = new SAPContext())
                {
                    afs_repair_header h = new afs_repair_header();
                    h = context.afs_repair_header.Where(t => t.REPAIR_CODE == postdata.header.repair_code).FirstOrDefault();
                    if (h != null)
                    {
                        context.Entry(h).State = EntityState.Modified;
                    }
                    else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Repair data not found");

                    //set status
                    h.STATUS = postdata.header.status;
                    if (postdata.header.status != "COMPLETE")
                    {   
                        if (postdata.raws.Where(w => w.status == "มีอะไหล่").Count() == postdata.raws.Count())
                        {
                            h.STATUS = "PROCESS";
                        }
                        else
                        {
                            h.STATUS = "PREPARE";
                        };
                    };

                    h.UPDATE_BY = postdata.header.update_by;
                    h.UPDATE_DATE = DateTime.Now;
                    context.SaveChanges();

                    //Set return value
                    postdata.header.update_by = h.UPDATE_BY;
                    postdata.header.update_date = h.UPDATE_DATE;
                    
                    //นัดหมายลูกค้า
                    if (postdata.appoint != null)
                    {
                        afs_repair_appointment p = new afs_repair_appointment();
                        p = context.afs_repair_appointment.Where(t => t.REPAIR_APPOINT_ID == postdata.appoint.repair_appoint_id).FirstOrDefault();
                        if (p != null)
                        {
                            context.Entry(p).State = EntityState.Modified;
                        }
                        else p = new afs_repair_appointment();
                        p.APPOINTMENT_DATE = postdata.appoint.appointment_date; //DateTime.ParseExact(postdata.appoint.appointment_date, "dd/MM/yyyy", null);
                        p.APPOINTMENT_TIME = postdata.appoint.appointment_time;
                        p.TARGET_DATE = postdata.appoint.target_date; // DateTime.ParseExact(postdata.appoint.target_date, "dd/MM/yyyy", null);
                        p.TECHNICIAN_TEAM = postdata.appoint.technician_team;
                        p.PRICE_AMOUNT = postdata.appoint.price_amount;
                        p.PRICE_EXTRA = postdata.appoint.price_extra;
                        p.REMARK_CUSTOMER = postdata.appoint.remark_customer;
                        p.UPDATE_BY = postdata.header.update_by;
                        p.UPDATE_DATE = DateTime.Now;
                        if (p.REPAIR_APPOINT_ID == 0)
                        {
                            p.REPAIR_CODE = postdata.header.repair_code;
                            p.CREATED_BY = postdata.header.created_by;
                            p.CREATED_DATE = DateTime.Now;
                            context.afs_repair_appointment.Add(p);
                        }
                        context.SaveChanges();

                        //Set return value
                        postdata.appoint.repair_appoint_id = p.REPAIR_APPOINT_ID;
                        postdata.appoint.repair_code = postdata.header.repair_code;
                    }

                    List<afs_repair_raw> i = new List<afs_repair_raw>();
                    foreach (var item in postdata.raws)
                    {
                        afs_repair_raw v = new afs_repair_raw();
                        v = context.afs_repair_raw.Where(t => t.REPAIR_RAW_ID == item.repair_raw_id).FirstOrDefault();
                        if (v != null)
                        {
                            context.Entry(v).State = EntityState.Modified;
                        }
                        else v = new afs_repair_raw();
                        v.RAW_NAME = item.raw_name;
                        v.RAW_QTY = item.raw_qty;
                        v.STATUS = item.status;
                        if (item.raw_date.HasValue)
                        {
                            v.RAW_DATE = item.raw_date.Value; //DateTime.ParseExact(item.raw_date, "dd/MM/yyyy", null);
                        };
                        v.REMARK = item.remark;
                        v.UPDATE_BY = postdata.header.update_by;
                        v.UPDATE_DATE = DateTime.Now;

                        if (item.repair_raw_id == 0)
                        {
                            v.REPAIR_CODE = postdata.header.repair_code;
                            v.CREATED_BY = postdata.header.created_by;
                            v.CREATED_DATE = DateTime.Now;
                            context.afs_repair_raw.Add(v);
                        }
                        context.SaveChanges();

                        //Set return value
                        item.repair_raw_id = v.REPAIR_RAW_ID;
                        item.repair_code = postdata.header.repair_code;
                    };
                }

                return Request.CreateResponse(HttpStatusCode.OK, postdata);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Remove item of raw
        /// </summary>
        /// <param name="raw_id"></param>
        /// <returns></returns>
        /// <response code="200"></response>
        [Authorize]
        [Route("remove_raw")]
        public HttpResponseMessage RemoveRaw(double raw_id)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    afs_repair_raw v = new afs_repair_raw();
                    v = context.afs_repair_raw.Where(t => t.REPAIR_RAW_ID == raw_id).FirstOrDefault();
                    if (v != null)
                    {
                        context.Entry(v).State = EntityState.Deleted;
                        context.SaveChanges();
                    } else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Raw id is not found" );
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.InnerException.Message);
            }
        }
    }
}