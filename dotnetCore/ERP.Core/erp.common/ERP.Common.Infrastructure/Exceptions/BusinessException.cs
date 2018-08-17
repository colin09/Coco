using System;


namespace ERP.Common.Infrastructure.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException()
        {
        }
        public BusinessException(string message)
            : base(message)
        {
        }
        public BusinessException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
        public BusinessException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
