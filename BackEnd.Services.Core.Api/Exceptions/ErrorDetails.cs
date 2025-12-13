using System.Text.Json;

namespace BackEnd.Services.Core.Api.Exceptions
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public int HResult { get; set; }

        
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}