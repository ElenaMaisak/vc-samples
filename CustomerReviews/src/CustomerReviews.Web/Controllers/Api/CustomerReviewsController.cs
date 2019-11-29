using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CustomerReviews.Core.Model;
using CustomerReviews.Core.Services;
using VirtoCommerce.Domain.Commerce.Model.Search;
using VirtoCommerce.Platform.Core.Web.Security;

namespace CustomerReviews.Web.Controllers.Api
{
    [RoutePrefix("api/customerReviews")]
    public class CustomerReviewsController : ApiController
    {
        private readonly ICustomerReviewSearchService _customerReviewSearchService;
        private readonly ICustomerReviewService _customerReviewService;

        public CustomerReviewsController()
        {
        }

        public CustomerReviewsController(ICustomerReviewSearchService customerReviewSearchService, ICustomerReviewService customerReviewService)
        {
            _customerReviewSearchService = customerReviewSearchService;
            _customerReviewService = customerReviewService;
        }

        /// <summary>
        /// Return product Customer review search results
        /// </summary>
        [HttpPost]
        [Route("search")]
        [ResponseType(typeof(GenericSearchResult<CustomerReview>))]
        [CheckPermission(Permission = Core.ModuleConstants.Security.Permissions.CustomerReviewRead)]
        public IHttpActionResult SearchCustomerReviews(CustomerReviewSearchCriteria criteria)
        {
            var result = _customerReviewSearchService.SearchCustomerReviews(criteria);
            return Ok(result);
        }

        /// <summary>
        ///  Create new or update existing customer review
        /// </summary>
        /// <param name="customerReviews">Customer reviews</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(void))]
        [CheckPermission(Permission = Core.ModuleConstants.Security.Permissions.CustomerReviewUpdate)]
        public IHttpActionResult Update(CustomerReview[] customerReviews)
        {
            _customerReviewService.SaveCustomerReviewsAsync(customerReviews);
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Delete Customer Reviews by IDs
        /// </summary>
        /// <param name="ids">IDs</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        [ResponseType(typeof(void))]
        [CheckPermission(Permission = Core.ModuleConstants.Security.Permissions.CustomerReviewDelete)]
        public IHttpActionResult Delete([FromUri] string[] ids)
        {
            _customerReviewService.DeleteCustomerReviewsAsync(ids);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
