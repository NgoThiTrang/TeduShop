using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeduShop.Service;
using TeduShop.Web.InFrastructure.Core;

namespace TeduShop.Web.API
{
    [RoutePrefix("api/statistic")]
    public class GetRevenueStatisticController : ApiControllerBase
    {
        IStatisticService _statisticServiec;
        public GetRevenueStatisticController(IErrorService errorService, IStatisticService statisticService): base(errorService)
        {
            _statisticServiec = statisticService;
        }
        [Route("getrevenue")]
        [HttpGet]
        public HttpResponseMessage GetrevenueStatistic( HttpRequestMessage request, string fromDate, string toDate)
        {
            var model = _statisticServiec.GetRevenueStatistic(fromDate, toDate);
            return request.CreateResponse(HttpStatusCode.OK ,model);
        }
    }
}
