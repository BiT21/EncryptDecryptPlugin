using System;
using System.Collections.Generic;
using System.Text;

namespace BiT21.EncryptDecryptLib.Exceptions
{
    public class EncryptDecriptException : Exception
    {
        public EncryptDecriptException(string message) : base(message)
        {
        }
        public EncryptDecriptException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class EncryptDecryptExceptionDataCorruption : Exception
    {
        public EncryptDecryptExceptionDataCorruption(string message) : base(message)
        {
        }
        public EncryptDecryptExceptionDataCorruption(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
