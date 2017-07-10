using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using SecureHashAlgorithm;


namespace ShaTests
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public void AppExitsOnNoParameters()
        {
            var output = RunProgram(new string[0],false);
            output.Should().BeNullOrWhiteSpace();
        }

        [Test]
        public void HelpDisplayedOnInvalidInput()
        {
            var output = RunProgram(new[] {"-q"}, false);
            output.Should().BeNullOrWhiteSpace();
        }

        [Test]
        public void UnknownHashDisplaysError()
        {
            var output = RunProgram(new[] { "-h", "Bob", "-i", "password" }, false);
            output.Trim().Should().Be("Unknown hash Algorith Bob.");
        }

        [Test]
        public void MissingDataDisplaysError()
        {
            var output = RunProgram(new[] { "-h", "SHA256", "-i", "" }, false);
            output.Trim().Should().Be("No data to hash");
        }

        [Test]
        public void Valid()
        {
            var output = RunProgram(new[] {"-h","SHA1","-i","password"},false);
            output.Trim().Should().Be("5BAA61E4C9B93F3F0682250B6CF8331B7EE68FD8");
        }

        public string RunProgram(string[] args,bool error)
        {
            using (var ms = new MemoryStream())
            {
                using (var tw = new StreamWriter(ms))
                {
                    if (error)
                    {
                        Console.SetError(tw);
                    }
                    else
                    {
                        Console.SetOut(tw);
                    }                    
                    Program.Main(args);
                    tw.Flush();

                    ms.Seek(0, SeekOrigin.Begin);
                    using (var tr = new StreamReader(ms))
                    {
                        var text = tr.ReadToEnd();
                        return text;
                    }
                }
            }
        }
    }
}
