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
                byte[] keys = GetKey(password);

                var ret = Encrypt(keys, START_PADDING + data);

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

                var ret = Decrypt(data, keys);

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

        protected abstract string Encrypt(byte[] keys, string data);
        protected abstract string Decrypt(string data2, byte[] keys);
        protected abstract byte[] GetKey(string password);
    }
}
