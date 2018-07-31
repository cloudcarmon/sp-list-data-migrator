using CommandLine;

namespace ListDataMigrator.Exporter.Models
{
    public class CommandLineArguments
    {
        [Option('e', "export-config", Required = true, HelpText = "Input file to be processed containing the export definition.")]
        public string ExportConfigFile { get; set; }

        [Option('o', "output-path", Required = true, HelpText = "Path for exported files to reside.")]
        public string OutputPath { get; set; }

        [Option('c', "content-type-mapping", Required = true, HelpText = "File path for the content type mappings between the two site collections.")]
        public string ContentTypeMappingPath { get; set; }
    }
}
