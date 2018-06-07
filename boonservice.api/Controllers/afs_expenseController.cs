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
    /// For Table afs_expense
    /// </summary>
    /// <remarks>
    /// Method about table of afs_expense
    /// </remarks>
    [RoutePrefix("afs_expense")]
    public class afs_expenseController : ApiController
    {
        /// <summary>
        /// Get all afs_expense
        /// </summary>
        /// <remarks>
        /// Get a list of all afs_expense
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="404">Data not found</response>
        [ResponseType(typeof(IEnumerable<afs_expense>))]
        [Authorize]
        [Route("get")]
        public HttpResponseMessage Get()
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var afs_expense = context.afs_expense.ToList();
                    return afs_expense == null
                        ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_expense not found")
                        : Request.CreateResponse(HttpStatusCode.OK, afs_expense);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get afs_expense by id
        /// </summary>
        /// <remarks>
        /// Get a afs_expense by id
        /// </remarks>
        /// <param name="id">afs_expense-expense_id</param>
        /// <returns></returns>
        /// <response code="200">afs_expense found</response>
        /// <response code="404">afs_expense not foundd</response>
        [ResponseType(typeof(afs_expense))]
        public HttpResponseMessage GetById(Int16 id)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var afs_expense = context.afs_expense.Where(t => t.expense_id == id).FirstOrDefault();
                    return afs_expense == null
                        ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_expense not found")
                        : Request.CreateResponse(HttpStatusCode.OK, afs_expense);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Add new afs_expense
        /// </summary>
        /// <remarks>
        /// Add a new afs_expense
        /// </remarks>
        /// <param name="post_afs_expense">afs_expense to add</param>
        /// <returns></returns>
        /// <response code="201">afs_expense created</response>
        [Authorize]
        [ResponseType(typeof(afs_expense))]
        public HttpResponseMessage Post(afs_expense post_afs_expense)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    afs_expense data = new afs_expense();
                    data.expense_id = post_afs_expense.expense_id;
                    data.expense_desc = post_afs_expense.expense_desc;
                    data.expense_amount = post_afs_expense.expense_amount;
                    context.afs_expense.Add(data);

                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update an existing afs_expense
        /// </summary>
        /// <param name="put_afs_expense">afs_expense to update</param>
        /// <returns></returns>
        /// <response code="200">afs_expense updated</response>
        /// <response code="404">afs_expense not found</response>")]
        [Authorize]
        [ResponseType(typeof(afs_expense))]
        public HttpResponseMessage Put(afs_expense put_afs_expense)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var existing = context.afs_expense.Where(t => t.expense_id == put_afs_expense.expense_id).FirstOrDefault();
                    if (existing == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_expense not found");

                    context.afs_expense.Remove(existing);
                    context.afs_expense.Add(put_afs_expense);

                    return Request.CreateResponse(HttpStatusCode.OK, put_afs_expense);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete a afs_expense
        /// </summary>
        /// <remarks>
        /// Delete a afs_expense
        /// </remarks>
        /// <param name="expense_id">expense_id of the afs_expense to delete</param>
        /// <returns></returns>
        [Authorize]
        public HttpResponseMessage Delete(Int16 expense_id)
        {
            try
            {
                using (var context = new SAPContext())
                {
                    var existing = context.afs_expense.Where(t => t.expense_id == expense_id).FirstOrDefault();
                    if (existing == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "afs_expense not found");

                    context.afs_expense.Remove(existing);

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