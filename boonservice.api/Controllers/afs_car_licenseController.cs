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
    /// For Table afs_car_license
    /// </summary>
    /// <remarks>
    /// Method about table of afs_car_license
    /// </remarks>
    [RoutePrefix("afs_car_license")]
    public class afs_car_licenseController : ApiController
    {
        /// <summary>
        /// Get all afs_car_license
        /// </summary>
        /// <remarks>
        /// Get a list of all afs_car_license
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="404">Data not found</response>
        [ResponseType(typeof(IEnumerable<afs_car_license>))]
        [Route("get")]
        public HttpResponseMessage Get()
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var afs_car_license = context.afs_car_license.ToList();
                    return afs_car_license == null
                        ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_car_license not found")
                        : Request.CreateResponse(HttpStatusCode.OK, afs_car_license);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get afs_car_license by id
        /// </summary>
        /// <remarks>
        /// Get a afs_car_license by id
        /// </remarks>
        /// <param name="id">afs_car_license-car_id</param>
        /// <returns></returns>
        /// <response code="200">afs_car_license found</response>
        /// <response code="404">afs_car_license not foundd</response>
        [ResponseType(typeof(afs_car_license))]
        public HttpResponseMessage GetById(Int16 id)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var afs_car_license = context.afs_car_license.Where(t => t.CAR_ID == id).FirstOrDefault();
                    return afs_car_license == null
                        ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_car_license not found")
                        : Request.CreateResponse(HttpStatusCode.OK, afs_car_license);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Add new afs_car_license
        /// </summary>
        /// <remarks>
        /// Add a new afs_car_license
        /// </remarks>
        /// <param name="post_afs_car_license">afs_car_license to add</param>
        /// <returns></returns>
        /// <response code="201">afs_car_group created</response>
        [Authorize]
        [ResponseType(typeof(afs_car_license))]
        public HttpResponseMessage Post(afs_car_license post_afs_car_license)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    afs_car_license data = new afs_car_license();
                    data.CARGROUP_CODE = post_afs_car_license.CARGROUP_CODE;
                    data.CAR_SAP = post_afs_car_license.CAR_SAP;
                    data.CREATED_BY = post_afs_car_license.CREATED_BY;
                    data.CREATED_DATE = DateTime.Now;
                    data.UPDATE_BY = post_afs_car_license.UPDATE_BY;
                    data.UPDATE_DATE = DateTime.Now;
                    context.afs_car_license.Add(data);
                    context.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.InnerException.InnerException.Message);
            }
        }

        /// <summary>
        /// Update an existing afs_car_license
        /// </summary>
        /// <param name="put_afs_car_license">afs_car_license to update</param>
        /// <returns></returns>
        /// <response code="200">afs_car_license updated</response>
        /// <response code="404">afs_car_license not found</response>")]
        [Authorize]
        [ResponseType(typeof(afs_car_license))]
        public HttpResponseMessage Put(afs_car_license put_afs_car_license)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var existing = context.afs_car_license.Where(t => t.CAR_ID == put_afs_car_license.CAR_ID).FirstOrDefault();
                    if (existing == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_car_license not found");

                    context.afs_car_license.Remove(existing);
                    context.afs_car_license.Add(put_afs_car_license);
                    context.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, put_afs_car_license);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete a afs_car_license
        /// </summary>
        /// <remarks>
        /// Delete a afs_car_license
        /// </remarks>
        /// <param name="car_id">car_id of the afs_car_license to delete</param>
        /// <returns></returns>
        [Authorize]
        public HttpResponseMessage Delete(Int16 car_id)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var existing = context.afs_car_license.Where(t => t.CAR_ID == car_id).FirstOrDefault();
                    if (existing == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_car_license not found");

                    context.afs_car_license.Remove(existing);
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