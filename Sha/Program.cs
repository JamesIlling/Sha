using System;
using System.Security.Cryptography;
using System.Text;

namespace SecureHashAlgorithm
{
    static class Program
    {
        static void Main(string[] args)
        {
            var options = new Configuration();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                HashAlgorithm hashAlgorithm;
                switch (options.Hash.ToUpper())
                {
                    case "SHA1":
                        hashAlgorithm = new SHA1Managed();
                        break;
                    case "SHA256":
                        hashAlgorithm = new SHA256Managed();
                        break;
                    case "SHA384":
                        hashAlgorithm = new SHA384Managed();
                        break;
                    case "SHA512":
                        hashAlgorithm = new SHA512Managed();
                        break;
                    default:
                        Console.WriteLine($"Unknown Hash {options.Hash}");
                        return;                        
                }
                var textToHash = options.Input;
                if (string.IsNullOrEmpty(textToHash))
                {
                    Console.WriteLine("No data to hash");
                    return;
                }
                var hash = hashAlgorithm.ComputeHash(Encoding.ASCII.GetBytes(textToHash));
                Console.WriteLine(hash.ToHexString());        
            }                           
        }    
    }

    public static class Extension
    {
        public static string ToHexString(this byte[] data)
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
