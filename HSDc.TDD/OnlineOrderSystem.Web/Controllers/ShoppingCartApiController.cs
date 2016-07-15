using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineOrderSystem.Web.Controllers
{
    public class ShoppingCartApiController : ApiController
    {
        [Authorize]
        public ProcessStatus Get(string ProductId)
        {
            return ProcessStatus.Success;
        }
    }

    public enum ProcessStatus
    {
        Success, Failed
    }
}
