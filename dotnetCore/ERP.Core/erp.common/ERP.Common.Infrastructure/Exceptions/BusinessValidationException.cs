using System;


namespace ERP.Common.Infrastructure.Exceptions
{
    public class BusinessValidationException : Exception
    {
        public BusinessValidationException()
        {
        }
        public BusinessValidationException(string message)
            : base(message)
        {
        }
        public BusinessValidationException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
        public BusinessValidationException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
