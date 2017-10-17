using Plugin.EncryptDecrypt.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.EncryptDecrypt
{
    //REF: https://www.youtube.com/watch?v=ysxC6-AFEYg

    public abstract class EncryptDecryptBase : IEncryptDecrypt
    {
        const string START_PADDING = "abcdefghijklmopqrstuvwxwz0123456789";

        Task<string> IEncryptDecrypt.EncryptStringAsync(string password, string data)
        {
            try
            {
                byte[] data2Encrypt = UTF8Encoding.UTF8.GetBytes(START_PADDING + data);
                byte[] keys = GetKey(password);

                var results = Encrypt(keys, data2Encrypt);

                var ret = Convert.ToBase64String(results, 0, results.Length);

                return Task.FromResult(ret);
            }
            catch (Exception ex)
            {
                throw new EncryptDecriptException("Unexpected exception.", ex);
            }
        }

        Task<string> IEncryptDecrypt.DecryptStringAsync(string password, string data)
        {
            try
            {
                byte[] keys = GetKey(password);
                byte[] data2Decrypt = Convert.FromBase64String(data);

                var results = Decrypt(keys, data2Decrypt);

                var ret = UTF8Encoding.UTF8.GetString(results);

                if (!ret.StartsWith(START_PADDING))
                    throw new Exception("Starting padding does not match");

                return Task.FromResult(ret.Substring(START_PADDING.Length));
            }
            catch (EncryptDecryptExceptionWrongPassword)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new EncryptDecryptExceptionDataCorruption("DataCorruption", ex);
            }
        }

        protected abstract byte[] Encrypt(byte[] keys, byte[] data);
        protected abstract byte[] Decrypt(byte[] keys, byte[] data);
        protected abstract byte[] GetKey(string password);
    }
}
