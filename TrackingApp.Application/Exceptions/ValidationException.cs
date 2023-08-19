using FluentValidation.Results;

namespace TrackingApp.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("One or more validation failures have occurred.")
        {
            Errors = new List<ErrorModel>();
        }

        public List<ErrorModel> Errors { get; }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(new ErrorModel()
                {
                    propertyName = failure.PropertyName,
                    errorMessage = failure.ErrorMessage,
                });
            }
        }
    }

    public class ErrorModel
    {
        public string latestError;
        public string propertyName { get; set; }
        public string errorMessage { get; set; }
    }
}
