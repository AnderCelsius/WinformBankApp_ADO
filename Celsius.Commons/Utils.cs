using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Celsius.Commons
{
    public class Utils
    {
        public static Guid Id { get; set; } = Guid.NewGuid();
        public enum AccountType
        {
            Savings,
            Current
        }

        public enum TransactionType
        {
            Cr,
            Dr
        }

        public enum TransactionDescription
        {
            POS,
            ATM,
            USSD,
            FIP,
        }

        public static string GenerateAccountNumber()
        {
            string startWith = "32";
            Random generator = new Random();
            string r = generator.Next(0, 999999).ToString("D8");
            string accountNumber = startWith + r;
            return accountNumber;

        }

        /// <summary>
        /// Generate PasswordHash and PasswordSalt for a given password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static Tuple<byte[], byte[]> GeneratePasswordCredentials(string password)
        {
            byte[] passwordSalt, passwordHash;
            // convert password to hash value and generate salt
            using (var hash = new HMACSHA512())
            {
                passwordSalt = hash.Key;
                passwordHash = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
            var passwordCredentials = new Tuple<byte[], byte[]>(passwordSalt, passwordHash);
            return passwordCredentials;
        }


        /// <summary>
        /// compare passwordhash with the inputed password.
        /// </summary>
        /// <param name="passwordSalt"></param>
        /// <param name="passwordHash"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool CompareHash(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            byte[] passwordToCompare;
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                passwordToCompare = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            var test = passwordHash.SequenceEqual(passwordToCompare);
            return test;
        }
    }
}
