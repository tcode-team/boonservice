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
    /// For Table afs_shipment_carries
    /// </summary>
    /// <remarks>
    /// Method about table of afs_shipment_carries
    /// </remarks>
    [RoutePrefix("afs_shipment_carries")]
    public class afs_shipment_carriesController : ApiController
    {
        /// <summary>
        /// Get all afs_shipment_carries
        /// </summary>
        /// <remarks>
        /// Get a list of all afs_shipment_carries
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="404">Data not found</response>
        [ResponseType(typeof(IEnumerable<afs_shipment_carries>))]
        [Authorize]
        [Route("get")]
        public HttpResponseMessage Get()
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var afs_shipment_carries = context.afs_expense.ToList();
                    return afs_shipment_carries == null
                        ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_shipment_carries not found")
                        : Request.CreateResponse(HttpStatusCode.OK, afs_shipment_carries);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get afs_shipment_carries by id
        /// </summary>
        /// <remarks>
        /// Get a afs_shipment_carries by id
        /// </remarks>
        /// <param name="postdata">afs_shipment_carries-expense_id</param>
        /// <returns></returns>
        /// <response code="200">afs_shipment_carries found</response>
        /// <response code="404">afs_shipment_carries not foundd</response>
        [ResponseType(typeof(afs_shipment_carries))]
        public HttpResponseMessage GetById(afs_shipment_carries postdata)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var afs_shipment_carries = context.afs_shipment_carries.Where(t => t.CLIENT == postdata.CLIENT && t.SHIPMENT_NUMBER == postdata.SHIPMENT_NUMBER).FirstOrDefault();
                    return afs_shipment_carries == null
                        ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_shipment_carries not found")
                        : Request.CreateResponse(HttpStatusCode.OK, afs_shipment_carries);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Add new afs_shipment_carries
        /// </summary>
        /// <remarks>
        /// Add a new afs_shipment_carries
        /// </remarks>
        /// <param name="post_afs_shipment_carries">afs_shipment_carries to add</param>
        /// <returns></returns>
        /// <response code="201">afs_shipment_carries created</response>
        [Authorize]
        [ResponseType(typeof(afs_shipment_carries))]
        public HttpResponseMessage Post(afs_shipment_carries post_afs_shipment_carries)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    afs_shipment_carries data = new afs_shipment_carries();
                    data.CLIENT = post_afs_shipment_carries.CLIENT;
                    data.SHIPMENT_NUMBER = post_afs_shipment_carries.SHIPMENT_NUMBER;
                    data.POINT_ID = post_afs_shipment_carries.POINT_ID;
                    data.SALEORDER_NUMBER = post_afs_shipment_carries.SALEORDER_NUMBER;
                    data.DRIVER_AMOUNT = post_afs_shipment_carries.DRIVER_AMOUNT;
                    data.STAFF_AMOUNT = post_afs_shipment_carries.STAFF_AMOUNT;
                    data.TIME_RANGE = post_afs_shipment_carries.TIME_RANGE;
                    data.CREATED_BY = post_afs_shipment_carries.CREATED_BY;
                    data.CREATED_DATE = DateTime.Now;
                    data.UPDATE_BY = post_afs_shipment_carries.UPDATE_BY;
                    data.UPDATE_DATE = DateTime.Now;
                    context.afs_shipment_carries.Add(data);
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
        /// Update an existing afs_shipment_carries
        /// </summary>
        /// <param name="put_afs_shipment_carries">afs_shipment_carries to update</param>
        /// <returns></returns>
        /// <response code="200">afs_shipment_carries updated</response>
        /// <response code="404">afs_shipment_carries not found</response>")]
        [Authorize]
        [ResponseType(typeof(afs_shipment_carries))]
        public HttpResponseMessage Put(afs_shipment_carries put_afs_shipment_carries)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var existing = context.afs_shipment_carries.Where(t => t.CLIENT == put_afs_shipment_carries.CLIENT && t.SHIPMENT_NUMBER == put_afs_shipment_carries.SHIPMENT_NUMBER).FirstOrDefault();
                    if (existing == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_shipment_carries not found");

                    context.afs_shipment_carries.Remove(existing);
                    context.afs_shipment_carries.Add(put_afs_shipment_carries);
                    context.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, put_afs_shipment_carries);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete a afs_shipment_carries
        /// </summary>
        /// <remarks>
        /// Delete a afs_shipment_carries
        /// </remarks>
        /// <param name="postdata">client,shipment_number of the afs_shipment_carries to delete</param>
        /// <returns></returns>
        [Authorize]
        public HttpResponseMessage Delete(afs_shipment_carries postdata)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var existing = context.afs_shipment_carries.Where(t => t.CLIENT == postdata.CLIENT && t.SHIPMENT_NUMBER == postdata.SHIPMENT_NUMBER).FirstOrDefault();
                    if (existing == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_shipment_carries not found");

                    context.afs_shipment_carries.Remove(existing);
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