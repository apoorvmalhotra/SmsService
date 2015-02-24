using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Emails.Data;
using EmailService.Models;
using EmailService.Utilities;

namespace EmailService.Controllers
{
    public class BaseApiController : ApiController
    {
        protected static HttpResponseMessage CreateResponseMessage(string data = null, string mediaType = null, DateTime? lastModified = null, bool isNew = false, bool returnBody = true)
        {
            var message = new HttpResponseMessage(isNew ? HttpStatusCode.Created : HttpStatusCode.OK);

            if (!string.IsNullOrEmpty(data) && returnBody)
            {
                message.Headers.ETag = new EntityTagHeaderValue(string.Format("\"{0}\"", data.GetHashCode().ToString(CultureInfo.CurrentCulture)));
                message.Content = new StringContent(data);
//                message.Content.Headers.ContentType.MediaType = mediaType;
                if (lastModified.HasValue)
                    message.Content.Headers.Add("Last-Modified", new[] { new DateTimeOffset(lastModified.Value.ToLocalTime()).ToString("r") });
            }

            return message;
        }

//        protected EntityFormat GetContentType()
//        {
//            var contentType = Request.Content.Headers.ContentType.Maybe(ct => ct.MediaType, string.Empty);
//
//            var entityFormat = EntityFormat.JSON;
//            switch (contentType)
//            {
//                case "application/xml":
//                    entityFormat = EntityFormat.XML;
//                    break;
//            }
//            return entityFormat;
//        }
    }
}