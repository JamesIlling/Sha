using System;
using System.Security.Cryptography;
using System.Text;

namespace SecureHashAlgorithm
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var options = new Configuration();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {

                var hashAlgorithm = HashIdentifier.GetHashAlgorithm(options.Hash);
                if (hashAlgorithm == null)
                {
                    Console.WriteLine($"Unknown hash Algorith {options.Hash}.");
                    return;
                }
                var textToHash = options.Input;
                if (string.IsNullOrEmpty(textToHash))
                {
                    Console.WriteLine("No data to hash");
                    return;
                }
                Console.WriteLine(hashAlgorithm.GenerateHash(textToHash));
            }                           
        }


        private static string GenerateHash(this HashAlgorithm hashAlgorithm, string textToHash)
        {
            var hash = hashAlgorithm.ComputeHash(Encoding.ASCII.GetBytes(textToHash));
            return hash.ToHexString();
        }

        private static string ToHexString(this byte[] data)
        {
            var hexString = new StringBuilder();
            foreach (var b in data)
            {
                hexString.AppendFormat("{0:X2}", b);
            }
            return hexString.ToString();
        }
    }
}
