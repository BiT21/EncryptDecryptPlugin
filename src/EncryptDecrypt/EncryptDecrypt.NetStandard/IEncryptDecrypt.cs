﻿using System.Threading.Tasks;

namespace BiT21.EncryptDecrypt.NetStandard
{
    public interface IEncryptDecrypt
    {
        /// <summary>
        /// Decrypts data with the given format.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="data"></param>
        /// <returns>Decripted string.</returns>
        /// <exception cref="EncryptDecryptExceptionDataCorruption">Thrown when is detected that the data provided has been modified since the encryption.</exception>        
        /// <exception cref="EncryptDecriptException">General error decrypting. It could be wrong password.</exception>
        Task<string> DecryptStringAsync(string password, string data);

        /// <summary>
        /// Encrypts data with the given password.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="data"></param>
        /// <returns>Return the encrypted data on Base64 format</returns>
        /// <exception cref="EncryptDecriptException">General error exception.</exception>
        Task<string> EncryptStringAsync(string password, string data);
    }
}