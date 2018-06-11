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
    /// For Table afs_shipment_expense
    /// </summary>
    /// <remarks>
    /// Method about table of afs_shipment_expense
    /// </remarks>
    [RoutePrefix("afs_shipment_expense")]
    public class afs_shipment_expenseController : ApiController
    {
        /// <summary>
        /// Get all afs_shipment_expense
        /// </summary>
        /// <remarks>
        /// Get a list of all afs_shipment_expense
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="404">Data not found</response>
        [ResponseType(typeof(IEnumerable<afs_shipment_expense>))]
        [Authorize]
        [Route("get")]
        public HttpResponseMessage Get()
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var afs_shipment_expense = context.afs_shipment_expense.ToList();
                    return afs_shipment_expense == null
                        ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_shipment_expense not found")
                        : Request.CreateResponse(HttpStatusCode.OK, afs_shipment_expense);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get afs_shipment_expense by client, shipment_number
        /// </summary>
        /// <remarks>
        /// Get a afs_shipment_expense by client, shipment_number
        /// </remarks>
        /// <param name="postdata">afs_shipment_expense-client, shipment_number</param>
        /// <returns></returns>
        /// <response code="200">afs_shipment_expense found</response>
        /// <response code="404">afs_shipment_expense not foundd</response>
        [ResponseType(typeof(afs_shipment_expense))]
        public HttpResponseMessage GetById(afs_shipment_expense postdata)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var afs_shipment_expense = context.afs_shipment_expense.Where(t => t.CLIENT == postdata.CLIENT && t.SHIPMENT_NUMBER == postdata.SHIPMENT_NUMBER).FirstOrDefault();
                    return afs_shipment_expense == null
                        ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_shipment_expense not found")
                        : Request.CreateResponse(HttpStatusCode.OK, afs_shipment_expense);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Add new afs_shipment_expense
        /// </summary>
        /// <remarks>
        /// Add a new afs_shipment_expense
        /// </remarks>
        /// <param name="post_afs_shipment_expense">afs_shipment_expense to add</param>
        /// <returns></returns>
        /// <response code="201">afs_shipment_expense created</response>
        [Authorize]
        [ResponseType(typeof(afs_shipment_expense))]
        public HttpResponseMessage Post(afs_shipment_expense post_afs_shipment_expense)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    afs_shipment_expense data = new afs_shipment_expense();
                    data.CLIENT = post_afs_shipment_expense.CLIENT;
                    data.SHIPMENT_NUMBER = post_afs_shipment_expense.SHIPMENT_NUMBER;
                    data.EXPENSE_ID = post_afs_shipment_expense.EXPENSE_ID;
                    data.EXPENSE_AMOUNT = post_afs_shipment_expense.EXPENSE_AMOUNT;
                    data.REMARK = post_afs_shipment_expense.REMARK;
                    data.CREATED_BY = post_afs_shipment_expense.CREATED_BY;
                    data.CREATED_DATE = DateTime.Now;
                    data.UPDATE_BY = post_afs_shipment_expense.UPDATE_BY;
                    data.UPDATE_DATE = DateTime.Now;
                    context.afs_shipment_expense.Add(data);
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
        /// Update an existing afs_shipment_expense
        /// </summary>
        /// <param name="put_afs_shipment_expense">afs_shipment_expense to update</param>
        /// <returns></returns>
        /// <response code="200">afs_shipment_expense updated</response>
        /// <response code="404">afs_shipment_expense not found</response>")]
        [Authorize]
        [ResponseType(typeof(afs_shipment_expense))]
        public HttpResponseMessage Put(afs_shipment_expense put_afs_shipment_expense)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var existing = context.afs_shipment_expense.Where(t => t.CLIENT == put_afs_shipment_expense.CLIENT && t.SHIPMENT_NUMBER == put_afs_shipment_expense.SHIPMENT_NUMBER).FirstOrDefault();
                    if (existing == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_shipment_expense not found");

                    context.afs_shipment_expense.Remove(existing);
                    context.afs_shipment_expense.Add(put_afs_shipment_expense);
                    context.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, put_afs_shipment_expense);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete a afs_shipment_expense
        /// </summary>
        /// <remarks>
        /// Delete a afs_shipment_expense
        /// </remarks>
        /// <param name="postdata">client,shipment_number of the afs_shipment_expense to delete</param>
        /// <returns></returns>
        [Authorize]
        public HttpResponseMessage Delete(afs_shipment_expense postdata)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var existing = context.afs_shipment_expense.Where(t => t.CLIENT == postdata.CLIENT && t.SHIPMENT_NUMBER == postdata.SHIPMENT_NUMBER).FirstOrDefault();
                    if (existing == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_shipment_expense not found");

                    context.afs_shipment_expense.Remove(existing);
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