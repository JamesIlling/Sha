using System;
using System.Linq;
using DotNetVersion;
using FluentAssertions;
using NUnit.Framework;
using SecureHashAlgorithm;

namespace ShaTests
{

    [TestFixture]
    public class HashDetectionTests
    {
        [Test]
        public void TestClr()
        {
            var versionDetector = new VersionDetector();
            var versions = versionDetector.dotNetVersionsInstalled();
            versions.Any(x => Version.Parse(x) > new Version(4, 6, 2) || Version.Parse(x) == new Version(4, 5, 2)).Should().BeTrue();
        }

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
