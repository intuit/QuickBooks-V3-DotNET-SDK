using System;
using System.IO;
using System.Security.Cryptography;

namespace $safeprojectname$
{
    internal static class CryptographyHelper
    {
        /// <summary>
        /// Encrypt using triple DES
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static string EncryptData(string plaintext,string key)
        {
            TripleDESCryptoServiceProvider TripleDes = new TripleDESCryptoServiceProvider();
            TripleDes.Key = TruncateHash(key, TripleDes.KeySize / 8);
            TripleDes.IV = TruncateHash("", TripleDes.BlockSize / 8);
   
            // Convert the plaintext string to a byte array.
            byte[] plaintextBytes = System.Text.Encoding.Unicode.GetBytes(plaintext);

            // Create the stream.
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            // Create the encoder to write to the stream.
            CryptoStream encStream = new CryptoStream(ms, TripleDes.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

            // Use the crypto stream to write the byte array to the stream.
            encStream.Write(plaintextBytes, 0, plaintextBytes.Length);
            encStream.FlushFinalBlock();

            // Convert the encrypted stream to a printable string.
            return Convert.ToBase64String(ms.ToArray());
        }

        /// <summary>
        /// Decrypt using triple DES
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static string DecryptData(string encryptedtext,string key)
        {

            TripleDESCryptoServiceProvider TripleDes = new TripleDESCryptoServiceProvider();
            TripleDes.Key = TruncateHash(key, TripleDes.KeySize / 8);
            TripleDes.IV = TruncateHash("", TripleDes.BlockSize / 8);

            // Convert the encrypted text string to a byte array.
            byte[] encryptedBytes = Convert.FromBase64String(encryptedtext);

            // Create the stream.
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            // Create the decoder to write to the stream.
            CryptoStream decStream = new CryptoStream(ms, TripleDes.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

            // Use the crypto stream to write the byte array to the stream.
            decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
            decStream.FlushFinalBlock();

            // Convert the plaintext stream to a string.
            return System.Text.Encoding.Unicode.GetString(ms.ToArray());
        }



        /// <summary>
        ///  truncate or pad the hash based on required length 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private static byte[] TruncateHash(string key, int length)
        {

            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

            // Hash the key.
            byte[] keyBytes = System.Text.Encoding.Unicode.GetBytes(key);
            byte[] hash = sha1.ComputeHash(keyBytes);

            // Truncate or pad the hash.
            Array.Resize(ref hash, length);
            return hash;
        }


    
    
    
    }
}