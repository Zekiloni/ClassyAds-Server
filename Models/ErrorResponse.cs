using System.Net;

namespace ClassyAdsServer.Models
{
    public class ErrorResponse
    {

        public string Message { get; set; }
        public string? Description { get; set; }

        public ErrorResponse(string message) { 
            Message = message;
        }

        public ErrorResponse(string message, string? description)
        {
            Message = message;
            Description = description;
        }
    }
}
