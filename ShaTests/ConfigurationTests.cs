using FluentAssertions;
using NUnit.Framework;
using SecureHashAlgorithm;

namespace ShaTests
{
    [TestFixture]
    public class ConfigurationTests
    {
        [Test]
        public void ConfigurationDetectsShortFlags()
        {
            var config = ParseArgs(new[] {"-h", "Hash", "-i", "input"});
            config.Hash.Should().Be("Hash");
            config.Input.Should().Be("input");
        }

        [Test]
        public void ConfigurationDetectsFullFlags()
        {
            var config = ParseArgs(new[] { "--hash", "Hash", "--input", "input" });
            config.Hash.Should().Be("Hash");
            config.Input.Should().Be("input");
        }

        [Test]
        public void ConfigurationFailsToParseRubbish()
        {
            var config = ParseArgs(new[] { "nanananananananana" });
            config.Should().BeNull();            
        }

        [Test]
        public void HelpTextContainsVariableNames()
        {
            var config = new Configuration();
            var message =config.GetUsage();
            message.Should().Contain("hash");
            message.Should().Contain("input");
        }

        private Configuration ParseArgs(string[] args)
        {
            var options = new Configuration();
            return CommandLine.Parser.Default.ParseArguments(args, options) ? options : null;
        }
    }
}

