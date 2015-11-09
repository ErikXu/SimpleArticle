using System;

namespace SimpleArticle.Domain
{
    public class BusinessException : Exception
    {
        public BusinessExceptionLevel Level { get; set; }

        public BusinessException(string message, BusinessExceptionLevel level = BusinessExceptionLevel.Error)
            : base(message)
        {
            Level = level;
        }
    }

    public enum BusinessExceptionLevel
    {
        Error,
        Warning
    }
}