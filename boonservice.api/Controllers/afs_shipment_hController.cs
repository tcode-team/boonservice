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
    /// For Table afs_shipment_h
    /// </summary>
    /// <remarks>
    /// Method about table of afs_shipment_h
    /// </remarks>
    [RoutePrefix("afs_shipment_h")]
    public class afs_shipment_hController : ApiController
    {
        /// <summary>
        /// Get all afs_shipment_h
        /// </summary>
        /// <remarks>
        /// Get a list of all afs_shipment_h
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="404">Data not found</response>
        [ResponseType(typeof(IEnumerable<afs_shipment_h>))]
        [Authorize]
        [Route("get")]
        public HttpResponseMessage Get()
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var afs_shipment_h = context.afs_shipment_h.ToList();
                    return afs_shipment_h == null
                        ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_shipment_expense not found")
                        : Request.CreateResponse(HttpStatusCode.OK, afs_shipment_h);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get afs_shipment_h by client, shipment_number
        /// </summary>
        /// <remarks>
        /// Get a afs_shipment_h by client, shipment_number
        /// </remarks>
        /// <param name="postdata">afs_shipment_h-client, shipment_number</param>
        /// <returns></returns>
        /// <response code="200">afs_shipment_h found</response>
        /// <response code="404">afs_shipment_h not foundd</response>
        [ResponseType(typeof(afs_shipment_h))]
        public HttpResponseMessage GetById(afs_shipment_h postdata)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var afs_shipment_h = context.afs_shipment_h.Where(t => t.CLIENT == postdata.CLIENT && t.SHIPMENT_NUMBER == postdata.SHIPMENT_NUMBER).FirstOrDefault();
                    return afs_shipment_h == null
                        ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_shipment_expense not found")
                        : Request.CreateResponse(HttpStatusCode.OK, afs_shipment_h);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Add new afs_shipment_h
        /// </summary>
        /// <remarks>
        /// Add a new afs_shipment_h
        /// </remarks>
        /// <param name="post_afs_shipment_h">afs_shipment_h to add</param>
        /// <returns></returns>
        /// <response code="201">afs_shipment_h created</response>
        [Authorize]
        [ResponseType(typeof(afs_shipment_h))]
        public HttpResponseMessage Post(afs_shipment_h post_afs_shipment_h)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    afs_shipment_h data = new afs_shipment_h();
                    data.CLIENT = post_afs_shipment_h.CLIENT;
                    data.SHIPMENT_NUMBER = post_afs_shipment_h.SHIPMENT_NUMBER;
                    data.TRANSPORT_DATE = post_afs_shipment_h.TRANSPORT_DATE;
                    data.CARGROUP_CODE = post_afs_shipment_h.CARGROUP_CODE;
                    data.DRIVER_ID = post_afs_shipment_h.DRIVER_ID;
                    data.REMARK = post_afs_shipment_h.REMARK;
                    data.DRIVER_ID = post_afs_shipment_h.DRIVER_ID;
                    data.STAFF1_ID = post_afs_shipment_h.STAFF1_ID;
                    data.STAFF2_ID = post_afs_shipment_h.STAFF2_ID;
                    data.STATUS = post_afs_shipment_h.STATUS;
                    data.CREATED_BY = post_afs_shipment_h.CREATED_BY;
                    data.CREATED_DATE = DateTime.Now;
                    data.UPDATE_BY = post_afs_shipment_h.UPDATE_BY;
                    data.UPDATE_DATE = DateTime.Now;
                    context.afs_shipment_h.Add(data);
                    context.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update an existing afs_shipment_h
        /// </summary>
        /// <param name="put_afs_shipment_h">afs_shipment_h to update</param>
        /// <returns></returns>
        /// <response code="200">afs_shipment_h updated</response>
        /// <response code="404">afs_shipment_h not found</response>")]
        [Authorize]
        [ResponseType(typeof(afs_shipment_h))]
        public HttpResponseMessage Put(afs_shipment_h put_afs_shipment_h)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var existing = context.afs_shipment_h.Where(t => t.CLIENT == put_afs_shipment_h.CLIENT && t.SHIPMENT_NUMBER == put_afs_shipment_h.SHIPMENT_NUMBER).FirstOrDefault();
                    if (existing == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_shipment_expense not found");

                    context.afs_shipment_h.Remove(existing);
                    context.afs_shipment_h.Add(put_afs_shipment_h);
                    context.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, put_afs_shipment_h);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete a afs_shipment_h
        /// </summary>
        /// <remarks>
        /// Delete a afs_shipment_h
        /// </remarks>
        /// <param name="postdata">client,shipment_number of the afs_shipment_h to delete</param>
        /// <returns></returns>
        [Authorize]
        public HttpResponseMessage Delete(afs_shipment_h postdata)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var existing = context.afs_shipment_h.Where(t => t.CLIENT == postdata.CLIENT && t.SHIPMENT_NUMBER == postdata.SHIPMENT_NUMBER).FirstOrDefault();
                    if (existing == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_shipment_expense not found");

                    context.afs_shipment_h.Remove(existing);
                    context.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}