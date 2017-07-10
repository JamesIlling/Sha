using FluentAssertions;
using NUnit.Framework;
using SecureHashAlgorithm;

namespace ShaTests
{

    [TestFixture]
    public class HashDetectionTests
    {     
        [TestCase("Sha1", true)]
        [TestCase("SHA1", true)]
        [TestCase("SHa1", true)]
        [TestCase("shA1", true)]
        [TestCase("Sha256", true)]
        [TestCase("SHA384", true)]
        [TestCase("Sha512", true)]
        [TestCase("Sha3-512", false)]
        [TestCase("Sha16", false)]
        [TestCase("Vov", false)]
        public void TestHashDetection(string hash, bool accepted)
        {
            var hashed = HashIdentifier.GetHashAlgorithm(hash);
            accepted.Should().Be(hashed != null);
        }
    }
}
