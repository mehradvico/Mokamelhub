using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Services.ProductSrvs.TorobSrv.Dto
{
    public sealed class TorobErrorDto
    {
        public TorobErrorDto(string error)
        {
            Error = error;
        }

        [JsonPropertyName("error")]
        public string Error { get; set; }
    }
}
