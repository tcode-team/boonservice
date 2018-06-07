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
    /// For Table afs_shipment_status
    /// </summary>
    /// <remarks>
    /// Method about table of afs_shipment_status
    /// </remarks>
    [RoutePrefix("afs_shipment_status")]
    public class afs_shipment_statusController : ApiController
    {
        /// <summary>
        /// Get all afs_shipment_status
        /// </summary>
        /// <remarks>
        /// Get a list of all afs_shipment_status
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="404">Data not found</response>
        [ResponseType(typeof(IEnumerable<afs_shipment_status>))]
        [Authorize]
        [Route("get")]
        public HttpResponseMessage Get()
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var afs_shipment_status = context.afs_shipment_status.ToList();
                    return afs_shipment_status == null
                        ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_shipment_status not found")
                        : Request.CreateResponse(HttpStatusCode.OK, afs_shipment_status);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get afs_shipment_status by code
        /// </summary>
        /// <remarks>
        /// Get a afs_shipment_status by code
        /// </remarks>
        /// <param name="code">afs_shipment_status-status_code</param>
        /// <returns></returns>
        /// <response code="200">afs_shipment_status found</response>
        /// <response code="404">afs_shipment_status not found</response>
        [ResponseType(typeof(afs_shipment_status))]
        public HttpResponseMessage GetById(string code)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var afs_shipment_status = context.afs_shipment_status.Where(t => t.STATUS_CODE == code).FirstOrDefault();
                    return afs_shipment_status == null
                        ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_shipment_status not found")
                        : Request.CreateResponse(HttpStatusCode.OK, afs_shipment_status);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Add new afs_shipment_status
        /// </summary>
        /// <remarks>
        /// Add a new afs_shipment_status
        /// </remarks>
        /// <param name="post_afs_shipment_status">afs_shipment_status to add</param>
        /// <returns></returns>
        /// <response code="201">afs_shipment_status created</response>
        [Authorize]
        [ResponseType(typeof(afs_shipment_status))]
        public HttpResponseMessage Post(afs_shipment_status post_afs_shipment_status)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    afs_shipment_status data = new afs_shipment_status();
                    data.STATUS_CODE = post_afs_shipment_status.STATUS_CODE;
                    data.STATUS_DESC = post_afs_shipment_status.STATUS_DESC;
                    context.afs_shipment_status.Add(data);
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
        /// Update an existing afs_shipment_status
        /// </summary>
        /// <param name="put_afs_shipment_status">afs_shipment_status to update</param>
        /// <returns></returns>
        /// <response code="200">afs_shipment_status updated</response>
        /// <response code="404">afs_shipment_status not found</response>")]
        [Authorize]
        [ResponseType(typeof(afs_shipment_status))]
        public HttpResponseMessage Put(afs_shipment_status put_afs_shipment_status)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var existing = context.afs_shipment_status.Where(t => t.STATUS_CODE == put_afs_shipment_status.STATUS_CODE).FirstOrDefault();
                    if (existing == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_shipment_status not found");

                    context.afs_shipment_status.Remove(existing);
                    context.afs_shipment_status.Add(put_afs_shipment_status);
                    context.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, put_afs_shipment_status);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete a afs_shipment_status
        /// </summary>
        /// <remarks>
        /// Delete a afs_shipment_status
        /// </remarks>
        /// <param name="code">expense_id of the afs_shipment_status to delete</param>
        /// <returns></returns>
        [Authorize]
        public HttpResponseMessage Delete(string code)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var existing = context.afs_shipment_status.Where(t => t.STATUS_CODE == code).FirstOrDefault();
                    if (existing == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_shipment_status not found");

                    context.afs_shipment_status.Remove(existing);
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