using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.EncryptDecrypt
{
    using Plugin.EncryptDecrypt.Abstractions;
#if NETSTANDARD1_0
    public class EncryptDecryptImplementation : EncryptDecryptBase, IEncryptDecrypt
    {
        Task<string> IEncryptDecrypt.DecryptStringAsync(string password, string data)
        {
            throw new NotImplementedException();
        }

        Task<string> IEncryptDecrypt.EncryptStringAsync(string password, string data)
        {
            throw new NotImplementedException();
        }

        protected override string Decrypt(string data2, byte[] keys)
        {
            throw new NotImplementedException();
        }

        protected override string Encrypt(byte[] keys, string data)
        {
            throw new NotImplementedException();
        }

        protected override byte[] GetKey(string password)
        {
            throw new NotImplementedException();
        }

    }



#else
    using System.Security.Cryptography;
    using System.Runtime.Serialization;


    public class EncryptDecryptImplementation : EncryptDecryptBase, IEncryptDecrypt
    {

        protected override string Encrypt(byte[] keys, string data)
        {
            byte[] d = UTF8Encoding.UTF8.GetBytes(data);

            using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
            {
                ICryptoTransform transform = tripDes.CreateEncryptor();
                byte[] results = transform.TransformFinalBlock(d, 0, d.Length);
                return Convert.ToBase64String(results, 0, results.Length);
            }
        }
        protected override string Decrypt(string data2, byte[] keys)
        {
            try
            {
                byte[] data = Convert.FromBase64String(data2);

                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return UTF8Encoding.UTF8.GetString(results);
                }
            }
            catch (CryptographicException cx)
            {
                if (cx.Message.StartsWith("Bad Data."))
                    throw new EncryptDecryptExceptionWrongPassword("WrongPassword", cx);
                else
                    throw;
            }
        }

        protected override byte[] GetKey(string password)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                return md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(password));
            }
        }


    }

#if WINDOWS_UWP || NETSTANDARD1_0
    internal class EncryptDecriptException : Exception
    {
        public EncryptDecriptException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    internal class EncryptDecryptExceptionDataCorruption : Exception
    {
        public EncryptDecryptExceptionDataCorruption(string message, Exception innerException) : base(message, innerException)
        {
        }

    }

    internal class EncryptDecryptExceptionWrongPassword : Exception
    {
        public EncryptDecryptExceptionWrongPassword(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
#else
    [Serializable]
    internal class EncryptDecriptException : Exception
    {
        public EncryptDecriptException()
        {
        }

        public EncryptDecriptException(string message) : base(message)
        {
        }

        public EncryptDecriptException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EncryptDecriptException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    internal class EncryptDecryptExceptionDataCorruption : Exception
    {
        public EncryptDecryptExceptionDataCorruption()
        {
        }

        public EncryptDecryptExceptionDataCorruption(string message) : base(message)
        {
        }

        public EncryptDecryptExceptionDataCorruption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EncryptDecryptExceptionDataCorruption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    internal class EncryptDecryptExceptionWrongPassword : Exception
    {
        public EncryptDecryptExceptionWrongPassword()
        {
        }

        public EncryptDecryptExceptionWrongPassword(string message) : base(message)
        {
        }

        public EncryptDecryptExceptionWrongPassword(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EncryptDecryptExceptionWrongPassword(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
#endif //UWP 
#endif //Class

}
