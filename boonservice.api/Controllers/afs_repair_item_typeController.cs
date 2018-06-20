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
    /// For Table afs_repair_item_type
    /// </summary>
    /// <remarks>
    /// Method about table of afs_repair_item_type
    /// </remarks>
    [RoutePrefix("afs_repair_item_type")]
    public class afs_repair_item_typeController : ApiController
    {
        /// <summary>
        /// Get all afs_repair_item_type
        /// </summary>
        /// <remarks>
        /// Get a list of all afs_repair_item_type
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="404">Data not found</response>
        [ResponseType(typeof(IEnumerable<afs_repair_item_type>))]
        [Route("get")]
        public HttpResponseMessage Get()
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var afs_repair_item_type = context.afs_repair_item_type.ToList();
                    return afs_repair_item_type == null
                        ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_repair_item_type not found")
                        : Request.CreateResponse(HttpStatusCode.OK, afs_repair_item_type);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}