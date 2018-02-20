using BiT21.EncryptDecryptLib.Exceptions;
using BiT21.EncryptDecryptLib.IService;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BiT21.EncryptDecryptLib.Service
{
    public class EncryptDecrypt : IEncryptDecrypt
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
                    throw new EncryptDecryptExceptionDataCorruption("Starting padding does not match");

                return Task.FromResult(ret.Substring(START_PADDING.Length));
            }
            catch (EncryptDecryptExceptionDataCorruption)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new EncryptDecriptException("Unexpected exception", ex);
            }
        }

        private byte[] Encrypt(byte[] keys, byte[] data)
        {
            using (var prov = new AesCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
            {
                ICryptoTransform transform = prov.CreateEncryptor();
                byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                return results;
            }
        }
        private byte[] Decrypt(byte[] keys, byte[] data)
        {
            try
            {
                using (var prov = new AesCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = prov.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);

                    return results;
                }
            }
            catch (Exception ex)
            {
                throw new EncryptDecriptException("Error Decrypting data. WrongPassword or DataCorruption.", ex);
            }
        }
        private byte[] GetKey(string password)
        {
            using (var hashProv = new SHA256CryptoServiceProvider())
            {
                return hashProv.ComputeHash(UTF8Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
