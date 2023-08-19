using TrackingApp.Application.Exceptions;

namespace TrackingApp.Application.Wrappers
{
    public class ErrorResponse<T>
    {
        public ErrorResponse()
        {
        }
        public ErrorResponse(string message)
        {
            success = false;
            this.message = message;
        }

        public ErrorResponse(T data, string message = null)
        {
            success = true;
            this.message = message;
            this.data = data;
        }

        public bool success { get; set; }
        public string message { get; set; }
        public List<ErrorModel> errors { get; set; }
        public T data { get; set; }
    }
}
