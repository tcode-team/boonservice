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
    /// For Table afs_carries_point
    /// </summary>
    /// <remarks>
    /// Method about table of afs_carries_point
    /// </remarks>
    [RoutePrefix("afs_carries_point")]
    public class afs_carries_pointController : ApiController
    {
        /// <summary>
        /// Get all afs_carries_point
        /// </summary>
        /// <remarks>
        /// Get a list of all afs_carries_point
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="404">Data not found</response>
        [ResponseType(typeof(IEnumerable<afs_carries_point>))]
        [Authorize]
        [Route("get")]
        public HttpResponseMessage Get()
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var afs_carries_point = context.afs_carries_point.ToList();
                    return afs_carries_point == null
                        ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_carries_point not found")
                        : Request.CreateResponse(HttpStatusCode.OK, afs_carries_point);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get afs_carries_point by id
        /// </summary>
        /// <remarks>
        /// Get a afs_carries_point by id
        /// </remarks>
        /// <param name="id">afs_carries_point-point_id</param>
        /// <returns></returns>
        /// <response code="200">afs_carries_point found</response>
        /// <response code="404">afs_carries_point not foundd</response>
        [ResponseType(typeof(afs_carries_point))]
        public HttpResponseMessage GetById(Int16 id)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var afs_carries_point = context.afs_carries_point.Where(t => t.POINT_ID == id).FirstOrDefault();
                    return afs_carries_point == null
                        ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_car_group not found")
                        : Request.CreateResponse(HttpStatusCode.OK, afs_carries_point);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get afs_carries_point by id
        /// </summary>
        /// <remarks>
        /// Get a afs_carries_point by id
        /// </remarks>
        /// <param name="cargroup">afs_carries_point-cargroup</param>
        /// <returns>afs_carries_point of row</returns>
        /// <response code="200">afs_carries_point found</response>
        /// <response code="404">afs_carries_point not foundd</response>
        [ResponseType(typeof(IEnumerable<afs_carries_point>))]
        public HttpResponseMessage GetByCarGroup(string cargroup)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var afs_carries_point = context.afs_carries_point.Where(t => t.CARGROUP_CODE == cargroup).ToList();
                    return afs_carries_point == null
                        ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_car_group not found")
                        : Request.CreateResponse(HttpStatusCode.OK, afs_carries_point);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Add new afs_carries_point
        /// </summary>
        /// <remarks>
        /// Add a new afs_carries_point
        /// </remarks>
        /// <param name="post_afs_carries_point">afs_carries_point to add</param>
        /// <returns></returns>
        /// <response code="201">afs_carries_point created</response>
        [Authorize]
        [ResponseType(typeof(afs_carries_point))]
        public HttpResponseMessage Post(afs_carries_point post_afs_carries_point)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    afs_carries_point data = new afs_carries_point();
                    data.POINT_ID = post_afs_carries_point.POINT_ID;
                    data.CARGROUP_CODE = post_afs_carries_point.CARGROUP_CODE;
                    data.TIER_DESC = post_afs_carries_point.TIER_DESC;
                    data.DPOINT1_AMOUNT = post_afs_carries_point.DPOINT1_AMOUNT;
                    data.DPOINT2_AMOUNT = post_afs_carries_point.DPOINT2_AMOUNT;
                    data.SPOINT1_AMOUNT = post_afs_carries_point.SPOINT1_AMOUNT;
                    data.SPOINT2_AMOUNT = post_afs_carries_point.SPOINT2_AMOUNT;
                    data.CREATED_BY = post_afs_carries_point.CREATED_BY;
                    data.CREATED_DATE = DateTime.Now;
                    data.UPDATE_BY = post_afs_carries_point.UPDATE_BY;
                    data.UPDATE_DATE = DateTime.Now;
                    context.afs_carries_point.Add(data);
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
        /// Update an existing afs_carries_point
        /// </summary>
        /// <param name="put_afs_carries_point">afs_carries_point to update</param>
        /// <returns></returns>
        /// <response code="200">afs_carries_point updated</response>
        /// <response code="404">afs_carries_point not found</response>")]
        [Authorize]
        [ResponseType(typeof(afs_carries_point))]
        public HttpResponseMessage Put(afs_carries_point put_afs_carries_point)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var existing = context.afs_carries_point.Where(t => t.POINT_ID == put_afs_carries_point.POINT_ID).FirstOrDefault();
                    if (existing == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_carries_point not found");

                    context.afs_carries_point.Remove(existing);
                    put_afs_carries_point.UPDATE_DATE = DateTime.Now;
                    context.afs_carries_point.Add(put_afs_carries_point);
                    context.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, put_afs_carries_point);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete a afs_carries_point
        /// </summary>
        /// <remarks>
        /// Delete a afs_carries_point
        /// </remarks>
        /// <param name="point_id">point_id of the afs_carries_point to delete</param>
        /// <returns></returns>
        [Authorize]
        public HttpResponseMessage Delete(Int16 point_id)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var existing = context.afs_carries_point.Where(t => t.POINT_ID == point_id).FirstOrDefault();
                    if (existing == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_carries_point not found");

                    context.afs_carries_point.Remove(existing);
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