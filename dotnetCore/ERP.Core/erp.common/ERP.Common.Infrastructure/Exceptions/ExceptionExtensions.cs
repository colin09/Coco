using System;


namespace ERP.Common.Infrastructure.Exceptions
{
    public static class ExceptionExtensions
    {
        public static bool IsBusinessException(this Exception ex)
        {
            return ex is BusinessException || ex is BusinessValidationException;
        }
    }
}
