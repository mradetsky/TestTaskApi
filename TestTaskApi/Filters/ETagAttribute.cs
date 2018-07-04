using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace TestTaskApi.Filters
{
    public class ETagAttribute : ResultFilterAttribute
    {
        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var result = context.Result as OkObjectResult;
            if (result == null) return base.OnResultExecutionAsync(context, next);

            string token;
            using (var ms = new MemoryStream())
            {
                using (var writer = new BsonWriter(ms))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(writer, result);
                    token = GetToken(ms.ToArray());
                }
            }

            var clientToken = context.HttpContext.Request.Headers["If-None-Match"];

            if (token != clientToken)
            {
                context.HttpContext.Response.Headers["ETag"] = token;
            }
            else
            {
                context.Result = new StatusCodeResult(304);
            }

            return base.OnResultExecutionAsync(context, next);
        }

        private static string GetToken(byte[] bytes)
        {
            var checksum = MD5.Create().ComputeHash(bytes);
            return Convert.ToBase64String(checksum, 0, checksum.Length);
        }
    }
}
