using System.IO;
using Microsoft.Build.Utilities;

namespace MSBuild.Community.Tasks.Paket
{
    public class PaketRestore : ToolTask
    {
        /// <summary>
        /// If true Paket will run in verbose mode and show detailed information.
        /// </summary>
        public bool Verbose { get; set; }

        /// <summary>
        /// Touches project files referencing packages which are being restored, to help incremental 
        /// build tools detecting the change.
        /// </summary>
        public bool TouchAffectedRefs { get; set; }

        protected override string ToolName => "paket.exe";

        protected override string GenerateCommandLineCommands()
        {
            var builder = new CommandLineBuilder();
            builder.AppendSwitch("restore");
            if (Verbose)
            {
                builder.AppendSwitch("-v");
            }

            if (TouchAffectedRefs)
            {
                builder.AppendSwitch("--touch-affected-refs");
            }
            return builder.ToString();
        }

        protected override string GenerateFullPathToTool()
        {
            return string.IsNullOrWhiteSpace(ToolPath)
                ? Path.Combine(".paket", ToolName)
                : Path.Combine(ToolPath, ToolName);
        }
    }
}
