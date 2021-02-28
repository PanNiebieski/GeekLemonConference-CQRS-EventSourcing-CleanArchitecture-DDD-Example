using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Description;

namespace GeekLemonConference.Api.Controllers
{
    public abstract class BaseGeekLemonController : Controller
    {
        protected virtual MethodFailureResult MethodFailure([ActionResultObjectValue] object value)
        {
            return new MethodFailureResult(value);
        }
    }
}
