using CommandLine;

namespace ListDataMigrator.Importer.Models
{
    public class CommandLineArguments
    {
        [Option('e', "export-config-dir", Required = true, HelpText = "Directory containing the exported definition files of lists/libraries.")]
        public string ExportedConfigDirectory { get; set; }

        [Option('c', "content-type-mapping", Required = true, HelpText = "File path for the content type mappings between the two site collections.")]
        public string ContentTypeMappingPath { get; set; }

        [Option('u', "users-mapping", Required = false, HelpText = "File path for the user mappings from the user information list.")]
        public string UserInformationListPath { get; set; }
    }
}
