using System.IO;
using Microsoft.Build.Utilities;

namespace MSBuild.Community.Tasks.Paket
{
    /// <summary>
    /// Downloads (and updates) paket.exe
    /// </summary>
    public class PaketBootstrap : ToolTask
    {
        /// <summary>
        /// Path to the root directory of the project in which the .paket directory resides.
        /// </summary>
        public string ProjectRoot { get; set; } = string.Empty;

        /// <summary>
        /// Force download from nuget.org, do not use github.com as fallback.
        /// </summary>
        public bool ForceNuget { get; set; }

        /// <summary>
        /// The specified path is used instead of nuget.org when trying to fetch paket.exe as a nuget package.
        /// Combine this with ForceNuget to get paket.exe from a custom source.
        /// </summary>
        public string NuGetSource { get; set; }

        /// <summary>
        /// If the paket.exe already exists, and it is not older than the specified value (in minutes)
        /// all checks will be skipped.
        /// </summary>
        public int MaxFileAge { get; set; } = 120;

        protected override string ToolName => "paket.bootstrapper.exe";

        protected override string GenerateFullPathToTool()
        {
            return Path.Combine(ProjectRoot, ".paket", ToolName);
        }

        protected override string GenerateCommandLineCommands()
        {
            var builder = new CommandLineBuilder();
            if (ForceNuget)
            {
                builder.AppendSwitch("--force-nuget");
            }

            if (!string.IsNullOrWhiteSpace(NuGetSource))
            {
                builder.AppendSwitch($"--nuget-source={NuGetSource}");
            }

            builder.AppendSwitch($"--max-file-age={MaxFileAge}");

            return builder.ToString();
        }
    }
}
