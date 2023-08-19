using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingApp.Application.Exceptions
{
    public class ConflictException : Exception
    {
        public List<ErrorModel> ErrorModels { get; set; }

        public ConflictException() : base()
        {
        }

        public ConflictException(string message) : base(message)
        {

        }
        public ConflictException(string message, List<ErrorModel> errorModels) : base(message)
        {
            this.ErrorModels = errorModels;
        }

        public ConflictException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {

        }
    }
}
