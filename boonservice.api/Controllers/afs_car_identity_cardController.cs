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
    [RoutePrefix("afs_car_identity_card")]
    public class afs_car_identity_cardController : ApiController
    {
        /// <summary>
        /// Get all afs_car_identity_card
        /// </summary>
        /// <remarks>
        /// Get a list of all afs_car_identity_card
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="404">Data not found</response>
        [ResponseType(typeof(IEnumerable<afs_car_identity_card>))]
        [Authorize]
        [Route("get")]
        public HttpResponseMessage Get()
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var afs_car_identity_card = context.afs_car_identity_card.ToList();
                    return afs_car_identity_card == null
                        ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_car_identity_card not found")
                        : Request.CreateResponse(HttpStatusCode.OK, afs_car_identity_card);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get afs_car_identity_card by id
        /// </summary>
        /// <remarks>
        /// Get a afs_car_identity_card by id
        /// </remarks>
        /// <param name="id">afs_car_identity_card-people_id</param>
        /// <returns></returns>
        /// <response code="200">afs_car_identity_card found</response>
        /// <response code="404">afs_car_identity_card not foundd</response>
        [ResponseType(typeof(afs_car_identity_card))]
        public HttpResponseMessage GetById(Int16 id)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var afs_car_identity_card = context.afs_car_identity_card.Where(t => t.PEOPLE_ID == id).FirstOrDefault();
                    return afs_car_identity_card == null
                        ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_car_identity_card not found")
                        : Request.CreateResponse(HttpStatusCode.OK, afs_car_identity_card);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Add new afs_car_identity_card
        /// </summary>
        /// <remarks>
        /// Add a new afs_car_identity_card
        /// </remarks>
        /// <param name="post_afs_car_identity_card">afs_car_identity_card to add</param>
        /// <returns></returns>
        /// <response code="201">afs_car_identity_card created</response>
        [Authorize]
        [ResponseType(typeof(afs_car_identity_card))]
        public HttpResponseMessage Post(afs_car_identity_card post_afs_car_identity_card)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    afs_car_identity_card data = new afs_car_identity_card();
                    data.PEOPLE_ID = post_afs_car_identity_card.PEOPLE_ID;
                    data.NAME = post_afs_car_identity_card.NAME;
                    data.CARD_CODE = post_afs_car_identity_card.CARD_CODE;
                    data.PASSPORT_CODE = post_afs_car_identity_card.PASSPORT_CODE;
                    data.NATION = post_afs_car_identity_card.NATION;
                    data.DRIVER_FLAG = post_afs_car_identity_card.DRIVER_FLAG;
                    data.STAFF_FLAG = post_afs_car_identity_card.STAFF_FLAG;
                    context.afs_car_identity_card.Add(data);
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
        /// Update an existing post_afs_car_identity_card
        /// </summary>
        /// <param name="put_afs_car_identity_card">afs_car_identity_card to update</param>
        /// <returns></returns>
        /// <response code="200">post_afs_car_identity_card updated</response>
        /// <response code="404">post_afs_car_identity_card not found</response>")]
        [Authorize]
        [ResponseType(typeof(afs_car_identity_card))]
        public HttpResponseMessage Put(afs_car_identity_card put_afs_car_identity_card)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var existing = context.afs_car_identity_card.Where(t => t.PEOPLE_ID == put_afs_car_identity_card.PEOPLE_ID).FirstOrDefault();
                    if (existing == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_car_group not found");

                    context.afs_car_identity_card.Remove(existing);
                    context.afs_car_identity_card.Add(put_afs_car_identity_card);
                    context.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, put_afs_car_identity_card);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete a afs_car_identity_card
        /// </summary>
        /// <remarks>
        /// Delete a afs_car_identity_card
        /// </remarks>
        /// <param name="people_id">afs_car_identity_card of the afs_car_group to delete</param>
        /// <returns></returns>
        [Authorize]
        public HttpResponseMessage Delete(Int16 people_id)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var existing = context.afs_car_identity_card.Where(t => t.PEOPLE_ID == people_id).FirstOrDefault();
                    if (existing == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_car_identity_card not found");

                    context.afs_car_identity_card.Remove(existing);
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