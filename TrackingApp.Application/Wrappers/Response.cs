using TrackingApp.Application.Exceptions;

namespace TrackingApp.Application.Wrappers
{
    public class Response<T>
    {
        public Response()
        {

        }

        public Response(T data, string message = "")
        {
            Message = message;
            PayLoad = data;
        }

        public Response(bool success, T data, string message, List<ErrorModel> errors)
        {
            Success = success;
            this.Message = message;
            this.PayLoad = data;
            this.Errors = errors;
        }

        public Response(bool success, T data, string message)
        {
            Success = success;
            this.Message = message;
            this.PayLoad = data;
            this.Errors = null;
        }

        public List<ErrorModel> Errors { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public T PayLoad { get; set; }
    }
}
