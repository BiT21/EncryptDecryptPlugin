using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 using Plugin.EncryptDecrypt.Abstractions;

namespace Plugin.EncryptDecrypt
{

#if __NETSTANDARD__
    public class EncryptDecryptImplementation : EncryptDecryptBase, IEncryptDecrypt
    {

        protected override string Decrypt(byte[] keys, string data2)
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
#endif

#if WINDOWS_UWP
    using Windows.Security.Cryptography;
    using Windows.Security.Cryptography.Core;
    using Windows.Storage.Streams;

    public class EncryptDecryptImplementation : EncryptDecryptBase, IEncryptDecrypt
    {
        protected override byte[] Decrypt(byte[] keys, byte[] data)
        {
            IBuffer data2Decrypt = CryptographicBuffer.CreateFromByteArray(data);
            IBuffer keyMaterial = CryptographicBuffer.CreateFromByteArray(keys);

            SymmetricKeyAlgorithmProvider objAlg = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.TripleDesEcbPkcs7);
            var key = objAlg.CreateSymmetricKey(keyMaterial);

            var ret = CryptographicEngine.Decrypt(key, data2Decrypt, null);

            CryptographicBuffer.CopyToByteArray(ret, out byte[] byteRet);
            return byteRet;
        }

        protected override byte[] Encrypt(byte[] keys, byte[] data)
        {
            IBuffer data2Encrypt = CryptographicBuffer.CreateFromByteArray(data);
            IBuffer keyMaterial = CryptographicBuffer.CreateFromByteArray(keys);

            SymmetricKeyAlgorithmProvider objAlg = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.TripleDesEcbPkcs7);
            var key = objAlg.CreateSymmetricKey(keyMaterial);

            var ret = CryptographicEngine.Encrypt(key, data2Encrypt, null);

            CryptographicBuffer.CopyToByteArray(ret, out byte[] byteRet);
            return byteRet;
        }

        protected override byte[] GetKey(string password)
        {
            IBuffer buffUtf8Msg = CryptographicBuffer.ConvertStringToBinary(password, BinaryStringEncoding.Utf8);

            HashAlgorithmProvider objAlgProv = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);

            IBuffer buffHash = objAlgProv.HashData(buffUtf8Msg);

            CryptographicBuffer.CopyToByteArray(buffHash, out byte[] byteRet);

            return byteRet;
        }
    }
#endif

#if __ANDROID__ || __IOS__ || __NET__
    using System.Security.Cryptography;
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
        protected override string Decrypt(byte[] keys, string data)
        {
            try
            {
                byte[] dataBytes = Convert.FromBase64String(data);

                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(dataBytes, 0, dataBytes.Length);
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
#endif

    internal class EncryptDecriptException : Exception
    {
        public EncryptDecriptException(string message) : base(message)
        {
        }
        public EncryptDecriptException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    internal class EncryptDecryptExceptionDataCorruption : Exception
    {
        public EncryptDecryptExceptionDataCorruption(string message) : base(message)
        {
        }
        public EncryptDecryptExceptionDataCorruption(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    internal class EncryptDecryptExceptionWrongPassword : Exception
    {
        public EncryptDecryptExceptionWrongPassword(string message) : base(message)
        {
        }
        public EncryptDecryptExceptionWrongPassword(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
    
//#if WINDOWS_UWP || NETSTANDARD1_0
//    internal class EncryptDecriptException : Exception
//    {
//        public EncryptDecriptException(string message) : base(message)
//        {
//        }
//        public EncryptDecriptException(string message, Exception innerException) : base(message, innerException)
//        {
//        }
//    }

//    internal class EncryptDecryptExceptionDataCorruption : Exception
//    {
//        public EncryptDecryptExceptionDataCorruption(string message) : base(message)
//        {
//        }
//        public EncryptDecryptExceptionDataCorruption(string message, Exception innerException) : base(message, innerException)
//        {
//        }

//    }

//    internal class EncryptDecryptExceptionWrongPassword : Exception
//    {
//        public EncryptDecryptExceptionWrongPassword(string message) : base(message)
//        {
//        }
//        public EncryptDecryptExceptionWrongPassword(string message, Exception innerException) : base(message, innerException)
//        {
//        }
//    }
//#else
//    [Serializable]
//    internal class EncryptDecriptException : Exception
//    {
//        public EncryptDecriptException()
//        {
//        }

//        public EncryptDecriptException(string message) : base(message)
//        {
//        }

//        public EncryptDecriptException(string message, Exception innerException) : base(message, innerException)
//        {
//        }

//        protected EncryptDecriptException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
//        {
//        }
//    }

//    [Serializable]
//    internal class EncryptDecryptExceptionDataCorruption : Exception
//    {
//        public EncryptDecryptExceptionDataCorruption()
//        {
//        }

//        public EncryptDecryptExceptionDataCorruption(string message) : base(message)
//        {
//        }

//        public EncryptDecryptExceptionDataCorruption(string message, Exception innerException) : base(message, innerException)
//        {
//        }

//        protected EncryptDecryptExceptionDataCorruption(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
//        {
//        }
//    }

//    [Serializable]
//    internal class EncryptDecryptExceptionWrongPassword : Exception
//    {
//        public EncryptDecryptExceptionWrongPassword()
//        {
//        }

//        public EncryptDecryptExceptionWrongPassword(string message) : base(message)
//        {
//        }

//        public EncryptDecryptExceptionWrongPassword(string message, Exception innerException) : base(message, innerException)
//        {
//        }

//        protected EncryptDecryptExceptionWrongPassword(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
//        {
//        }
//    }
//#endif //UWP 

}
