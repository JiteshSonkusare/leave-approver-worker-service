using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeave.Infrastructure.Extensions
{
    public class HeaderContentExtension
    {
        public const string HeaderContentType = "application/json";

        public static ByteArrayContent GetHeaderContent(string messageId)
        {
            var message = new { messageId = messageId };
            var myContent = JsonConvert.SerializeObject(message);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue(HeaderContentType);
            return byteContent;
        }


    }
}
