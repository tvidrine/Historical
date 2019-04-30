using System;
using System.Security.Cryptography;
using System.Text;
using Gemini.Core.Contracts.DomainServices;

namespace Gemini.Core.DomainServices
{
    public class PasswordService : IPasswordService
    {
        private const int HashByteSize = 20; // to match the size of the PBKDF2-HMAC-SHA-1 hash 
        private const int Pbkdf2Iterations = 20000;
        private const int IterationIndex = 0;
        private const int SaltIndex = 1;
        private const int Pbkdf2Index = 2;
        private const int SaltByteLength = 24;
 

        public string HashPassword(string password)
        {
            var cryptoProvider = new RNGCryptoServiceProvider();
            var salt = GenerateRandomSalt();
            var iterations = GetIterations();
            cryptoProvider.GetBytes(salt);

            var hash = GetPbkdf2Bytes(password, salt, iterations, HashByteSize);

            var hashedPassword = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($@"{iterations}:{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}")
                );
            return hashedPassword ;
        }

        public bool ValidatePassword(string password, string correctHash)
        {
            char[] delimiter = {':'};
            var buffer = Convert.FromBase64String(correctHash);
            var passwordHash = Encoding.UTF8.GetString(buffer);
            var split = passwordHash.Split(delimiter);
            var iterations = int.Parse(split[IterationIndex]);
            var salt = Convert.FromBase64String(split[SaltIndex]);
            var hash = Convert.FromBase64String(split[Pbkdf2Index]);

            var testHash = GetPbkdf2Bytes(password, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        private static byte[] GetPbkdf2Bytes(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt)
            {
                IterationCount = iterations
            };

            return pbkdf2.GetBytes(outputBytes);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint) a.Length ^ (uint) b.Length;
            for (var i = 0; i < a.Length && i < b.Length; i++) diff |= (uint) (a[i] ^ b[i]);
            return diff == 0;
        }

        private static byte[] GenerateRandomSalt()
        {
            var csprng = new RNGCryptoServiceProvider();
            var salt = new byte[SaltByteLength];
            csprng.GetBytes(salt);
            return salt;
        }

        private int GetIterations()
        {
            return Pbkdf2Iterations;
        }
    }
}