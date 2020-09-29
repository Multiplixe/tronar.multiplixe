using multiplixe.ferramentas.comum.commands;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace multiplixe.ferramentas.comum.projects
{
    public abstract class Project
    {
        public string PhysicalRootPath { get; }
        public string FileProjectPath { get; }
        public string ProjectPath { get; }
        public string NugetSource { get; }
        public string Namespace { get; set; }
        public string FullName { get; set; }
        public string CurrentVersion { get; set; }
        public string NewVersion { get; set; }
        public typeOfProject Type { get; set; }
        public List<string> Packages { get; set; }

        public Project(string @namespace, string name) : this()
        {
            Namespace = @namespace;

            if (name == "adduo.helper")
            {
                FullName = name;
            }
            else
            {
                FullName = name;
            }

            //Type = type;

            ProjectPath = Path.Combine(PhysicalRootPath, @namespace, FullName);
            FileProjectPath = Path.Combine(ProjectPath, string.Concat(FullName, ".csproj"));


            ExtractDataProjectFile();
        }

        public Project()
        {
            PhysicalRootPath = @"C:\inetpub\wwwroot\tronar.gamificacao";
            NugetSource = "github";
        }

        private void ExtractDataProjectFile()
        {

            SetPackages();
        }

        public void SetVersion()
        {
            var content = System.IO.File.ReadAllText(FileProjectPath);

            var matches = System.Text.RegularExpressions.Regex.Matches(content, @"<Version>([^""]*)</Version>");

            if (matches.Any())
            {
                CurrentVersion = matches[0].Groups[1].Value;
                var versionSplit = CurrentVersion.Split('.');

                var major = 1;
                var middle = 0;
                var minor = 0;

                if (versionSplit.Length == 3)
                {
                    int.TryParse(versionSplit[0], out major);
                    int.TryParse(versionSplit[1], out middle);
                    int.TryParse(versionSplit[2], out minor);

                    minor++;
                }

                NewVersion = $"{major}.{middle}.{minor}";

                var newContentCsProj = content.Replace($"<Version>{CurrentVersion}</Version>", $"<Version>{NewVersion}</Version>");

                File.WriteAllText(FileProjectPath, newContentCsProj);
            }

        }

        private void SetPackages()
        {
            var content = System.IO.File.ReadAllText(FileProjectPath);

            Packages = new List<string>();

            var matches = System.Text.RegularExpressions.Regex.Matches(content, @"PackageReference Include=""([^""]*)"" Version=""([^""]*)""");

            var allowedPackages = new string[]
            {
                "game", "adduo"
            };

            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                if (allowedPackages.Any(a => match.Groups[1].Value.StartsWith(a)))
                {
                    Packages.Add(match.Groups[1].Value);
                }
            }
        }

        public abstract Command CreatePublishCommand();
    }
}

