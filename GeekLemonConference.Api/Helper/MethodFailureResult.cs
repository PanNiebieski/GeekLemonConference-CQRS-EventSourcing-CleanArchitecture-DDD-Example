using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace GeekLemonConference.Api.Controllers
{
    /// <summary>
    /// An <see cref="ObjectResult"/> that when executed will produce a Not Found (404) response.
    /// </summary>
    [DefaultStatusCode(DefaultStatusCode)]
    public class MethodFailureResult : ObjectResult
    {
        private const int DefaultStatusCode = 420;

        /// <summary>
        /// Creates a new <see cref="NotFoundObjectResult"/> instance.
        /// </summary>
        /// <param name="value">The value to format in the entity body.</param>
        public MethodFailureResult([ActionResultObjectValue] object value)
            : base(value)
        {
            StatusCode = DefaultStatusCode;
        }
    }
}
