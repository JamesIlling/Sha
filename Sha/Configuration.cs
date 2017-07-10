using CommandLine;
using CommandLine.Text;

namespace SecureHashAlgorithm
{
    public class Configuration
    {
        [Option('h', "hash", Required = true, HelpText = "The cryptographic hash to use")]
        public string Hash{ get; set; }


        [Option('i', "input", Required = true, HelpText = "Input to encrypt")]
        public string Input { get; set; }

        [HelpOption]
        public string GetUsage()
        {            
            return HelpText.AutoBuild(this,
                (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
