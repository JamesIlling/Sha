using System;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;

namespace SecureHashAlgorithm
{
    public static class HashIdentifier
    {
        public static HashAlgorithm GetHashAlgorithm(string hashName)
        {            
            var asm = typeof(SHA1Managed).Assembly;
            var typeNames = asm.GetExportedTypes()
                .Where(x => x.IsPublic && x.Name.Contains("SHA") && x.Name.Contains("Managed")).ToList();

            hashName = hashName + "Managed";

            var hashType = typeNames.FirstOrDefault(x => string.Compare(x.Name,hashName,StringComparison.OrdinalIgnoreCase)==0)?.FullName;

            if (hashType != null)
            {
               return (HashAlgorithm) asm.CreateInstance(hashType);
            }
            return null;
        }
    }
}
