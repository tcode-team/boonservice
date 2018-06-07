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
    [RoutePrefix("afs_car_group")]
    public class afs_car_groupController : ApiController
    {
        /// <summary>
        /// Get all afs_car_group
        /// </summary>
        /// <remarks>
        /// Get a list of all afs_car_group
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="404">Data not found</response>
        [ResponseType(typeof(IEnumerable<afs_car_group>))]
        [Authorize]
        [Route("get")]
        public HttpResponseMessage Get()
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var afs_car_group = context.afs_car_group.ToList();
                    return afs_car_group == null
                        ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_car_group not found")
                        : Request.CreateResponse(HttpStatusCode.OK, afs_car_group);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get afs_car_group by id
        /// </summary>
        /// <remarks>
        /// Get a afs_car_group by id
        /// </remarks>
        /// <param name="id">afs_car_group-cargroup_code</param>
        /// <returns></returns>
        /// <response code="200">afs_car_group found</response>
        /// <response code="404">afs_car_group not foundd</response>
        [ResponseType(typeof(afs_car_group))]
        public HttpResponseMessage GetById(string id)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var afs_car_group = context.afs_car_group.Where(t => t.cargroup_code == id).FirstOrDefault();
                    return afs_car_group == null
                        ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_car_group not found")
                        : Request.CreateResponse(HttpStatusCode.OK, afs_car_group);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Add new afs_car_group
        /// </summary>
        /// <remarks>
        /// Add a new afs_car_group
        /// </remarks>
        /// <param name="post_afs_car_group">afs_car_group to add</param>
        /// <returns></returns>
        /// <response code="201">afs_car_group created</response>
        [Authorize]
        [ResponseType(typeof(afs_car_group))]
        public HttpResponseMessage Post(afs_car_group post_afs_car_group)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    afs_car_group data = new afs_car_group();
                    data.cargroup_code = post_afs_car_group.cargroup_code;
                    data.cargroup_desc = post_afs_car_group.cargroup_desc;
                    context.afs_car_group.Add(data);

                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update an existing afs_car_group
        /// </summary>
        /// <param name="put_afs_car_group">afs_car_group to update</param>
        /// <returns></returns>
        /// <response code="200">afs_car_group updated</response>
        /// <response code="404">afs_car_group not found</response>")]
        [Authorize]
        [ResponseType(typeof(afs_car_group))]
        public HttpResponseMessage Put(afs_car_group put_afs_car_group)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var existing = context.afs_car_group.Where(t => t.cargroup_code == put_afs_car_group.cargroup_code).FirstOrDefault();
                    if (existing == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_car_group not found");

                    context.afs_car_group.Remove(existing);
                    context.afs_car_group.Add(put_afs_car_group);

                    return Request.CreateResponse(HttpStatusCode.OK, put_afs_car_group);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete a afs_car_group
        /// </summary>
        /// <remarks>
        /// Delete a afs_car_group
        /// </remarks>
        /// <param name="cargroup_code">cargroup_code of the afs_car_group to delete</param>
        /// <returns></returns>
        [Authorize]
        public HttpResponseMessage Delete(string cargroup_code)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var existing = context.afs_car_group.Where(t => t.cargroup_code == cargroup_code).FirstOrDefault();
                    if (existing == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_car_group not found");

                    context.afs_car_group.Remove(existing);

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