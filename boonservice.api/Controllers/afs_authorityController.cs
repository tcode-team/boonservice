using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// For Table afs_authority
    /// </summary>
    /// <remarks>
    /// Method about table of afs_authority
    /// </remarks>
    [RoutePrefix("afs_authority")]
    public class afs_authorityController : ApiController
    {
        /// <summary>
        /// Get all afs_authority
        /// </summary>
        /// <remarks>
        /// Get a list of all afs_authority
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="404">Data not found</response>
        [ResponseType(typeof(IEnumerable<afs_authority>))]
        [Route("get")]
        public HttpResponseMessage Get()
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var afs_authority = context.afs_authority.ToList();
                    return afs_authority == null
                        ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_authority not found")
                        : Request.CreateResponse(HttpStatusCode.OK, afs_authority);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get afs_authority by User id
        /// </summary>
        /// <remarks>
        /// Get a afs_authority by User id
        /// </remarks>
        /// <param name="id">afs_authority-user_id</param>
        /// <returns></returns>
        /// <response code="200">afs_authority found</response>
        /// <response code="404">afs_authority not foundd</response>
        [ResponseType(typeof(afs_authority))]
        [Route("getid")]
        public HttpResponseMessage GetById(double id)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var afs_authority = context.afs_authority.Where(t => t.USER_ID == id).FirstOrDefault();
                    return afs_authority == null
                        ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_authority not found")
                        : Request.CreateResponse(HttpStatusCode.OK, afs_authority);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Add new afs_authority
        /// </summary>
        /// <remarks>
        /// Add a new afs_authority
        /// </remarks>
        /// <param name="post_afs_authority">afs_authority to add</param>
        /// <returns></returns>
        /// <response code="201">afs_authority created</response>
        [Authorize]
        [ResponseType(typeof(afs_authority))]
        public HttpResponseMessage Post(afs_authority post_afs_authority)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    afs_authority data = new afs_authority();
                    data.USER_ID = post_afs_authority.USER_ID;
                    data.SALE_FLAG = post_afs_authority.SALE_FLAG;
                    data.AFTERSALE_FLAG = post_afs_authority.AFTERSALE_FLAG;
                    data.PURCHASE_FLAG = post_afs_authority.PURCHASE_FLAG;
                    context.afs_authority.Add(data);
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
        /// Update an existing afs_authority
        /// </summary>
        /// <param name="put_afs_authority">afs_authority to update</param>
        /// <returns></returns>
        /// <response code="200">afs_authority updated</response>
        /// <response code="404">afs_authority not found</response>")]
        [Authorize]
        [ResponseType(typeof(afs_authority))]
        public HttpResponseMessage Put(afs_authority put_afs_authority)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var existing = context.afs_authority.Where(t => t.USER_ID == put_afs_authority.USER_ID).FirstOrDefault();
                    if (existing == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_authority not found");
                    else 
                        context.Entry(existing).State = EntityState.Modified;

                    existing.SALE_FLAG = put_afs_authority.SALE_FLAG;
                    existing.AFTERSALE_FLAG = put_afs_authority.AFTERSALE_FLAG;
                    existing.PURCHASE_FLAG = put_afs_authority.PURCHASE_FLAG;
                    context.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, put_afs_authority);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete a afs_authority
        /// </summary>
        /// <remarks>
        /// Delete a afs_authority
        /// </remarks>
        /// <param name="user_id">user_id of the afs_authority to delete</param>
        /// <returns></returns>
        [Authorize]
        public HttpResponseMessage Delete(double user_id)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var existing = context.afs_authority.Where(t => t.USER_ID == user_id).FirstOrDefault();
                    if (existing == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_authority not found");

                    context.afs_authority.Remove(existing);
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