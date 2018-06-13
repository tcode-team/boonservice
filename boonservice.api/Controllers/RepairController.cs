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
        
    }
}