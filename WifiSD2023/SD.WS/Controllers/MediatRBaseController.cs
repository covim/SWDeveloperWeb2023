using MediatR;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Net;

namespace SD.WS.Controllers
{
    public class MediatRBaseController : ControllerBase
    {
        private IMediator mediator;
        protected IMediator Mediator => this.mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected void SetLocationUri<T>(T result, string id)
        {
            if (result == null || string.IsNullOrWhiteSpace(id))
            {
                throw new HttpRequestException("Resource is null!");
            }

            // aktuelle URL abrufen
            var baseUrl = base.Request.HttpContext.Request.GetEncodedUrl();
            
            // basis URL kürzen (bis zum ersten Parameter, falls vorhanden)

            var length = baseUrl.IndexOf('?') > 0 ? baseUrl.IndexOf('?') : baseUrl.Length;
            var uri = baseUrl.Substring(0, length);

            // jetzt die Id an den basis URL anhängen
            uri = string.Concat(uri, uri.EndsWith("/") ? string.Empty : "/", id);

            base.HttpContext.Response.Headers.Add("Location", uri);
            base.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;


        }

    }
}
