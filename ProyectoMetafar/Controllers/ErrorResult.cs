using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Metafar_API.Controllers
{
    public class ErrorResult : ObjectResult
    {
        public ErrorResult(int statusCode, string message) : base(new { StatusCode = statusCode, Message = message })
        {
            StatusCode = statusCode;
        }
    }
}
